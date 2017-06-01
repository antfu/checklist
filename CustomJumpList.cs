
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
			BuildList();
		}

		public void AddToRecent(string destination)
		{
			//list.AddToRecent(destination);
			list.Refresh();
		}

		/// <summary>
		/// Builds the Jumplist
		/// </summary>
		private void BuildList()
		{
			JumpListCustomCategory cateTasks = new JumpListCustomCategory("Tasks");
			string location = Assembly.GetEntryAssembly().Location;
			Console.WriteLine(location);
			IconReference iconChecked = new IconReference(Path.Combine(Path.GetDirectoryName(location), "res\\checked.ico"), 0);
			IconReference iconUnchecked = new IconReference(Path.Combine(Path.GetDirectoryName(location), "res\\unchecked.ico"), 0);
			IconReference iconAdd = new IconReference(Path.Combine(Path.GetDirectoryName(location), "res\\add.ico"), 0);
			IconReference iconRefresh = new IconReference(Path.Combine(Path.GetDirectoryName(location), "res\\refresh.ico"), 0);
			IconReference iconOption = new IconReference(Path.Combine(Path.GetDirectoryName(location), "res\\option.ico"), 0);

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
				Arguments = "A0"
			});
			cateActions.AddJumpListItems(new JumpListLink(location, "Refresh")
			{
				IconReference = iconRefresh,
				Arguments = "A1"
			});
			cateActions.AddJumpListItems(new JumpListLink(location, "Options")
			{
				IconReference = iconOption,
				Arguments = "A2"
			});


			list.AddCustomCategories(cateActions);

			list.Refresh();
		}
	}
}