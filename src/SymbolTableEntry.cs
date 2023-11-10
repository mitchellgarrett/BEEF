using System;

namespace FTG.Studios.BEEF {

    public enum SymbolType : byte { Null, File, Section, Object, Function, Variable };

    public struct SymbolTableEntry {
        public UInt32     Value;
        public byte       Size;
        public byte       Scope;
        public byte       Visibility;
        public SymbolType Type;
        public UInt16     NameLength;
        public string     Name;
    }
}
