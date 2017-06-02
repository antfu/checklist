using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Checklist
{
	class Storage
	{
		public const string DATA_PATH = @".\data.json";
		private const string JSON_PRESET = @"
{
	'counter': 0,
	'checklist':[]
}";

		public JObject data;
		public Storage()
		{

		}

		public void load()
		{
			string json = JSON_PRESET;
			if (!File.Exists(DATA_PATH))
			{
				Log.log("Data not exists, load from preset.");
			}
			else
			{
				using (StreamReader file = File.OpenText(DATA_PATH))
				{
					json = file.ReadToEnd();
				}
			}
			try
			{
				data = JObject.Parse(json);
			}
			catch (Exception e)
			{
				Log.log(e.StackTrace);
			}
		}

		public void Save()
		{
			if (data != null)
			{
				Log.log("Saving data");
				using (var file = File.CreateText(DATA_PATH))
				{
					file.Write(data.ToString());
				}
			}
		}
	}
}
