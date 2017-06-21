using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Windows.Shell;

namespace Checklist
{
    class JumplistManager
    {
        public Storage storage;
        public JumplistManager()
        {
        }

        public Storage loadStorage()
        {
            storage = new Storage().Load();
            return storage;
        }

        public void updateJumplist()
        {
            if (storage == null)
                loadStorage();

            string location = Assembly.GetEntryAssembly().Location;
            string pwd = Path.GetDirectoryName(location);

            JumpList jumpList = new JumpList();
            jumpList.ShowFrequentCategory = false;
            jumpList.ShowRecentCategory = false;

            // Actions
            jumpList.JumpItems.Add(new JumpTask
            {
                Title = "New Item",
                Arguments = "/new",
                Description = "Add new Check list item",
                CustomCategory = "Actions",
                IconResourcePath = Path.Combine(pwd, "res/add.ico"),
                ApplicationPath = Assembly.GetEntryAssembly().CodeBase
            });

            jumpList.JumpItems.Add(new JumpTask
            {
                Title = "Counter " + storage.data["counter"],
                Arguments = "/counter",
                Description = "Setup counter",
                CustomCategory = "Actions",
                IconResourcePath = Path.Combine(pwd, "res/refresh.ico"),
                ApplicationPath = Assembly.GetEntryAssembly().CodeBase
            });

            jumpList.JumpItems.Add(new JumpTask
            {
                Title = "Option",
                Arguments = "/option",
                Description = "Option",
                CustomCategory = "Actions",
                IconResourcePath = Path.Combine(pwd, "res/option.ico"),
                ApplicationPath = Assembly.GetEntryAssembly().CodeBase
            });

            //TODO: CheckItems
            foreach (JObject i in storage.Checklist)
            {
                int state = (int)i["state"];
                string icon_path = state == 0 ? "res/unchecked.ico" : "res/checked.ico";
                jumpList.JumpItems.Add(new JumpTask
                {
                    Title = (string)i["text"],
                    Arguments = "/i:" + (string)i["id"], // This should be item ID			  
                    CustomCategory = "Tasks",
                    IconResourcePath = Path.Combine(pwd, icon_path),
                    ApplicationPath = Assembly.GetEntryAssembly().CodeBase
                });
            }

            JumpList.SetJumpList(App.Current, jumpList);
        }

    }
}