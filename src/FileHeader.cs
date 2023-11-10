using System;

namespace FTG.Studios.BEEF {

	public enum Endianness : byte { Little = 1, Big = 2 };

    public struct FileHeader {
        public const UInt16 MAGIC_NUMBER = 0xBEEF;

        public UInt16     MagicNumber;
        public byte       Architecture;
        public Endianness Endianness;
        public UInt32     EntryPoint;
        public UInt32     SectionTableOffset;
        public UInt16     SectionCount;
		
		public static void Serialize(FileHeader header, System.IO.BinaryWriter writer) {
			writer.Write(header.MagicNumber);
			writer.Write(header.Architecture);
			writer.Write((byte)header.Endianness);
			writer.Write(header.EntryPoint);
			writer.Write(header.SectionTableOffset);
			writer.Write(header.SectionCount);
		}
		
		public static FileHeader Deserialize(System.IO.BinaryReader reader) {
			FileHeader header = new FileHeader();
			
			header.MagicNumber = reader.ReadUInt16();
			header.Architecture = reader.ReadByte();
			header.Endianness = (Endianness)reader.ReadByte();
			header.EntryPoint = reader.ReadUInt32();
			header.SectionTableOffset = reader.ReadUInt32();
			header.SectionCount = reader.ReadUInt16();
			
			return header;
		}
		
		public override string ToString() {
			return $"Magic Number:         0x{MagicNumber:x4}\n" +
					$"Architecture:         0x{Architecture:x2}\n" +
					$"Endianness:           {(byte)Endianness} ({Endianness})\n" +
					$"Entry Point:          0x{EntryPoint:x8}\n" +
					$"Section Table Offset: 0x{SectionTableOffset:x8}\n" +
					$"Section count:        {SectionCount}";
		}
    }
}
