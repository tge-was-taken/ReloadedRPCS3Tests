using System;
using System.Diagnostics;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using Reloaded.Memory.Sigscan;
using Reloaded.Hooks;
using Reloaded.Hooks.Definitions;
using System.Threading;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using Reloaded.Memory.Exceptions;
using System.Runtime.InteropServices;
using Reloaded.Hooks.Definitions.X64;
using System.Text;
using Reloaded.Memory.Sources;
using System.Runtime.CompilerServices;
using ReloadedRPCS3TestsNoConfig.RPCS3;
using ReloadedRPCS3TestsNoConfig.Games.HelloWorld;
using Reloaded.Memory.Sigscan.Structs;
using System.Collections.Generic;
using ReloadedRPCS3TestsNoConfig.RPCS3.Types;
using ReloadedRPCS3TestsNoConfig.Games.P5;

namespace ReloadedRPCS3TestsNoConfig
{
    public class Services
    {
        private static Dictionary<Type, object> sServices = new Dictionary<Type, object>();

        public static void Register<T>(Type type, T obj)
        {
            sServices[type] = obj;
        }

        public static void Register<T>(T obj )
        {
            sServices[typeof(T)] = obj;
        }

        public static T Get<T>()
        {
            return ( T )sServices[typeof( T )];
        }
    }

    public unsafe class Program : IMod, IExports
    {
        private Thread mScanThread;
        private RPCS3Bindings mBindings;
        private IHook<Emulator_RunDelegate> mEmulatorRunHook;
        private P5Bindings mGameBindings;
        private IHook<ppu_initializeDelegate> mPPUInitializeHook;

        public void Start( IModLoaderV1 loader )
        {
#if DEBUG
            Debugger.Launch();
#endif

            //* Initialize application services
            Services.Register( typeof( IModLoader ), loader );
            Services.Register( typeof( ILogger ), new ConsoleLogger() );
            loader.GetController<Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks>().TryGetTarget( out var hooks );
            Services.Register( typeof( IReloadedHooks ), hooks );

            mBindings = new RPCS3Bindings( Process.GetCurrentProcess() );
            mBindings.Activate();

            mEmulatorRunHook = mBindings.Emulator_Run.Hook( EmulatorRunImpl );
            mEmulatorRunHook.Activate();

            mPPUInitializeHook = mBindings.ppu_initialize.Hook( PPUInitializeImpl );
            mPPUInitializeHook.Activate();

            mGameBindings = new P5Bindings( mBindings );

            /* Your mod code starts here. */
            Services.Get<ILogger>().WriteLine( "Hello World from rpcs3.test" );
        }

        private void PPUInitializeImpl()
        {
            mPPUInitializeHook.OriginalFunction();

            mGameBindings.Initialize();
            mGameBindings.Activate();
        }

        private void EmulatorRunImpl( Emulator* pThis )
        {
            mEmulatorRunHook.OriginalFunction( pThis );
            if ( pThis->m_state != system_state.running )
                return;
        }

        /* Mod loader actions. */
        public void Suspend()
        {
            /*  Some tips if you wish to support this (CanSuspend == true)
             
                A. Undo memory modifications.
                B. Deactivate hooks. (Reloaded.Hooks Supports This!)
            */
            SuspendCore();
            mBindings.Suspend();
            mGameBindings.Suspend();
        }

        private void SuspendCore()
        {

        }

        public void Resume()
        {
            /*  Some tips if you wish to support this (CanSuspend == true)
             
                A. Redo memory modifications.
                B. Re-activate hooks. (Reloaded.Hooks Supports This!)
            */
            mBindings.Resume();
            mGameBindings.Resume();
        }

        public void Unload()
        {
            /*  Some tips if you wish to support this (CanUnload == true).
             
                A. Execute Suspend(). [Suspend should be reusable in this method]
                B. Release any unmanaged resources, e.g. Native memory.
            */
            SuspendCore();
            mBindings.Unload();
            mGameBindings.Unload();
        }

        /*  If CanSuspend == false, suspend and resume button are disabled in Launcher and Suspend()/Resume() will never be called.
            If CanUnload == false, unload button is disabled in Launcher and Unload() will never be called.
        */
        public bool CanUnload() => true;
        public bool CanSuspend() => false;

        /* Automatically called by the mod loader when the mod is about to be unloaded. */
        public Action Disposing { get; }

        /* Contains the Types you would like to share with other mods.
           If you do not want to share any types, please remove this method and the
           IExports interface.
        
           Inter Mod Communication: https://github.com/Reloaded-Project/Reloaded-II/blob/master/Docs/InterModCommunication.md
        */
        public Type[] GetTypes() => new Type[0];
    }
}
