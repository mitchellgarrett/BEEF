using System.IO;

namespace FTG.Studios.BEEF {
	
	public class ObjectFile {
		
		public FileHeader FileHeader;
		public SectionHeader[] SectionHeaders;
		public byte[][] SectionData;
		
		public static void Serialize(ObjectFile obj, string path) {
			BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create));
			
			FileHeader.Serialize(obj.FileHeader, writer);
			foreach (SectionHeader header in obj.SectionHeaders) {
				SectionHeader.Serialize(header, writer);
			}
			
			for (int i = 0; i < obj.SectionData.Length; i++) {
				writer.Write(obj.SectionData[i]);
			}
			
			writer.Close();
		}
		
		public static ObjectFile Deserialize(string path) {
			ObjectFile obj = new ObjectFile();
			BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));
			
			obj.FileHeader = FileHeader.Deserialize(reader);
			obj.SectionHeaders = new SectionHeader[obj.FileHeader.SectionCount];
			for (int i = 0; i < obj.FileHeader.SectionCount; i++) {
				obj.SectionHeaders[i] = SectionHeader.Deserialize(reader);
			}
			
			obj.SectionData = new byte[obj.FileHeader.SectionCount][];
			for (int i = 0; i < obj.FileHeader.SectionCount; i++) {
				obj.SectionData[i] = reader.ReadBytes((int) obj.SectionHeaders[i].Size);
			}
			
			ObjectFile.Serialize(obj, "deserialize.beef");
			
			return obj;
		}
		
		public static void Main(string[] args) {
			ObjectFile obj = new ObjectFile();
			obj.FileHeader = new FileHeader() {
				MagicNumber = FileHeader.MAGIC_NUMBER,
				Architecture = 0xb,
				Endianness = Endianness.Little,
				EntryPoint = 0,
				SectionTableOffset = 14,
				SectionCount = 1
			};
			
			obj.SectionHeaders = new SectionHeader[1];
			obj.SectionHeaders[0] = new SectionHeader() {
				Type = SectionType.Program,
				Flags = SectionFlag.Readable | SectionFlag.Writable | SectionFlag.Code | SectionFlag.Executable,
				Offset = 14 + 32,
				Address = 0,
				Size = 4,
				Name = ".text"
			};
			
			obj.SectionData = new byte[1][];
			obj.SectionData[0] = new byte[4];
			obj.SectionData[0][0] = 0xba;
			obj.SectionData[0][1] = 0xbe;
			obj.SectionData[0][2] = 0xca;
			obj.SectionData[0][3] = 0xfe;
			System.Console.WriteLine(obj);
			ObjectFile.Serialize(obj, "test.beef");
			obj = ObjectFile.Deserialize("test.beef");
			
			System.Console.WriteLine(obj);
		}
		
		public override string ToString() {
			string output = FileHeader.ToString();
			for (int i = 0; i < SectionHeaders.Length; i++) {
				output += $"\n\nSection {i}\n" + SectionHeaders[i].ToString();
				output += "\nData:\n\t";
				for (int b = 0; b < SectionData[i].Length; b++) {
					output += $"{SectionData[i][b]:x2} ";
					if (b != 0 && b % 4 == 0) output += "\n\t";
				}
			}
			return output;
		}
	}
}