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
using System.Runtime.InteropServices;

namespace Checklist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        [DllImport("user32.dll")]
        static extern void mouse_event(uint flags, int dx, int dy, uint data, UIntPtr extraInfo);
        const uint RightDown = 0x0008;
        const uint RightUp = 0x0010;

        private ILog Log = LogManager.GetLogger("App");
        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            Log.Info("Starting " + string.Join(" ", e.Args));

            string location = Assembly.GetEntryAssembly().Location;
            string pwd = Path.GetDirectoryName(location);
            var manager = new JumplistManager();
            var storage = manager.loadStorage();

            if (e.Args.Count() > 0)
            {
                foreach (string arg in e.Args)
                {
                    bool success = ActionHandler.handle(arg, storage);
                    if (success)
                        storage.Save();
                    manager.updateJumplist();
                }
                Shutdown();
            }
            else
            {
                manager.updateJumplist();

                // If the first time lanuch this app / Not launched from taskbar
                if (false)
                {
                    // There should be a UI guide
                    MessageBox.Show("This program intended to work with Taskbar Jumplist. \n\nPlease pin Checklist to taskbar.");
                }
                else
                {
                    // Send right click at current mouse position
                    mouse_event(RightDown, 0, 0, 0, UIntPtr.Zero);
                    mouse_event(RightUp, 0, 0, 0, UIntPtr.Zero);
                }
            }
            Shutdown();
        }
    }
}
