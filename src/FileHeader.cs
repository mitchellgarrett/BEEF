﻿using System;

namespace FTG.Studios.BEEF
{

	public enum Endianness : byte { Little = 1, Big = 2 };

	/// <summary>
	/// Size: 16 bytes.
	/// </summary>
	public struct FileHeader
	{
		// Magic number is flipped so that it will be written as beef in little endian order
		public const UInt16 MAGIC_NUMBER = 0xEFBE;
		public const int SizeInBytes = 16;

		public UInt16 HeaderBegin;
		public byte Architecture;
		public Endianness Endianness;
		public UInt32 EntryPoint;
		public UInt32 SectionTableOffset;
		public UInt16 SectionCount;
		public UInt16 HeaderEnd;

		public static void Serialize(FileHeader header, System.IO.BinaryWriter writer)
		{
			writer.Write(header.HeaderBegin);
			writer.Write(header.Architecture);
			writer.Write((byte)header.Endianness);
			writer.Write(header.EntryPoint);
			writer.Write(header.SectionTableOffset);
			writer.Write(header.SectionCount);
			writer.Write(header.HeaderEnd);
		}

		public static FileHeader Deserialize(System.IO.BinaryReader reader)
		{
			FileHeader header = new FileHeader();

			header.HeaderBegin = reader.ReadUInt16();
			header.Architecture = reader.ReadByte();
			header.Endianness = (Endianness)reader.ReadByte();
			header.EntryPoint = reader.ReadUInt32();
			header.SectionTableOffset = reader.ReadUInt32();
			header.SectionCount = reader.ReadUInt16();
			header.HeaderEnd = reader.ReadUInt16();

			return header;
		}

		public override string ToString()
		{
			return $"Magic Number:         0x{HeaderBegin:x4}\n" +
					$"Architecture:         0x{Architecture:x2}\n" +
					$"Endianness:           {(byte)Endianness} ({Endianness})\n" +
					$"Entry Point:          0x{EntryPoint:x8}\n" +
					$"Section Table Offset: 0x{SectionTableOffset:x8}\n" +
					$"Section count:        {SectionCount}\n" +
					$"Magic Number:         0x{HeaderEnd:x4}";
		}
	}
}
