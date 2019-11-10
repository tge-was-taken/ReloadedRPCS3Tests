using ReloadedRPCS3TestsNoConfig.Interop.CPP;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ReloadedRPCS3TestsNoConfig.RPCS3.Types
{
    // 
    // Types from rps3/Emu/Cell/PPUTranslator.h + cpp
    //
    // https://github.com/RPCS3/rpcs3/blob/ec1ea466fd6fe65e4934a4343bbcd9685ee08274/rpcs3/Emu/Cell/PPUTranslator.h
    // https://github.com/RPCS3/rpcs3/blob/ec1ea466fd6fe65e4934a4343bbcd9685ee08274/rpcs3/Emu/Cell/PPUTranslator.cpp

    // Based on https://github.com/RPCS3/rpcs3/blob/ec1ea466fd6fe65e4934a4343bbcd9685ee08274/rpcs3/Emu/Cell/PPUTranslator.cpp#L48
    public unsafe struct context_t
    {
        /* 000 */ public fixed byte reserved1[28];
        /* 01c */ public cpu_flag state;
        /* 020 */ public fixed ulong gpr[32];
        /* 120 */ public fixed double fpr[32]; 
        /* 220 */ public fixed uint vr[32 * 4];
        /* 420 */ public fixed byte cr[32];
        /* 440 */ public fixed byte fpscr[32]; 
        /* 460 */ public ulong lr; 
        /* 468 */ public ulong ctr;
        /* 470 */ public uint vrsave; 
        /* 474 */ public uint cia;
        /* 478 */ public byte so;
        /* 479 */ public byte ov;
        /* 47A */ public byte ca;
        /* 47B */ public byte cnt;
        /* 47C */ public byte sat;
        /* 47D */ public byte nj;

        // Helper properties
        #region GPR
        public ulong r0 { get => gpr[0]; set => gpr[0] = value; }
        public ulong r1 { get => gpr[1]; set => gpr[1] = value; }
        public ulong r2 { get => gpr[2]; set => gpr[2] = value; }
        public ulong r3 { get => gpr[3]; set => gpr[3] = value; }
        public ulong r4 { get => gpr[4]; set => gpr[4] = value; }
        public ulong r5 { get => gpr[5]; set => gpr[5] = value; }
        public ulong r6 { get => gpr[6]; set => gpr[6] = value; }
        public ulong r7 { get => gpr[7]; set => gpr[7] = value; }
        public ulong r8 { get => gpr[8]; set => gpr[8] = value; }
        public ulong r9 { get => gpr[9]; set => gpr[9] = value; }
        public ulong r10 { get => gpr[10]; set => gpr[10] = value; }
        public ulong r11 { get => gpr[11]; set => gpr[11] = value; }
        public ulong r12 { get => gpr[12]; set => gpr[12] = value; }
        public ulong r13 { get => gpr[13]; set => gpr[13] = value; }
        public ulong r14 { get => gpr[14]; set => gpr[14] = value; }
        public ulong r15 { get => gpr[15]; set => gpr[15] = value; }
        public ulong r16 { get => gpr[16]; set => gpr[16] = value; }
        public ulong r17 { get => gpr[17]; set => gpr[17] = value; }
        public ulong r18 { get => gpr[18]; set => gpr[18] = value; }
        public ulong r19 { get => gpr[19]; set => gpr[19] = value; }
        public ulong r20 { get => gpr[20]; set => gpr[20] = value; }
        public ulong r21 { get => gpr[21]; set => gpr[21] = value; }
        public ulong r22 { get => gpr[22]; set => gpr[22] = value; }
        public ulong r23 { get => gpr[23]; set => gpr[23] = value; }
        public ulong r24 { get => gpr[24]; set => gpr[24] = value; }
        public ulong r25 { get => gpr[25]; set => gpr[25] = value; }
        public ulong r26 { get => gpr[26]; set => gpr[26] = value; }
        public ulong r27 { get => gpr[27]; set => gpr[27] = value; }
        public ulong r28 { get => gpr[28]; set => gpr[28] = value; }
        public ulong r29 { get => gpr[29]; set => gpr[29] = value; }
        public ulong r30 { get => gpr[30]; set => gpr[30] = value; }
        public ulong r31 { get => gpr[31]; set => gpr[31] = value; }
        #endregion

        #region FPR
        public double f0 { get => fpr[0]; set => fpr[0] = value; }
        public double f1 { get => fpr[1]; set => fpr[1] = value; }
        public double f2 { get => fpr[2]; set => fpr[2] = value; }
        public double f3 { get => fpr[3]; set => fpr[3] = value; }
        public double f4 { get => fpr[4]; set => fpr[4] = value; }
        public double f5 { get => fpr[5]; set => fpr[5] = value; }
        public double f6 { get => fpr[6]; set => fpr[6] = value; }
        public double f7 { get => fpr[7]; set => fpr[7] = value; }
        public double f8 { get => fpr[8]; set => fpr[8] = value; }
        public double f9 { get => fpr[9]; set => fpr[9] = value; }
        public double f10 { get => fpr[10]; set => fpr[10] = value; }
        public double f11 { get => fpr[11]; set => fpr[11] = value; }
        public double f12 { get => fpr[12]; set => fpr[12] = value; }
        public double f13 { get => fpr[13]; set => fpr[13] = value; }
        public double f14 { get => fpr[14]; set => fpr[14] = value; }
        public double f15 { get => fpr[15]; set => fpr[15] = value; }
        public double f16 { get => fpr[16]; set => fpr[16] = value; }
        public double f17 { get => fpr[17]; set => fpr[17] = value; }
        public double f18 { get => fpr[18]; set => fpr[18] = value; }
        public double f19 { get => fpr[19]; set => fpr[19] = value; }
        public double f20 { get => fpr[20]; set => fpr[20] = value; }
        public double f21 { get => fpr[21]; set => fpr[21] = value; }
        public double f22 { get => fpr[22]; set => fpr[22] = value; }
        public double f23 { get => fpr[23]; set => fpr[23] = value; }
        public double f24 { get => fpr[24]; set => fpr[24] = value; }
        public double f25 { get => fpr[25]; set => fpr[25] = value; }
        public double f26 { get => fpr[26]; set => fpr[26] = value; }
        public double f27 { get => fpr[27]; set => fpr[27] = value; }
        public double f28 { get => fpr[28]; set => fpr[28] = value; }
        public double f29 { get => fpr[29]; set => fpr[29] = value; }
        public double f30 { get => fpr[30]; set => fpr[30] = value; }
        public double f31 { get => fpr[31]; set => fpr[31] = value; }
        #endregion

        #region VR
        public vr128 vr0 { get => new vr128( vr[0], vr[1], vr[2], vr[3] ); set { vr[0] = value.x; vr[1] = value.y; vr[2] = value.z; vr[3] = value.w; } }
        public vr128 vr1 { get => new vr128( vr[4], vr[5], vr[6], vr[7] ); set { vr[4] = value.x; vr[5] = value.y; vr[6] = value.z; vr[7] = value.w; } }
        public vr128 vr2 { get => new vr128( vr[8], vr[9], vr[10], vr[11] ); set { vr[8] = value.x; vr[9] = value.y; vr[10] = value.z; vr[11] = value.w; } }
        public vr128 vr3 { get => new vr128( vr[12], vr[13], vr[14], vr[15] ); set { vr[12] = value.x; vr[13] = value.y; vr[14] = value.z; vr[15] = value.w; } }
        public vr128 vr4 { get => new vr128( vr[16], vr[17], vr[18], vr[19] ); set { vr[16] = value.x; vr[17] = value.y; vr[18] = value.z; vr[19] = value.w; } }
        public vr128 vr5 { get => new vr128( vr[20], vr[21], vr[22], vr[23] ); set { vr[20] = value.x; vr[21] = value.y; vr[22] = value.z; vr[23] = value.w; } }
        public vr128 vr6 { get => new vr128( vr[24], vr[25], vr[26], vr[27] ); set { vr[24] = value.x; vr[25] = value.y; vr[26] = value.z; vr[27] = value.w; } }
        public vr128 vr7 { get => new vr128( vr[28], vr[29], vr[30], vr[31] ); set { vr[28] = value.x; vr[29] = value.y; vr[30] = value.z; vr[31] = value.w; } }
        public vr128 vr8 { get => new vr128( vr[32], vr[33], vr[34], vr[35] ); set { vr[32] = value.x; vr[33] = value.y; vr[34] = value.z; vr[35] = value.w; } }
        public vr128 vr9 { get => new vr128( vr[36], vr[37], vr[38], vr[39] ); set { vr[36] = value.x; vr[37] = value.y; vr[38] = value.z; vr[39] = value.w; } }
        public vr128 vr10 { get => new vr128( vr[40], vr[41], vr[42], vr[43] ); set { vr[40] = value.x; vr[41] = value.y; vr[42] = value.z; vr[43] = value.w; } }
        public vr128 vr11 { get => new vr128( vr[44], vr[45], vr[46], vr[47] ); set { vr[44] = value.x; vr[45] = value.y; vr[46] = value.z; vr[47] = value.w; } }
        public vr128 vr12 { get => new vr128( vr[48], vr[49], vr[50], vr[51] ); set { vr[48] = value.x; vr[49] = value.y; vr[50] = value.z; vr[51] = value.w; } }
        public vr128 vr13 { get => new vr128( vr[52], vr[53], vr[54], vr[55] ); set { vr[52] = value.x; vr[53] = value.y; vr[54] = value.z; vr[55] = value.w; } }
        public vr128 vr14 { get => new vr128( vr[56], vr[57], vr[58], vr[59] ); set { vr[56] = value.x; vr[57] = value.y; vr[58] = value.z; vr[59] = value.w; } }
        public vr128 vr15 { get => new vr128( vr[60], vr[61], vr[62], vr[63] ); set { vr[60] = value.x; vr[61] = value.y; vr[62] = value.z; vr[63] = value.w; } }
        public vr128 vr16 { get => new vr128( vr[64], vr[65], vr[66], vr[67] ); set { vr[64] = value.x; vr[65] = value.y; vr[66] = value.z; vr[67] = value.w; } }
        public vr128 vr17 { get => new vr128( vr[68], vr[69], vr[70], vr[71] ); set { vr[68] = value.x; vr[69] = value.y; vr[70] = value.z; vr[71] = value.w; } }
        public vr128 vr18 { get => new vr128( vr[72], vr[73], vr[74], vr[75] ); set { vr[72] = value.x; vr[73] = value.y; vr[74] = value.z; vr[75] = value.w; } }
        public vr128 vr19 { get => new vr128( vr[76], vr[77], vr[78], vr[79] ); set { vr[76] = value.x; vr[77] = value.y; vr[78] = value.z; vr[79] = value.w; } }
        public vr128 vr20 { get => new vr128( vr[80], vr[81], vr[82], vr[83] ); set { vr[80] = value.x; vr[81] = value.y; vr[82] = value.z; vr[83] = value.w; } }
        public vr128 vr21 { get => new vr128( vr[84], vr[85], vr[86], vr[87] ); set { vr[84] = value.x; vr[85] = value.y; vr[86] = value.z; vr[87] = value.w; } }
        public vr128 vr22 { get => new vr128( vr[88], vr[89], vr[90], vr[91] ); set { vr[88] = value.x; vr[89] = value.y; vr[90] = value.z; vr[91] = value.w; } }
        public vr128 vr23 { get => new vr128( vr[92], vr[93], vr[94], vr[95] ); set { vr[92] = value.x; vr[93] = value.y; vr[94] = value.z; vr[95] = value.w; } }
        public vr128 vr24 { get => new vr128( vr[96], vr[97], vr[98], vr[99] ); set { vr[96] = value.x; vr[97] = value.y; vr[98] = value.z; vr[99] = value.w; } }
        public vr128 vr25 { get => new vr128( vr[100], vr[101], vr[102], vr[103] ); set { vr[100] = value.x; vr[101] = value.y; vr[102] = value.z; vr[103] = value.w; } }
        public vr128 vr26 { get => new vr128( vr[104], vr[105], vr[106], vr[107] ); set { vr[104] = value.x; vr[105] = value.y; vr[106] = value.z; vr[107] = value.w; } }
        public vr128 vr27 { get => new vr128( vr[108], vr[109], vr[110], vr[111] ); set { vr[108] = value.x; vr[109] = value.y; vr[110] = value.z; vr[111] = value.w; } }
        public vr128 vr28 { get => new vr128( vr[112], vr[113], vr[114], vr[115] ); set { vr[112] = value.x; vr[113] = value.y; vr[114] = value.z; vr[115] = value.w; } }
        public vr128 vr29 { get => new vr128( vr[116], vr[117], vr[118], vr[119] ); set { vr[116] = value.x; vr[117] = value.y; vr[118] = value.z; vr[119] = value.w; } }
        public vr128 vr30 { get => new vr128( vr[120], vr[121], vr[122], vr[123] ); set { vr[120] = value.x; vr[121] = value.y; vr[122] = value.z; vr[123] = value.w; } }
        public vr128 vr31 { get => new vr128( vr[124], vr[125], vr[126], vr[127] ); set { vr[124] = value.x; vr[125] = value.y; vr[126] = value.z; vr[127] = value.w; } }
        #endregion

        #region CR
        public byte cr0 { get => cr[0]; set => cr[0] = value; }
        public byte cr1 { get => cr[1]; set => cr[1] = value; }
        public byte cr2 { get => cr[2]; set => cr[2] = value; }
        public byte cr3 { get => cr[3]; set => cr[3] = value; }
        public byte cr4 { get => cr[4]; set => cr[4] = value; }
        public byte cr5 { get => cr[5]; set => cr[5] = value; }
        public byte cr6 { get => cr[6]; set => cr[6] = value; }
        public byte cr7 { get => cr[7]; set => cr[7] = value; }
        public byte cr8 { get => cr[8]; set => cr[8] = value; }
        public byte cr9 { get => cr[9]; set => cr[9] = value; }
        public byte cr10 { get => cr[10]; set => cr[10] = value; }
        public byte cr11 { get => cr[11]; set => cr[11] = value; }
        public byte cr12 { get => cr[12]; set => cr[12] = value; }
        public byte cr13 { get => cr[13]; set => cr[13] = value; }
        public byte cr14 { get => cr[14]; set => cr[14] = value; }
        public byte cr15 { get => cr[15]; set => cr[15] = value; }
        public byte cr16 { get => cr[16]; set => cr[16] = value; }
        public byte cr17 { get => cr[17]; set => cr[17] = value; }
        public byte cr18 { get => cr[18]; set => cr[18] = value; }
        public byte cr19 { get => cr[19]; set => cr[19] = value; }
        public byte cr20 { get => cr[20]; set => cr[20] = value; }
        public byte cr21 { get => cr[21]; set => cr[21] = value; }
        public byte cr22 { get => cr[22]; set => cr[22] = value; }
        public byte cr23 { get => cr[23]; set => cr[23] = value; }
        public byte cr24 { get => cr[24]; set => cr[24] = value; }
        public byte cr25 { get => cr[25]; set => cr[25] = value; }
        public byte cr26 { get => cr[26]; set => cr[26] = value; }
        public byte cr27 { get => cr[27]; set => cr[27] = value; }
        public byte cr28 { get => cr[28]; set => cr[28] = value; }
        public byte cr29 { get => cr[29]; set => cr[29] = value; }
        public byte cr30 { get => cr[30]; set => cr[30] = value; }
        public byte cr31 { get => cr[31]; set => cr[31] = value; }
        #endregion

        #region FPSCR
        public byte fpscr0 { get => fpscr[0]; set => fpscr[0] = value; }
        public byte fpscr1 { get => fpscr[1]; set => fpscr[1] = value; }
        public byte fpscr2 { get => fpscr[2]; set => fpscr[2] = value; }
        public byte fpscr3 { get => fpscr[3]; set => fpscr[3] = value; }
        public byte fpscr4 { get => fpscr[4]; set => fpscr[4] = value; }
        public byte fpscr5 { get => fpscr[5]; set => fpscr[5] = value; }
        public byte fpscr6 { get => fpscr[6]; set => fpscr[6] = value; }
        public byte fpscr7 { get => fpscr[7]; set => fpscr[7] = value; }
        public byte fpscr8 { get => fpscr[8]; set => fpscr[8] = value; }
        public byte fpscr9 { get => fpscr[9]; set => fpscr[9] = value; }
        public byte fpscr10 { get => fpscr[10]; set => fpscr[10] = value; }
        public byte fpscr11 { get => fpscr[11]; set => fpscr[11] = value; }
        public byte fpscr12 { get => fpscr[12]; set => fpscr[12] = value; }
        public byte fpscr13 { get => fpscr[13]; set => fpscr[13] = value; }
        public byte fpscr14 { get => fpscr[14]; set => fpscr[14] = value; }
        public byte fpscr15 { get => fpscr[15]; set => fpscr[15] = value; }
        public byte fpscr16 { get => fpscr[16]; set => fpscr[16] = value; }
        public byte fpscr17 { get => fpscr[17]; set => fpscr[17] = value; }
        public byte fpscr18 { get => fpscr[18]; set => fpscr[18] = value; }
        public byte fpscr19 { get => fpscr[19]; set => fpscr[19] = value; }
        public byte fpscr20 { get => fpscr[20]; set => fpscr[20] = value; }
        public byte fpscr21 { get => fpscr[21]; set => fpscr[21] = value; }
        public byte fpscr22 { get => fpscr[22]; set => fpscr[22] = value; }
        public byte fpscr23 { get => fpscr[23]; set => fpscr[23] = value; }
        public byte fpscr24 { get => fpscr[24]; set => fpscr[24] = value; }
        public byte fpscr25 { get => fpscr[25]; set => fpscr[25] = value; }
        public byte fpscr26 { get => fpscr[26]; set => fpscr[26] = value; }
        public byte fpscr27 { get => fpscr[27]; set => fpscr[27] = value; }
        public byte fpscr28 { get => fpscr[28]; set => fpscr[28] = value; }
        public byte fpscr29 { get => fpscr[29]; set => fpscr[29] = value; }
        public byte fpscr30 { get => fpscr[30]; set => fpscr[30] = value; }
        public byte fpscr31 { get => fpscr[31]; set => fpscr[31] = value; }
        #endregion
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct vr128
    {
        [FieldOffset(0)] public uint x;
        [FieldOffset(0)] public float xf;
        [FieldOffset(4)] public uint y;
        [FieldOffset(4)] public float yf;
        [FieldOffset(8)] public uint z;
        [FieldOffset(8)] public float zf;
        [FieldOffset(12)] public uint w;
        [FieldOffset(12)] public float wf;

        public vr128(uint x, uint y, uint z, uint w)
        {
            this.xf = 0;
            this.yf = 0;
            this.zf = 0;
            this.wf = 0;
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override string ToString()
        {
            return $"{xf} {yf} {zf} {wf} ({x} {y} {z} {w})";
        }
    }
}
