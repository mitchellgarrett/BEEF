using System;

namespace FTG.Studios.BEEF
{

	public enum SymbolType : byte { Null, File, Section, Object, Function, Variable };

	public struct SymbolTableEntry
	{

		/// <summary>
		/// Current value of symbol. Either binary value or pointer to data.
		/// </summary>
		public UInt32 Value;

		/// <summary>
		/// Size in bytes of associated data.
		/// </summary>
		public byte Size;

		/// <summary>
		/// Scope of this symbol.
		/// </summary>
		public byte Scope;

		/// <summary>
		/// Visibility of this symbol.
		/// </summary>
		public byte Visibility;

		/// <summary>
		/// Type of data this symbol points to.
		/// </summary>
		public SymbolType Type;

		/// <summary>
		/// Length of symbol identifier.
		/// </summary>
		public UInt16 NameLength;

		/// <summary>
		/// Symbol identifier.
		/// </summary>
		public string Name;
	}
}
