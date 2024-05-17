using System;
using System.Text;

namespace FTG.Studios.BEEF
{

	public enum SymbolType : byte { Null, File, Section, Object, Function, Variable };


	/// <summary>
	/// Size: 10 bytes + length of string.
	/// </summary>
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

		public static void Serialize(SymbolTableEntry entry, System.IO.BinaryWriter writer)
		{
			writer.Write(entry.Value);
			writer.Write(entry.Size);
			writer.Write(entry.Scope);
			writer.Write(entry.Visibility);
			writer.Write((byte)entry.Type);
			writer.Write(entry.NameLength);
			writer.Write(Encoding.ASCII.GetBytes(entry.Name));
		}

		public static SymbolTableEntry Deserialize(System.IO.BinaryReader reader)
		{
			SymbolTableEntry entry = new SymbolTableEntry();

			entry.Value = reader.ReadUInt32();
			entry.Size = reader.ReadByte();
			entry.Scope = reader.ReadByte();
			entry.Visibility = reader.ReadByte();
			entry.Type = (SymbolType)reader.ReadByte();
			entry.NameLength = reader.ReadUInt16();
			char[] name_bytes = reader.ReadChars(entry.NameLength);
			header.Name = new string(name_bytes);

			return entry;
		}
	}
}
