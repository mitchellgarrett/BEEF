using System;
using System.Runtime.InteropServices;

namespace FTG.Studios.BEEF
{

	/// <summary>
	/// Size: 16 bytes.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct RelocationTableEntry
	{

		public const int SizeInBytes = 16;

		/// <summary>
		/// Index in the section header table of the section the symbol is reference in.
		/// </summary>
		public UInt16 SectionIndex;

		/// <summary>
		/// Location of symbol reference to be relocated relative to its section.
		/// </summary>
		public UInt32 Offset;

		/// <summary>
		/// Index of symbol in symbol table.
		/// </summary>
		public UInt32 SymbolIndex;

		/// <summary>
		/// Constant to be added to relocated address.
		/// </summary>
		public UInt32 Addend;

		/// <summary>
		/// Platform-specific relocation data to be handled by linker.
		/// </summary>
		public UInt16 RelocationType;

		// TODO: This may need to be made endian-agnostic
		public static RelocationTableEntry Assemble(byte[] bytes)
		{
			IntPtr pointer = Marshal.AllocHGlobal(SizeInBytes);
			Marshal.Copy(bytes, 0, pointer, SizeInBytes);
			RelocationTableEntry entry = (RelocationTableEntry)Marshal.PtrToStructure(pointer, typeof(RelocationTableEntry));
			Marshal.FreeHGlobal(pointer);

			return entry;
		}

		public static byte[] Disassemble(RelocationTableEntry entry)
		{
			byte[] bytes = new byte[SizeInBytes];

			IntPtr pointer = Marshal.AllocHGlobal(SizeInBytes);
			Marshal.StructureToPtr(entry, pointer, true);
			Marshal.Copy(pointer, bytes, 0, SizeInBytes);
			Marshal.FreeHGlobal(pointer);

			return bytes;
		}

		public static void Serialize(RelocationTableEntry entry, System.IO.BinaryWriter writer)
		{
			writer.Write(entry.SectionIndex);
			writer.Write(entry.Offset);
			writer.Write(entry.SymbolIndex);
			writer.Write(entry.Addend);
			writer.Write(entry.RelocationType);
		}

		public static RelocationTableEntry Deserialize(System.IO.BinaryReader reader)
		{
			RelocationTableEntry entry = new RelocationTableEntry();

			entry.SectionIndex = reader.ReadUInt16();
			entry.Offset = reader.ReadUInt32();
			entry.SymbolIndex = reader.ReadUInt32();
			entry.Addend = reader.ReadUInt32();
			entry.RelocationType = reader.ReadUInt16();

			return entry;
		}
	}
}
