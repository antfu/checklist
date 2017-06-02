using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Checklist
{
	class Log
	{
		public const string LOG_PATH = @".\debug.log";
		public static void log(string str)
		{
			raw(string.Format("[{0:yyddMM HH:mm:ss}]{1}\n", DateTime.Now, str));
		}

		public static void log(List<string> strs)
		{
			log(string.Join("\n", strs));
		}

		public static void raw(string str)
		{
			Console.Write(str);
			File.AppendAllText(LOG_PATH, str);
		}

		public static void error()
		{

		}
	}
}
