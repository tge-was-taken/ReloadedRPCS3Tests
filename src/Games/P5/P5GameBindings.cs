using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Memory.Sources;
using Reloaded.Mod.Interfaces;
using ReloadedRPCS3TestsNoConfig.Interop;
using ReloadedRPCS3TestsNoConfig.RPCS3;
using ReloadedRPCS3TestsNoConfig.RPCS3.Types;

namespace ReloadedRPCS3TestsNoConfig.Games.P5
{
    public unsafe delegate int sndManGetBgmDelegate();

    public unsafe class P5Bindings : GameBindings
    {
        private ILogger mLogger;
        private IHook<PPUFunctionDelegate> mAppUpdateExHook;
        private IHook<PPUFunctionDelegate> mTaskProcFldMainHook;
        private Thread mInputThread;
        private bool mHandleInput;
        private PPUFunction mDummyFunction;

        public PPUFunction appCreate { get; private set; }
        public PPUFunction appInit { get; private set; }
        public PPUFunction appUpdateEx { get; private set; }
        public PPUFunction sndManGetBgm { get; private set; }
        public PPUFunction sndManPlayBgm { get; private set; }
        public sndManGetBgmDelegate _sndManGetBgm { get; private set; }
        public PPUFunction seqManTransition { get; private set; }
        public PPUFunction taskProc_fld_main { get; private set; }

        private IHook<PPUFunctionDelegate> mDummyFunctionHook;

        public P5Bindings( RPCS3Bindings bindings )
            : base( bindings )
        {
            mLogger = Services.Get<ILogger>();
            mInputThread = new Thread( InputThreadProc );
            mHandleInput = false;
        }

        private void InputThreadProc( object obj )
        {
            while ( true )
            {
                if ( Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Enter )
                {
                    mHandleInput = true;
                }

                Thread.Sleep( 500 );
            }
        }

        private double timer = 60f;
        private int updateCounter = 0;

        private unsafe void appUpdateExDetour( context_t* context )
        {
            //var deltaTime = context->fpr[1];
            //timer -= deltaTime;
            //if ( timer <= 0d )
            //{
            //    sndManGetBgm.GetWrapper()( context );
            //    context->r3 = ( ulong )( 0x2290 + ( updateCounter % 10 ) );
            //    sndManPlayBgm.GetWrapper()( context );
            //    //Bindings.VirtualMemory.Read( 0xCFF4C4, out int value );
            //    //value += 1;
            //    //Bindings.VirtualMemory.Write( 0xCFF4C4, ref value );

            //    timer = 20f;
            //}

            ++updateCounter;


            if ( mHandleInput && context->state == 0 )
            {
                //var tempContext = *context;
                //tempContext.r3 = ( ulong )( 0x2BC );
                //sndManPlayBgm.GetWrapper()( &tempContext );
                //context->r3 = 0x2BC;
                //sndManGetBgm.GetWrapper()( context );
                // TODO: maybe try setting the CIA & LR, invalidating the status (non zero) and then calling the function
                // in the hopes it gets scheduled...?
                //var tempContext = *context;
                //context->r3 = 1;
                //context->r4 = 0;
                //context->r5 = 0;
                //context->r6 = 8;
                //context->cia = 0x10DB4;
                //seqManTransition.GetWrapper()( context );
                //*context = tempContext;

                var bgm = CallFunction(sndManGetBgm, context);
                mLogger.WriteLine( $"bgm = {bgm:X8}" );

                CallFunction( sndManPlayBgm, context, ( context ) =>
                {
                    context.Value->r3 = 0x12C;
                    //ontext.Value->r3 = bgm + 1;
                } );

                //CallFunction( seqManTransition, context, ( context ) =>
                // {
                //     context.Value->r3 = 1;
                //     context.Value->r4 = 0;
                //     context.Value->r5 = 0;
                //     context.Value->r6 = 8;
                // } );

                mHandleInput = false;
            }

            mAppUpdateExHook.OriginalFunction( context );
        }

        public override void Initialize()
        {
            //* Register named functions
            mDummyFunction = RegisterPPUFunction( 0xB43518 );
            appCreate = RegisterPPUFunction( 0x901B10, "appCreate" );
            appInit = RegisterPPUFunction( 0x10488, "appInit" );
            appUpdateEx = RegisterPPUFunction( 0x9209B0, "appUpdateEx" );
            sndManGetBgm = RegisterPPUFunction( 0x6CCB8, "sndManGetBgm" );
            sndManPlayBgm = RegisterPPUFunction( 0x6CF04, "sndManPlayBgm" );
            seqManTransition = RegisterPPUFunction( 0x10DB4, "seqManTransition" );
            taskProc_fld_main = RegisterPPUFunction( 0x29BF68 );

            //* Hook some functions
            mDummyFunctionHook = mDummyFunction.Hook( DummyFunctionDetour );
            mAppUpdateExHook = appUpdateEx.Hook( appUpdateExDetour );
            mTaskProcFldMainHook = taskProc_fld_main.Hook( taskProc_fld_mainDetour );
        }

        private void DummyFunctionDetour( context_t* context )
        {
            // Used to keep track of function execution
            context->cia = 0xDEADBABE;
        }

        private void taskProc_fld_mainDetour( context_t* context )
        {
            mTaskProcFldMainHook.OriginalFunction( context );
        }

        private static int OffsetOf<T, T2>( T* objPtr, T2* fieldPtr ) 
            where T : unmanaged
            where T2 : unmanaged
        {
            return ( int )( ( long )fieldPtr - ( long )objPtr );
        }

        private static int OffsetOf<T, T2>(ref T obj, ref T2 field) 
            where T : unmanaged
            where T2 : unmanaged
        {
            var objPtr = Unsafe.AsPointer(ref obj);
            var fieldPtr = Unsafe.AsPointer(ref field);
            return OffsetOf( ( T* )objPtr, ( T2* )fieldPtr );
        }

        public static void Copy<T, T2, T3>(T* obj, T2* field, T3* destination)
            where T : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
        {
            var size = Unsafe.SizeOf<T>();
            var length = (uint)(size - OffsetOf(obj, field));
            Unsafe.CopyBlock( ( void* )field, ( void* )destination, length );
        }

        //private ulong CallFunction( PPUFunction function, context_t* context, Action<Ptr<context_t>> setArgsAction = default)
        //{
        //    // Save context
        //    var originalContext = *context;

        //    if ( setArgsAction != default )
        //        setArgsAction( context );

        //    // Set return address to dummy function address
        //    context->cia = 0;
        //    context->lr = mDummyFunction.VirtualAddress;
        //    IFunction<PPUFunctionDelegate> currentFunction = function;
        //    var currentFunctionAddress = function.VirtualAddress;

        //    while ( true )
        //    {
        //        // Call function (which may or may not run)
        //        context->cia = 0;
        //        currentFunction.GetWrapper()( context );

        //        if ( context->cia == currentFunctionAddress )
        //        {
        //            // Function hasn't actually run (blocked by __check which sets the cia)
        //        }
        //        else if ( context->cia != mDummyFunction.VirtualAddress )
        //        {
        //            // Function was actually run (not blocked by __check), but still has more functions to execute
        //            // Get next function in call chain
        //            currentFunctionAddress = ( uint )context->lr;
        //            currentFunction = Bindings.PPUFunctions[currentFunctionAddress];
        //        }
        //        else if ( context->cia == mDummyFunction.VirtualAddress )
        //        {
        //            // Assume function was actually executed if cia is now the previously set return address
        //            break;
        //        }
        //        else
        //        {
        //            throw new NotImplementedException();
        //        }
        //    }

        //    // Save return value
        //    var returnValue = context->r3;

        //    // Restore registers
        //    Copy( context, context->gpr, originalContext.gpr );

        //    return returnValue;
        //}

        private ulong CallFunction( PPUFunction function, context_t* context, Action<Ptr<context_t>> setArgsAction = default )
        {
            // Save context
            var originalContext = *context;

            if ( setArgsAction != default )
                setArgsAction( context );

            context->lr = mDummyFunction.VirtualAddress;
            var currentFunction = function;

            while ( true )
            {
                // Call function (which may or may not run)
                context->cia = 0;
                currentFunction.GetWrapper()( context );

                // Check if our sentinel exit function has been run
                if ( context->cia == 0xDEADBABE )
                    break;

                if ( context->lr != mDummyFunction.VirtualAddress )
                {
                    // Call next function in chain
                    currentFunction = Bindings.PPUFunctions[( uint )context->lr];
                }
            }

            // Save return value
            var returnValue = context->r3;

            // Restore registers
            Copy( context, context->gpr, originalContext.gpr );

            return returnValue;
        }

        public override void Activate()
        {
            mAppUpdateExHook.Activate();
            mTaskProcFldMainHook.Activate();
            mDummyFunctionHook.Activate();

            if ( !mInputThread.IsAlive )
                mInputThread.Start();
        }

        public override void Resume()
        {
            mAppUpdateExHook.Enable();
        }

        public override void Suspend()
        {
            mAppUpdateExHook.Disable();
        }

        public override void Unload()
        {
            Suspend();
        }
    }
}
