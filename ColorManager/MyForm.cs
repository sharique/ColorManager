
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace ColorManager
{
	partial class MyForm : Gtk.Window
	{
		protected PaletteManager mgr = new PaletteManager ();

		public MyForm () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			BuildForm ();
			hbox2.Visible = false;
		}

		public void BuildForm ()
		{
			loadList ();
		}

		public void loadList ()
		{
			List<string> lst = mgr.GetList ();
			foreach (var item in lst) {
				combobox1.AppendText (item);
			}
			combobox1.Active = 0;
		}

		protected virtual void OnCombobox1Changed (object sender, System.EventArgs e)
		{
			loadPlt (combobox1.ActiveText);
		}

		protected void loadPlt (string name)
		{
			clearClrBtns ();
			IEnumerable<XElement> elem = mgr.GetPalette (name).Descendants ();
			lblDebug.Text = "Palette "+ name+ " loaded.";
			tbClr.Text="";
			int cnt = elem.Count ();
			
			if (cnt >= 1)
				clrBtn1.Color = getColor (elem.ElementAt (0).Attribute ("COLOR").Value.ToString ());
			if (cnt >= 2)
				clrBtn2.Color = getColor (elem.ElementAt (1).Attribute ("COLOR").Value.ToString ());
			if (cnt >= 3)
				clrBtn3.Color = getColor (elem.ElementAt (2).Attribute ("COLOR").Value.ToString ());
			if (cnt >= 4)
				clrBtn4.Color = getColor (elem.ElementAt (3).Attribute ("COLOR").Value.ToString ());
			if (cnt >= 5)
				clrBtn5.Color = getColor (elem.ElementAt (4).Attribute ("COLOR").Value.ToString ());
			if (cnt >= 6)
				clrBtn6.Color = getColor (elem.ElementAt (5).Attribute ("COLOR").Value.ToString ());
			if (cnt >= 7)
				clrBtn7.Color = getColor (elem.ElementAt (6).Attribute ("COLOR").Value.ToString ());
			if (cnt >= 8)
				clrBtn8.Color = getColor (elem.ElementAt (7).Attribute ("COLOR").Value.ToString ());
			if (cnt >= 9)
				clrBtn9.Color = getColor (elem.ElementAt (8).Attribute ("COLOR").Value.ToString ());
			if (cnt >= 10)
				clrBtn10.Color = getColor (elem.ElementAt (9).Attribute ("COLOR").Value.ToString ());
		}

		protected void clearClrBtns ()
		{
			clrBtn1.Color = new Gdk.Color (255, 255, 255);
			clrBtn2.Color = new Gdk.Color (255, 255, 255);
			clrBtn3.Color = new Gdk.Color (255, 255, 255);
			clrBtn4.Color = new Gdk.Color (255, 255, 255);
			clrBtn5.Color = new Gdk.Color (255, 255, 255);
			clrBtn6.Color = new Gdk.Color (255, 255, 255);
			clrBtn7.Color = new Gdk.Color (255, 255, 255);
			clrBtn8.Color = new Gdk.Color (255, 255, 255);
			clrBtn9.Color = new Gdk.Color (255, 255, 255);
			clrBtn10.Color = new Gdk.Color (255, 255, 255);
		}

		protected Gdk.Color getColor (string clr1)
		{
			string r1 = clr1.Substring (1, 2);
			
			//Code To convert hexadecimal to decimal
			int ir1 = Int32.Parse (r1, NumberStyles.HexNumber);
			
			string g1 = clr1.Substring (3, 2);
			int ig1 = Int32.Parse (g1, NumberStyles.HexNumber);
			
			string b1 = clr1.Substring (5, 2);
			int ib1 = Int32.Parse (b1, NumberStyles.HexNumber);
			
			return new Gdk.Color ((byte)ir1, (byte)ig1, (byte)ib1);
			
		}

		protected string GetColorCode (Gtk.ColorButton clrbtn)
		{
			//Red      
			double a = clrbtn.Color.Red;
			int mod1 = Convert.ToInt32 (a / 257);
			
			//string red = mod1.ToString ();
			string l1 = Convert.ToString (mod1, 16).ToUpper ();
			string hax1;
			
			if (l1.Length <= 1)
				
				hax1 = string.Concat ("0", l1);
			else
				hax1 = l1;
			//Green		
			double b = clrbtn.Color.Green;
			int mod2 = Convert.ToInt32 (b / 257);
			
			//string green = mod2.ToString ();
			string l2 = Convert.ToString (mod2, 16).ToUpper ();
			string hax2;
			
			if (l2.Length <= 1)
				
				hax2 = string.Concat ("0", l2);
			else
				hax2 = l2;
			//blue
			double c = clrbtn.Color.Blue;
			int mod3 = Convert.ToInt32 (c / 257);
			
			//string blue = mod3.ToString ();
			string l3 = Convert.ToString (mod3, 16).ToUpper ();
			string hax3;
			if (l3.Length <= 1)
				
				hax3 = string.Concat ("0", l3);
			else
				hax3 = l3;
			
			string XCode = string.Concat ("#", hax1, hax2, hax3);
			
			tbClr.Text = XCode;
			//string Code = string.Concat (red, green, blue);
			return XCode;
		}

		protected virtual void OnBtnCreateClicked (object sender, System.EventArgs e)
		{
			hbox2.Visible = !hbox2.Visible;
		}

		protected virtual void OnButCr1Clicked (object sender, System.EventArgs e)
		{
			mgr.CreatePalette (tbName.Text);
			lblDebug.Text = "New Palette created : " + tbName.Text;
			combobox1.AppendText (tbName.Text);
			hbox2.Visible=false;
		}

		protected void updateColor (string pltname, Gtk.ColorButton btn, int no)
		{
			
			string clrCode = GetColorCode (btn);
			IEnumerable<XElement> elem = mgr.GetPalette (pltname).Descendants ();
			try {
				XElement e1 = elem.ElementAt (no);
				if (e1 != null)
					e1.Attribute ("COLOR").Value = clrCode;
				//else
				//	e1.Add ("Color",new XAttribute ("COLOR", clrCode));
			} catch (Exception ex) {
				XElement plelem = mgr.GetPalette (pltname).Single ();
				XElement e = new XElement ("Color", new XAttribute ("COLOR", clrCode));
				plelem.Add (e);
			}
			
			mgr.save ();
		}

		protected virtual void OnClrBtn1ColorSet (object sender, System.EventArgs e)
		{
			updateColor (combobox1.ActiveText, clrBtn1, 0);
		}

		protected virtual void OnClrBtn2ColorSet (object sender, System.EventArgs e)
		{
			updateColor (combobox1.ActiveText, clrBtn2, 1);
		}

		protected virtual void OnClrBtn3ColorSet (object sender, System.EventArgs e)
		{
			updateColor (combobox1.ActiveText, clrBtn3, 2);
		}

		protected virtual void OnClrBtn4ColorSet (object sender, System.EventArgs e)
		{
			updateColor (combobox1.ActiveText, clrBtn4, 3);
		}

		protected virtual void OnClrBtn5ColorSet (object sender, System.EventArgs e)
		{
			updateColor (combobox1.ActiveText, clrBtn5, 4);
		}

		protected virtual void OnClrBtn6ColorSet (object sender, System.EventArgs e)
		{
			updateColor (combobox1.ActiveText, clrBtn6, 5);
		}

		protected virtual void OnClrBtn7ColorSet (object sender, System.EventArgs e)
		{
			updateColor (combobox1.ActiveText, clrBtn7, 6);
		}

		protected virtual void OnClrBtn8ColorSet (object sender, System.EventArgs e)
		{
			updateColor (combobox1.ActiveText, clrBtn8, 7);
		}

		protected virtual void OnClrBtn9ColorSet (object sender, System.EventArgs e)
		{
			updateColor (combobox1.ActiveText, clrBtn9, 8);
		}

		protected virtual void OnClrBtn10ColorSet (object sender, System.EventArgs e)
		{
			updateColor (combobox1.ActiveText, clrBtn10, 9);
		}
		
		protected virtual void OnBtnDeleteClicked (object sender, System.EventArgs e)
		{
			mgr.DeletePalette(combobox1.ActiveText);
			combobox1.RemoveText(combobox1.Active);
		}
		
		
	}
}
