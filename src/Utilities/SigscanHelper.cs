using Reloaded.Hooks.Definitions;
using Reloaded.Memory.Sigscan;
using Reloaded.Memory.Sigscan.Structs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ReloadedRPCS3TestsNoConfig.Utilities
{
    public static unsafe class SigscanHelper
    {
        public static IFunction<T> CreateFunction<T>( Scanner scanner, string name, string pattern ) where T : Delegate
        {
            return CreateFunction<T>( scanner, Process.GetCurrentProcess().MainModule.BaseAddress, name, pattern );
        }

        public static IFunction<T> CreateFunction<T>( Scanner scanner, IntPtr baseAddress, string name, string pattern ) where T : Delegate
        {
            var searchResult = scanner.CompiledFindPattern( pattern );
            if ( !searchResult.Found ) throw new InvalidOperationException( $"Function \"{name}\" not found with pattern \"{pattern}\"" );
            return Services.Get<IReloadedHooks>().CreateFunction<T>( ( long )baseAddress + searchResult.Offset );
        }

        public static IFunction<T> CreateFunction<T>( string pattern )
        {
            var compiledPattern = new CompiledScanPattern( pattern );

            Vanara.PInvoke.Kernel32.MEMORY_BASIC_INFORMATION m;
            var process = Process.GetCurrentProcess();
            var address = (ulong)process.MainModule.BaseAddress;
            while ( Vanara.PInvoke.Kernel32.VirtualQueryEx( process, ( IntPtr )address, ( IntPtr )( &m ), ( uint )Marshal.SizeOf( typeof( Vanara.PInvoke.Kernel32.MEMORY_BASIC_INFORMATION ) ) ).Value > 0 )
            {
                if ( ( m.Protect & ( ( uint )Vanara.PInvoke.Kernel32.MEM_PROTECTION.PAGE_EXECUTE | ( uint )Vanara.PInvoke.Kernel32.MEM_PROTECTION.PAGE_EXECUTE_READ | 
                                     ( uint )Vanara.PInvoke.Kernel32.MEM_PROTECTION.PAGE_EXECUTE_READWRITE | ( uint )Vanara.PInvoke.Kernel32.MEM_PROTECTION.PAGE_EXECUTE_WRITECOPY ) ) != 0 )
                {
                    var scanner = new Scanner((byte*)m.BaseAddress, m.RegionSize);
                    var result = scanner.CompiledFindPattern( compiledPattern );

                    if ( result.Found )
                    {
                        return Services.Get<IReloadedHooks>().CreateFunction<T>( ( long )( m.BaseAddress + result.Offset ) );
                    }

                }

                address = ( ulong )m.BaseAddress + m.RegionSize.Value;
            }

            return null;
        }

        public static T CreateWrapper<T>( Scanner scanner, IntPtr baseAddress, string name, string pattern ) where T : Delegate
        {
            var searchResult = scanner.CompiledFindPattern( pattern );
            if ( !searchResult.Found ) throw new InvalidOperationException( $"Function \"{name}\" not found with pattern \"{pattern}\"" );
            return Services.Get<IReloadedHooks>().CreateWrapper<T>( ( long )baseAddress + searchResult.Offset, out _ );
        }

        public static IHook<T> CreateHook<T>( Scanner scanner, IntPtr baseAddress, T function, string name, string pattern ) where T : Delegate
        {
            var searchResult = scanner.CompiledFindPattern( pattern );
            if ( !searchResult.Found ) throw new InvalidOperationException( $"Function \"{name}\" not found with pattern \"{pattern}\"" );
            return Services.Get<IReloadedHooks>().CreateHook<T>( function, ( long )baseAddress + searchResult.Offset );
        }
    }
}
