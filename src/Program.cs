using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Checklist
{
	static class Program
	{
		public static string MUTEX_ID = "Antfu.Checklist";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			bool firstInstance = false;

			Mutex mtx = new Mutex(true, MUTEX_ID, out firstInstance);

			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
				Log.log("Starting with arguments: " + String.Join(" ", args));
			else
				Log.log("Starting");

			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Main());
			}
			else
			{
				Log.log("Another instance is running, sending message...");
				// Send argument to running window
				HandleCmdLineArgs();
			}
		}

		public static void HandleCmdLineArgs()
		{
			if (Environment.GetCommandLineArgs().Length > 1)
			{
				switch (Environment.GetCommandLineArgs()[1])
				{
					case "-1":
						WindowsMessageHelper.SendMessage(MUTEX_ID, WindowsMessageHelper.ClearHistoryArg);
						break;
				}
			}
		}
	}
}
