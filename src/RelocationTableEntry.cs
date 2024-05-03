using System;

namespace FTG.Studios.BEEF
{
	// Must have access to symbol table and reference to section being relocated
	public struct RelocationTableEntry
	{
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
	}
}
