using ReloadedRPCS3TestsNoConfig.Interop;
using ReloadedRPCS3TestsNoConfig.Interop.CPP;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReloadedRPCS3TestsNoConfig.RPCS3.Types
{
    //
    // Types from rpcs3/Emu/Cell/PPUThread.h + cpp
    //

    public struct jit_module
    {
        public StdVector<Ptr<ulong>> vars;
        public StdVector<ppu_function> funcs;
    };
}
