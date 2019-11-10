using System;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using ReloadedRPCS3TestsNoConfig.RPCS3;

namespace ReloadedRPCS3TestsNoConfig.RPCS3
{
    public class PPUFunction : IFunction<PPUFunctionDelegate>
    {
        private readonly IFunction<PPUFunctionDelegate> mFunction;

        public uint VirtualAddress { get; }

        public long Address => mFunction.Address;

        public IReloadedHooks Hooks => mFunction.Hooks;

        public PPUFunction( IFunction<PPUFunctionDelegate> function, uint virtualAddress )
        {
            mFunction = function;
            VirtualAddress = virtualAddress;
        }

        public PPUFunctionDelegate GetWrapper( out IntPtr wrapperAddress )
        {
            return mFunction.GetWrapper( out wrapperAddress );
        }

        public PPUFunctionDelegate GetWrapper()
        {
            return mFunction.GetWrapper();
        }

        public IHook<PPUFunctionDelegate> Hook( PPUFunctionDelegate function, int minHookLength = -1 )
        {
            return mFunction.Hook( function, minHookLength );
        }

        public IAsmHook MakeAsmHook( string[] asmCode, AsmHookBehaviour behaviour = AsmHookBehaviour.ExecuteFirst, int hookLength = -1 )
        {
            return mFunction.MakeAsmHook( asmCode, behaviour, hookLength );
        }

        public IAsmHook MakeAsmHook( byte[] asmCode, AsmHookBehaviour behaviour = AsmHookBehaviour.ExecuteFirst, int hookLength = -1 )
        {
            return mFunction.MakeAsmHook( asmCode, behaviour, hookLength );
        }
    }
}
