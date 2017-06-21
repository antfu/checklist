using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Checklist
{
    class ActionHandler
    {
        private static List<string> preprocess(string argument)
        {
            return new List<string>(argument.ToLower().Trim().TrimStart('/').Split(':'));
        }
        public static bool handle(string argument, Storage storage)
        {
            var parts = preprocess(argument);

            // Checklist pressed
            if (parts.Count == 2 && parts[0] == "i")
            {
                string id = parts[1];
                bool found = false;

                // Find item by id
                foreach (JObject i in storage.Checklist)
                {
                    if ((string)i["id"] == id)
                    {
                        i["state"] = Math.Abs(1 - (int)i["state"]);
                        found = true;
                        break;
                    }
                }
                return found;
            }
            else if (parts.Count == 1 && parts[0] == "add")
            {
            }

            return false;
        }
    }
}
