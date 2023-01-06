using System;
using System.Runtime.InteropServices;

namespace FTG.Studios.BEEF {

    public enum SectionType : UInt16 { Null, Program, SymbolTable, StringTable, Debug };
    public enum SectionFlag : UInt16 { None = 0, Readable = 1, Writable = 2, Executable = 4, Code = 8, InitData = 16, UninitData = 32 };

    [StructLayout(LayoutKind.Sequential)]
    public struct SectionHeader {
        public SectionType Type;
        public UInt16 Flags;
        public UInt32 Offset;
        public UInt32 Address;
        public UInt32 Size;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string Name;
    }
}
