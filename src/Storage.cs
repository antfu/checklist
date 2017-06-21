using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;
using System.Reflection;

namespace Checklist
{
    class Storage
    {

        public string DATA_PATH = @"data.json";
        private const string JSON_PRESET = @"
{
	'counter': 0,
	'checklist':[]
}";

        private ILog Log = LogManager.GetLogger("Storage");
        public JObject data;
        public Storage()
        {
            DATA_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), DATA_PATH);
        }

        public Storage Load()
        {
            string json = JSON_PRESET;
            if (!File.Exists(DATA_PATH))
            {
                Log.Info("Data not exists, load from preset.");
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
                Log.Error(e.StackTrace);
            }
            return this;
        }

        public void Save()
        {
            if (data != null)
            {
                Log.Info("Saving data");
                using (var file = File.CreateText(DATA_PATH))
                {
                    file.Write(data.ToString());
                }
            }
        }

        public JArray Checklist
        {
            get
            {
                return this.data["checklist"] as JArray;
            }
        }
    }
}
