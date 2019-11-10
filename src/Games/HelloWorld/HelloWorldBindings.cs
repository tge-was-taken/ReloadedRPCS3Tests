using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.X64;
using ReloadedRPCS3TestsNoConfig.RPCS3;
using ReloadedRPCS3TestsNoConfig.RPCS3.Types;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ReloadedRPCS3TestsNoConfig.Games.HelloWorld
{
    public unsafe class HelloWorldBindings : GameBindings
    {
        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        [Function( CallingConventions.Microsoft )]
        public delegate int __0x16c2cDelegate( context_t* context );

        public IHook<__0x16c2cDelegate> __0x16c2cHook;

        public HelloWorldBindings( RPCS3Bindings bindings )
            : base( bindings )
        {

        }

        public override void Activate()
        {

        }

        public override void Resume()
        {
        }

        public override void Suspend()
        {
        }

        public override void Unload()
        {
        }

        public override void Initialize()
        {
        }
    }
}
