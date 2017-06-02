
using System;	   
using Microsoft.WindowsAPICodePack.Taskbar;
using System.IO;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Shell;

namespace Checklist
{
	class CustomJumpList
	{
		private JumpList list;

		/// <summary>
		/// Creating a JumpList for the application
		/// </summary>
		/// <param name="windowHandle"></param>
		public CustomJumpList(IntPtr windowHandle)
		{
			list = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, windowHandle);
			list.KnownCategoryToDisplay = JumpListKnownCategoryType.Neither;
		}
	   	   
		public void UpdateList(Storage storage)
		{
			JumpListCustomCategory cateTasks = new JumpListCustomCategory("Tasks");
			string location = Assembly.GetEntryAssembly().Location;
			string dir = Path.GetDirectoryName(location);
			Console.WriteLine(location);
			IconReference iconChecked = new IconReference(Path.Combine(dir, "res\\checked.ico"), 0);
			IconReference iconUnchecked = new IconReference(Path.Combine(dir, "res\\unchecked.ico"), 0);
			IconReference iconAdd = new IconReference(Path.Combine(dir, "res\\add.ico"), 0);
			IconReference iconRefresh = new IconReference(Path.Combine(dir, "res\\refresh.ico"), 0);
			IconReference iconOption = new IconReference(Path.Combine(dir, "res\\option.ico"), 0);

			cateTasks.AddJumpListItems(new JumpListLink(location, "Item1")
			{
				IconReference = iconUnchecked,
				Arguments = "1",
			});
			cateTasks.AddJumpListItems(new JumpListLink(location, "Item2")
			{
				IconReference = iconUnchecked,
				Arguments = "2"
			});

			cateTasks.AddJumpListItems(new JumpListLink(location, "Item3")
			{
				IconReference = iconChecked,
				Arguments = "3"
			});


			list.AddCustomCategories(cateTasks);


			JumpListCustomCategory cateActions = new JumpListCustomCategory("Actions");

			cateActions.AddJumpListItems(new JumpListLink(location, "New Task")
			{
				IconReference = iconAdd,
				Arguments = "/A0"
			});
			cateActions.AddJumpListItems(new JumpListLink(location, string.Format("Refresh ({0})",storage.data["counter"]))
			{
				IconReference = iconRefresh,
				Arguments = "/A1"
			});
			cateActions.AddJumpListItems(new JumpListLink(location, "Options")
			{
				IconReference = iconOption,
				Arguments = "/A2"
			});


			list.AddCustomCategories(cateActions);

			list.Refresh();
		}
	}
}