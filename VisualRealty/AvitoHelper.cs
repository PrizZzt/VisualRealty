using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Linq;

namespace VisualRealty
{
	public class AvitoHelper
	{
		public static List<AvitoRow> Load(string fileName)
		{
			List<AvitoRow> data = new List<AvitoRow>();
			if (File.Exists(fileName))
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AvitoRow), new DataContractJsonSerializerSettings { DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-dd HH:mm:ss") });
				using (StreamReader sr = new StreamReader(fileName))
				{
					while (sr.EndOfStream == false)
					{
						string line = sr.ReadLine().Replace('"', '_').Replace('\'', '"').Replace("\\x","");
						using (MemoryStream srr = new MemoryStream(Encoding.UTF8.GetBytes(line)))
						{
							data.Add((AvitoRow)serializer.ReadObject(srr));
						}
					}
				}
			}

			return data;
		}
	}
}
