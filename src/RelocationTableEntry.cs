using System;

namespace FTG.Studios.BEEF
{
	// Must have access to symbol table and reference to section being relocated
	public struct RelocationTableEntry
	{
		// TODO: Maybe include section index?

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
		/// Platform-specific relocation type to be handled by linker.
		/// </summary>
		public UInt32 RelocationType;

		public static void Serialize(RelocationTableEntry entry, System.IO.BinaryWriter writer)
		{
			writer.Write(entry.Offset);
			writer.Write(entry.SymbolIndex);
			writer.Write(entry.Addend);
			writer.Write(entry.RelocationType);
		}

		public static RelocationTableEntry Deserialize(System.IO.BinaryReader reader)
		{
			RelocationTableEntry entry = new RelocationTableEntry();

			entry.Value = reader.readUInt32();
			entry.SymbolIndex = reader.readUInt32();
			entry.Addend = reader.readUInt32();
			entry.RelocationType = reader.readUInt32();

			return entry;
		}
	}
}
