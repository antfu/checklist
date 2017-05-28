using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Checklist
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			bool firstInstance = false;

			Mutex mtx = new Mutex(true, "Antfu.Checklist", out firstInstance);

			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Main());
			}
			else
			{
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
						WindowsMessageHelper.SendMessage("Antfu.Checklist", WindowsMessageHelper.ClearHistoryArg);
						break;
				}
			}
		}
	}
}
