using System;

namespace FTG.Studios.BEEF {

    public struct FileHeader {
        public const UInt32 MAGIC_NUMBER = 0xBEEF;

        public UInt32 MagicNumber;
        public byte   Architecture;
        public byte   Endianness;
        public UInt32 EntryPoint;
        public UInt32 SectionTableOffset;
        public UInt16 SectionCount;
    }
}
