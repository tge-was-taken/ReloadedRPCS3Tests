using ReloadedRPCS3TestsNoConfig.Interop.CPP;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ReloadedRPCS3TestsNoConfig.RPCS3.Types
{
    //
    // Types from rpcs3/Emu/System.h+cpp
    //
    // https://github.com/RPCS3/rpcs3/blob/7475be99ab888ed38801e3a8e4565e81d9d8aa23/rpcs3/Emu/System.h
    // https://github.com/RPCS3/rpcs3/blob/7475be99ab888ed38801e3a8e4565e81d9d8aa23/rpcs3/Emu/System.cpp

    public enum system_state
    {
        running,
        paused,
        stopped,
        ready
    }

    public unsafe struct EmuCallbacks
    {
        public StdFunction call_after;
        public StdFunction on_run;
        public StdFunction on_pause;
        public StdFunction on_resume;
        public StdFunction on_stop;
        public StdFunction on_ready;
        public StdFunction exit;
        public StdFunction reset_pads;
        public StdFunction enable_pads;
        /// <summary>
        /// (type, value) type: 0 for reset, 1 for increment, 2 for set_limit.
        /// </summary>
        public StdFunction handle_taskbar_progress;
        public StdFunction init_kb_handler;
        public StdFunction init_mouse_handler;
        public StdFunction init_pad_handler;
        public StdFunction get_gs_frame;
        public StdFunction init_gs_render;
        public StdFunction get_audio;
        public StdFunction get_msg_dialog;
        public StdFunction get_osk_dialog;
        public StdFunction get_save_dialog;
        public StdFunction get_trophy_notification_dialog;
    }


    public unsafe struct Emulator
    {
        /* private */

        /* 000 */ public system_state m_state;
        public EmuCallbacks m_cb;
        /// <summary>
        /// Set when paused.
        /// </summary>
        /* 508 */ public ulong m_pause_start_time;   
        /// <summary>
        /// Increased when resumed.
        /// </summary>
        /* 510 */ public ulong m_pause_amend_time;    
        /* 518 */ public StdString m_path;    
        /* 538 */ public StdString m_path_old;
        public StdString m_title_id;
        public StdString m_title;
        public StdString m_cat;
        public StdString m_dir;
        public StdString m_sfo_dir;
        public StdString m_game_dir;
        public StdString m_usr;
        public uint m_usrid;
        public Bool m_force_boot;

        /* public */
        public StdVector<StdString> argv;
        public StdVector<StdString> envp;
        public StdVector<byte> data;
        public StdVector<byte> klic;
        public StdString disc;
    }
}
