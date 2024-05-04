using System;
using System.Runtime.InteropServices;

namespace FTG.Studios.BEEF
{

	public enum SectionType : UInt16 { Null, Program, SymbolTable, StringTable, RelocationTable, Debug };
	public enum SectionFlag : UInt16 { None = 0, Readable = 1, Writable = 2, Executable = 4, Code = 8, InitData = 16, UninitData = 32 };

	[StructLayout(LayoutKind.Sequential)]
	public struct SectionHeader
	{
		public SectionType Type;
		public SectionFlag Flags;
		public UInt32 Offset;
		public UInt32 Address;
		public UInt32 Size;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string Name;

		public static void Serialize(SectionHeader header, System.IO.BinaryWriter writer)
		{
			writer.Write((UInt16)header.Type);
			writer.Write((UInt16)header.Flags);
			writer.Write(header.Offset);
			writer.Write(header.Address);
			writer.Write(header.Size);
			writer.Write(Encoding.ASCII.GetBytes(header.Name));
		}

		public static SectionHeader Deserialize(System.IO.BinaryReader reader)
		{
			SectionHeader header = new SectionHeader();

			header.Type = (SectionType)reader.ReadUInt16();
			header.Flags = (SectionFlag)reader.ReadUInt16();
			header.Offset = reader.ReadUInt32();
			header.Address = reader.ReadUInt32();
			header.Size = reader.ReadUInt32();
			char[] name_bytes = reader.ReadChars(16);
			header.Name = new string(name_bytes);

			return header;
		}

		public override string ToString()
		{
			string flag_names = "";
			foreach (SectionFlag flag in Enum.GetValues(typeof(SectionFlag)))
			{
				if ((Flags & flag) != 0) flag_names += $"{flag}, ";
			}
			if (flag_names.Length > 2) flag_names = flag_names.Substring(0, flag_names.Length - 2);

			return $"Type:    {Type}\n" +
					$"Flags:   0x{(UInt16)Flags:x4} ({flag_names})\n" +
					$"Offset:  0x{Offset:x8}\n" +
					$"Address: 0x{Address:x8}\n" +
					$"Size:    0x{Size:x8}\n" +
					$"Name:    {Name}";
		}
	}
}
