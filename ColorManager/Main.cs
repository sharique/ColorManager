using System;
using Gtk;

namespace ColorManager
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			//MainWindow win = new MainWindow ();
			MyForm win = new MyForm ();
			win.Show ();
			Application.Run ();
		}
	}
}