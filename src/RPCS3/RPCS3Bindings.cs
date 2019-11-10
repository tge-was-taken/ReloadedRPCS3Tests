using System;
using System.Diagnostics;
using Reloaded.Memory.Sigscan;
using System.Runtime.InteropServices;
using Reloaded.Hooks.Definitions.X64;
using Reloaded.Hooks.Definitions;
using ReloadedRPCS3TestsNoConfig.Utilities;
using ReloadedRPCS3TestsNoConfig.Interop.CPP;
using Reloaded.Mod.Interfaces;
using System.Collections.Generic;
using ReloadedRPCS3TestsNoConfig.RPCS3.Types;

namespace ReloadedRPCS3TestsNoConfig.RPCS3
{
    [UnmanagedFunctionPointer( CallingConvention.Cdecl ), Function( CallingConventions.Microsoft )]
    public unsafe delegate Emulator* Emulator_ctorDelegate( Emulator* pThis );

    [UnmanagedFunctionPointer( CallingConvention.Cdecl ), Function( CallingConventions.Microsoft )]
    public unsafe  delegate void Emulator_InitDelegate( Emulator* pThis );

    [UnmanagedFunctionPointer( CallingConvention.Cdecl ), Function( CallingConventions.Microsoft )]
    public unsafe  delegate void Emulator_LoadDelegate( Emulator* pThis, StdString* titleId, Bool addOnly, Bool forceGlobalConfig );

    [UnmanagedFunctionPointer( CallingConvention.Cdecl ), Function( CallingConventions.Microsoft )]
    public unsafe  delegate void Emulator_RunDelegate( Emulator* pThis );

    [UnmanagedFunctionPointer( CallingConvention.Cdecl ), Function( CallingConventions.Microsoft )]
    public unsafe  delegate uint vm_allocDelegate( uint size, uint location, uint align = 0x10000 );

    [UnmanagedFunctionPointer( CallingConvention.Cdecl ), Function( CallingConventions.Microsoft )]
    public unsafe delegate uint vm_deallocDelegate( uint addr, uint location );

    [UnmanagedFunctionPointer( CallingConvention.Cdecl ), Function( CallingConventions.Microsoft )]
    public unsafe delegate void ppu_initializeDelegate();

    [UnmanagedFunctionPointer( CallingConvention.Cdecl ), Function( CallingConventions.Microsoft )]
    public unsafe delegate void ppu_initialize_moduleDelegate( ppu_module* info );

    [UnmanagedFunctionPointer( CallingConvention.Cdecl ), Function( CallingConventions.Microsoft )]
    public unsafe delegate void* utils_memory_reserveDelegate( ulong size, void* use_addr = null );

    [UnmanagedFunctionPointer( CallingConvention.Cdecl ), Function( CallingConventions.Microsoft )]
    public unsafe delegate void PPUFunctionDelegate( context_t* context );

    /// <summary>
    /// Responsible for managing bindings with an RPCS3 process.
    /// </summary>
    public unsafe class RPCS3Bindings
    {
        private Dictionary<uint, PPUFunction> mPPUFunctions;

        // Hooks
        private IHook<Emulator_ctorDelegate> mEmulatorCtorHook;
        private IHook<ppu_initializeDelegate> mPPUInitializeHook;
        private IHook<ppu_initialize_moduleDelegate> mPPUInitializeModuleHook;
        private IHook<utils_memory_reserveDelegate> mUtilsMemoryReserveHook;

        // Functions
        public IFunction<Emulator_ctorDelegate> Emulator_ctor { get; }
        public IFunction<Emulator_InitDelegate> Emulator_Init { get; }
        public IFunction<Emulator_LoadDelegate> Emulator_Load { get; }
        public IFunction<Emulator_RunDelegate> Emulator_Run { get; }
        public IFunction<vm_allocDelegate> vm_alloc { get; }
        public IFunction<vm_deallocDelegate> vm_dealloc { get; }
        public IFunction<ppu_initializeDelegate> ppu_initialize { get; }
        public IFunction<ppu_initialize_moduleDelegate> ppu_initialize_module { get; }
        public IFunction<utils_memory_reserveDelegate> utils_memory_reserve { get; }

        /// <summary>
        /// Global/static variables
        /// </summary>
        public Emulator* Emu { get; private set; }

        /// <summary>
        /// Emulated virtual memory
        /// </summary>
        public byte* vm_g_base_addr { get; private set; }

        /// <summary>
        /// Unprotected virtual memory mirror
        /// </summary>
        public byte* vm_g_sudo_addr { get; private set; }

        /// <summary>
        /// Auxiliary virtual memory for executable areas
        /// </summary>
        public byte* vm_g_exec_addr { get; private set; }

        /// <summary>
        /// Stats for debugging
        /// </summary>
        public byte* vm_g_stat_addr { get; private set; }

        /// <summary>
        /// Reservation stats (compressed x16)
        /// </summary>
        public byte* vm_g_reservations { get; private set; }

        public IReadOnlyDictionary<uint, PPUFunction> PPUFunctions => mPPUFunctions;

        /// <summary>
        /// Gets the virtual memory accessor.
        /// </summary>
        public RPCS3VirtualMemory VirtualMemory { get; }

        public RPCS3Bindings( Process process )
        {
            mPPUFunctions = new Dictionary<uint, PPUFunction>();

            var scanner = new Scanner(process, process.MainModule);

            //* Find functions
            Emulator_ctor = SigscanHelper.CreateFunction<Emulator_ctorDelegate>( scanner, "Emulator::Init", "48 89 4C 24 08 57 48 83 EC 30 48 C7 44 24 20 FE FF FF FF 48 89 5C 24 48 48 8B D9 C7 01 02 00 00" );
            Emulator_Init = SigscanHelper.CreateFunction<Emulator_InitDelegate>( scanner, "Emulator::Init", "48 8B C4 55 41 54 41 55 41 56 41 57 48 8D A8 88 F9 FF FF 48 81 EC 50 07 00 00 48 C7 45 80 FE FF" );
            Emulator_Load = SigscanHelper.CreateFunction<Emulator_LoadDelegate>( scanner, "Emulator::Load", "40 57 41 54 41 55 41 56 41 57 B8 70 1B 00 00 E8 ?? ?? ?? ?? 48 2B E0 48 C7 84 24 10 06 00 00 FE" );
            Emulator_Run = SigscanHelper.CreateFunction<Emulator_RunDelegate>( scanner, "Emulator::Run", "4C 8B DC 57 48 81 EC 80 00 00 00 49 C7 43 A8 FE FF FF FF 49 89 5B 10 49 89 6B 18 49 89 73 20 48" );
            vm_alloc = SigscanHelper.CreateFunction<vm_allocDelegate>( scanner, "vm::alloc", "40 57 48 83 EC 60 48 C7 44 24 30 FE FF FF FF 48 89 5C 24 70 48 89 74 24 78 41 8B F8 8B DA 8B F1 45 33 C0 48 8D 4C 24 48 E8 ?? ?? ?? ?? 90" );
            vm_dealloc = SigscanHelper.CreateFunction<vm_deallocDelegate>( scanner, "vm::dealloc", "40 57 48 83 EC 50 48 C7 44 24 30 FE FF FF FF 48 89 5C 24 60 8B FA 8B D9 44 8B C1 48 8D 4C 24 38 E8 ?? ?? ?? ?? 90" );
            ppu_initialize = SigscanHelper.CreateFunction<ppu_initializeDelegate>( scanner, "ppu_initialize", "40 57 48 83 EC ?? 48 C7 44 24 20 FE FF FF FF 48 89 5C 24 70 48 89 74 24 78 8B 0D ?? ?? ?? ?? 48" );
            ppu_initialize_module = SigscanHelper.CreateFunction<ppu_initialize_moduleDelegate>( scanner, "ppu_initialize", "48 8B C4 55 41 54 41 55 41 56 41 57 48 8D A8 A8 F8 FF FF 48 81 EC ?? ?? 00 00 48 C7 85 08 01 00" );
            utils_memory_reserve = SigscanHelper.CreateFunction<utils_memory_reserveDelegate>( scanner, "utils::memory_reserve", "48 8B C2 41 B9 01 00 00 00 48 8B D1 41 B8 00 20 00 00 48 8B C8 48" );

            //* Hook some to fill out some variables
            mEmulatorCtorHook = Emulator_ctor.Hook( EmulatorCtorImpl );
            mPPUInitializeHook = ppu_initialize.Hook( PPUInitializeImpl );
            mPPUInitializeModuleHook = ppu_initialize_module.Hook( PPUInitializeModuleImpl );
            mUtilsMemoryReserveHook = utils_memory_reserve.Hook( UtilsMemoryReserveImpl );

            //* Create memory accessor for ease of use
            VirtualMemory = new RPCS3VirtualMemory( this );
        }

        private void* UtilsMemoryReserveImpl( ulong size, void* use_addr )
        {
            var addr = mUtilsMemoryReserveHook.OriginalFunction( size, use_addr );
            if ( addr != null && size == 0x100000000 )
            {
                // Most likely a global memory allocation
                var byteAddr = (byte*)addr;
                if ( vm_g_base_addr == null )          vm_g_base_addr = byteAddr;
                else if ( vm_g_sudo_addr == null )     vm_g_sudo_addr = byteAddr;
                else if ( vm_g_exec_addr == null )     vm_g_exec_addr = byteAddr;
                else if ( vm_g_stat_addr == null )     vm_g_stat_addr = byteAddr;
                else if ( vm_g_reservations == null )  vm_g_reservations = byteAddr;
            }

            return addr;
        }

        private void PPUInitializeImpl()
        {
            mPPUFunctions.Clear();
            mPPUInitializeHook.OriginalFunction();
        }

        private void PPUInitializeModuleImpl( ppu_module* info )
        {
            mPPUInitializeModuleHook.OriginalFunction( info );
            var logger = Services.Get<ILogger>();
            var hooks = Services.Get<IReloadedHooks>();

            logger.WriteLine( $"ppu_module: name: {info->name} path: {info->path} cache: {info->cache}" );

            foreach ( var func in info->funcs )
            {
                //var jitAddr = *ppu_ref<uint>(func.addr);
                //logger.WriteLine( $"ppu_function: addr: {func.addr:X8} jit addr: {jitAddr:X8} name: {func.name}" );
                //var jitFunc = hooks.CreateFunction<PPUFunctionDelegate>( jitAddr );
                //mPPUFunctions[addr] = new PPUFunction( jitFunc, jitAddr );

                var blocks = func.blocks.GetValues();
                foreach ( var block in blocks )
                {
                    var addr = block.First;
                    var jitAddr = *ppu_ref<uint>(addr);
                    logger.WriteLine( $"ppu_function: addr: {addr:X8} jit addr: {jitAddr:X8} name: {func.name}" );
                    var jitFunc = hooks.CreateFunction<PPUFunctionDelegate>( jitAddr );
                    mPPUFunctions[addr] = new PPUFunction( jitFunc, jitAddr );
                }
            }
        }

        private Emulator* EmulatorCtorImpl( Emulator* pThis )
        {
            Emu = pThis;
            return mEmulatorCtorHook.OriginalFunction( pThis );
        }

        public void Activate()
        {
            mEmulatorCtorHook.Activate();
            mPPUInitializeModuleHook.Activate();
            mUtilsMemoryReserveHook.Activate();
        }    

        public void Suspend()
        {
            mEmulatorCtorHook.Disable();
        }

        public void Resume()
        {
            mEmulatorCtorHook.Enable();
        }

        public void Unload()
        {
            Suspend();
        }

        //* Ported inline functions
        public T* ppu_ref<T>( uint addr ) where T : unmanaged
        {
            return ( T* )( vm_g_exec_addr + ( ulong )addr * 2 );
        }
    }
}
