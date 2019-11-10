using System;
using System.Collections.Generic;
using System.Text;

namespace ReloadedRPCS3TestsNoConfig.RPCS3.Types
{
    // 
    // Types from CPUThread.h
    // https://github.com/RPCS3/rpcs3/blob/ec1ea466fd6fe65e4934a4343bbcd9685ee08274/rpcs3/Emu/CPU/CPUThread.h
    //

    [Flags]
    public enum cpu_flag : uint
    {
        /// <summary>
        /// Thread not running (HLE, initial state)
        /// </summary>
        stop = 1 << 0, // 
        /// <summary>
        /// Irreversible exit
        /// </summary>
        exit = 1 << 1, // 
        /// <summary>
        /// Indicates waiting state, set by the thread itself
        /// </summary>
        wait = 1 << 2, // 
        /// <summary>
        /// Thread suspended by suspend_all technique
        /// </summary>
        pause = 1 << 3, // 
        /// <summary>
        /// Thread suspended
        /// </summary>
        suspend = 1 << 4, // 
        /// <summary>
        /// Callback return requested
        /// </summary>
        ret = 1 << 5, //
        /// <summary>
        /// // Thread received a signal (HLE)
        /// </summary>
        signal = 1 << 6,
        /// <summary>
        /// Thread must unlock memory mutex
        /// </summary>
        memory = 1 << 7,

        /// <summary>
        ///  JIT compiler event (forced return)
        /// </summary>
        jit_return = 1 << 8,
        /// <summary>
        /// Emulation paused
        /// </summary>
        dbg_global_pause = 1 << 9,
        /// <summary>
        ///  Emulation stopped
        /// </summary>
        dbg_global_stop = 1 << 10,
        /// <summary>
        /// Thread paused
        /// </summary>
        dbg_pause = 1 << 11,
        /// <summary>
        ///  Thread forced to pause after one step (one instruction, etc)
        /// </summary>
        dbg_step = 1 << 12,
    }

    public unsafe struct cpu_thread
    {
        public void* vtbl;
        public fixed byte dummy[8];
        public ulong block_hash;
        public uint id;
        public cpu_flag state;
    }
}
