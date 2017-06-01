using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Checklist
{
	public partial class Main : Form
	{
		private CustomJumpList list;
		private Storage storage;
		public Main()
		{
			InitializeComponent();
			storage = new Storage();
			storage.load();
			storage.data["counter"] = (int)storage.data["counter"] + 1;
			storage.Save();
			list = new CustomJumpList(this.Handle);
		}
	}
}
