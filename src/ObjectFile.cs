using System.IO;

namespace FTG.Studios.BEEF
{

	public class ObjectFile
	{

		public FileHeader FileHeader;
		public SectionHeader[] SectionHeaders;
		public byte[][] SectionData;

		public static void Serialize(ObjectFile obj, string path)
		{
			BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create));

			FileHeader.Serialize(obj.FileHeader, writer);
			foreach (SectionHeader header in obj.SectionHeaders)
			{
				SectionHeader.Serialize(header, writer);
			}

			for (int i = 0; i < obj.SectionData.Length; i++)
			{
				writer.Write(obj.SectionData[i]);
			}

			writer.Close();
		}

		public static ObjectFile Deserialize(string path)
		{
			ObjectFile obj = new ObjectFile();
			BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));

			obj.FileHeader = FileHeader.Deserialize(reader);
			obj.SectionHeaders = new SectionHeader[obj.FileHeader.SectionCount];
			for (int i = 0; i < obj.FileHeader.SectionCount; i++)
			{
				obj.SectionHeaders[i] = SectionHeader.Deserialize(reader);
			}

			obj.SectionData = new byte[obj.FileHeader.SectionCount][];
			for (int i = 0; i < obj.FileHeader.SectionCount; i++)
			{
				obj.SectionData[i] = reader.ReadBytes((int)obj.SectionHeaders[i].Size);
			}

			return obj;
		}

		public override string ToString()
		{
			string output = FileHeader.ToString();
			for (int i = 0; i < SectionHeaders.Length; i++)
			{
				output += $"\n\nSection {i}\n" + SectionHeaders[i].ToString();
				output += "\nData:\n\t";
				for (int b = 0; b < SectionData[i].Length; b++)
				{
					output += $"{SectionData[i][b]:x2} ";
					if ((b + 1) % 4 == 0) output += "\n\t";
				}
			}
			return output;
		}
	}
}