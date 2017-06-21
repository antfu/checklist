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
        public static bool handle(string argument, Storage storage)
        {
            argument = argument.ToLower().Trim().TrimStart('/');
            if (argument.Length <= 0)
                return false;

            if (argument.StartsWith("i:"))
            {
                string id = argument.Split(':')[1];
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

            return false;
        }
    }
}
