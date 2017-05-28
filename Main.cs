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
		public Main()
		{
			InitializeComponent();
			list = new CustomJumpList(this.Handle);
		}
	}
}
