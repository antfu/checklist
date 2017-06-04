using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Shell;
using log4net;

namespace Checklist
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private ILog Log = LogManager.GetLogger("App");
		protected override void OnStartup(StartupEventArgs e)
		{
			log4net.Config.XmlConfigurator.Configure();

			Log.Info("Starting " + string.Join(" ", e.Args));

			if (e.Args.Count() > 0)
			{
				MessageBox.Show("argument: "+ string.Join(" ", e.Args));
				Shutdown();
			}

			string location = Assembly.GetEntryAssembly().Location;
			string pwd = Path.GetDirectoryName(location);

			var manager = new JumplistManager();
			var storage = manager.loadStorage();
			storage.data["counter"] = (int)storage.data["counter"] + 1;
			storage.Save();
			manager.updateJumplist();

			// There should be a UI guide
			MessageBox.Show("This program intended to work with Taskbar Jumplist. \n\nPlease pin Checklist to taskbar.");
			Shutdown();
		}
	}
}
