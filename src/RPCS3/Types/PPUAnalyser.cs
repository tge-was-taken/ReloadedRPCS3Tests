using ReloadedRPCS3TestsNoConfig.Interop.CPP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ReloadedRPCS3TestsNoConfig.RPCS3.Types
{
    // 
    // Types from PPUAnalyser.h
    // https://github.com/RPCS3/rpcs3/blob/f3ed26e9dbbb5aa31c7ddb5fb1f252f22bfb78a0/rpcs3/Emu/Cell/PPUAnalyser.h
    //

    /// <summary>
    /// PPU Function Attributes
    /// </summary>
    [Flags]
    public enum ppu_attr : uint
    {
        known_addr,
        known_size,
        no_return,
        no_size,
        __bitset_enum_max
    }


    /// <summary>
    ///  PPU Function Information
    /// </summary>
    public struct ppu_function
    {
        public uint addr;
        public uint toc;
        public uint size;
        public ppu_attr attr;

        public uint stack_frame;
        public uint trampoline;

        public StdMap<uint, uint> blocks; // Basic blocks: addr -> size
        public StdSet<uint> calls; // Set of called functions
        public StdSet<uint> callers;

        public StdString name; // Function name
    };

    /// <summary>
    /// PPU Relocation Information
    /// </summary>
    public struct ppu_reloc
    {
        public uint addr;
        public uint type;
        public ulong data;
    }

    /// <summary>
    /// PPU Segment Information
    /// </summary>
    public struct ppu_segment
    {
        public uint addr;
        public uint size;
        public uint type;
        public uint flags;
        public uint filesz;
    }

    /// <summary>
    /// PPU Module Information  
    /// </summary>
    public unsafe struct ppu_module
    {
        public fixed byte sha1[20];
        public StdString name;
        public StdString path;
        public StdString cache;
        public StdVector<ppu_reloc> relocs;
        public StdVector<ppu_segment> segs;
        public StdVector<ppu_segment> secs;
        public StdVector<ppu_function> funcs;
    }
}
