using System;
using System.Text;
using System.Runtime.InteropServices;

namespace FTG.Studios.BEEF
{

	public enum SectionType : UInt16 { Null, Program, SymbolTable, StringTable, RelocationTable, Debug };
	public enum SectionFlag : UInt16 { None = 0, Readable = 1, Writable = 2, Executable = 4, Code = 8, InititializedData = 16, UninitializedData = 32 };


	/// <summary>
	/// Size: 32 bytes.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct SectionHeader
	{
		public const int SizeInBytes = 32;
		public const int IdentifierSize = 16;

		public SectionType Type;
		public SectionFlag Flags;
		public UInt32 Offset;
		public UInt32 Address;
		public UInt32 Size;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = IdentifierSize)]
		public string Name;

		public static void Serialize(SectionHeader header, System.IO.BinaryWriter writer)
		{
			writer.Write((UInt16)header.Type);
			writer.Write((UInt16)header.Flags);
			writer.Write(header.Offset);
			writer.Write(header.Address);
			writer.Write(header.Size);
			
  			for (int i = 0; i < IdentifierSize; i++)
  			{
  				if (i < header.Name.Length) writer.Write(header.Name[i]);
  				else writer.Write((byte)0);
  			}
		}

		public static SectionHeader Deserialize(System.IO.BinaryReader reader)
		{
			SectionHeader header = new SectionHeader();

			header.Type = (SectionType)reader.ReadUInt16();
			header.Flags = (SectionFlag)reader.ReadUInt16();
			header.Offset = reader.ReadUInt32();
			header.Address = reader.ReadUInt32();
			header.Size = reader.ReadUInt32();
			header.Name = new string(reader.ReadChars(IdentifierSize));

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
