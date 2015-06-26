using System;
using Gtk;
using System.Xml;
using System.Globalization;
using System.IO;
using System.Xml.XPath;
using System.Xml.Linq;

public partial class MainWindow : Gtk.Window
{
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		FormLoad ();
	}
	protected void FormLoad ()
	{
		//comboboxentry1.Clear();	
		XmlDocument xdoc = new XmlDocument ();
		xdoc.Load ("colors.xml");

		XmlElement root = xdoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {
			comboboxentry1.AppendText (child.Attributes["name"].Value);
		}
		comboboxentry1.Active = 0;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	#region -------------------------HboxEnableDisable---------------------------
	protected virtual void OnBtnNewClicked (object sender, System.EventArgs e)
	{
		hbox2.Visible = true;
		btnUpdate.Visible = false;
		entry1.Text = "  ";
	}
	protected virtual void OnBtnCancleClicked (object sender, System.EventArgs e)
	{
		hbox2.Visible = false;
	}
	#endregion
	protected virtual void add_Click (object sender, System.EventArgs e)
	{

		if (entry1.Text == comboboxentry1.ActiveText) {
			int i = comboboxentry1.Active;
			comboboxentry1.InsertText (i, entry1.Text.Trim ());
		} else {
			string a = entry1.Text;
			comboboxentry1.AppendText (a);
		}
		//comboboxentry1.te		
		hbox2.Visible = false;
	}

	protected virtual void btnUpdateClick (object sender, System.EventArgs e)
	{
		if (entry1.Text == comboboxentry1.ActiveText) {
			int i = comboboxentry1.Active;
			comboboxentry1.InsertText (i, entry1.Text);
		} else {
			string a = entry1.Text;
			comboboxentry1.AppendText (a);
		}

		hbox2.Visible = false;
	}

	protected virtual void onBtnEditClicked (object sender, System.EventArgs e)
	{
		hbox2.Visible = true;
		entry1.Text = comboboxentry1.ActiveText;
		int j = comboboxentry1.Active;
		comboboxentry1.RemoveText (j);
		btnUpdate.Visible = true;

	}
		/*	string s =   comboboxentry1.ActiveText;
		string t=  s.Trim();
      
		//code for delete
		FileStream fs = new FileStream("colors.xml",FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument();
		xmldoc.Load(fs);
		XmlElement root =xmldoc.DocumentElement;
		 
        XmlNode rmNode= root.SelectSingleNode("Palette[@name='"+t+"']") ;
		root.RemoveChild(rmNode);
		FileStream fsxml = new FileStream("colors.xml",FileMode.Truncate,FileAccess.Write,FileShare.ReadWrite);
		xmldoc.Save(fsxml);
		
		int i  = comboboxentry1.Active;
		comboboxentry1.RemoveText(i);*/
		protected virtual void onBtnDeleteClicked (object sender, System.EventArgs e)
	{
		string s = comboboxentry1.ActiveText;
		string t = s.Trim ();

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);
		XmlElement root = xmldoc.DocumentElement;

		XmlNode rmNode = root.SelectSingleNode ("Palette[@name='" + t + "']");
		root.RemoveChild (rmNode);
		FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
		xmldoc.Save (fsxml);

		int i = comboboxentry1.Active;
		comboboxentry1.RemoveText (i);

	}

	protected virtual void OnCbtnClick (object sender, System.EventArgs e)
	{
	}

	protected virtual void OnColorbutton1ColorSet (object sender, System.EventArgs e)
	{
		//Red      
		double a = colorbutton1.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton1.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;
		//blue
		double c = colorbutton1.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;

		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;
		//label1.Text  = Code;


		//code to write data into xml file
		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement newpalette = xmldoc.CreateElement ("Palette");
		XmlAttribute newpname = xmldoc.CreateAttribute ("name");
		newpname.Value = entry1.Text.Trim ();
		newpalette.SetAttributeNode (newpname);

		XmlElement firstelement = xmldoc.CreateElement ("Color");
		XmlAttribute newcname = xmldoc.CreateAttribute ("COLOR");

		newcname.Value = entry4.Text;
		firstelement.SetAttributeNode (newcname);
		newpalette.AppendChild (firstelement);

		xmldoc.DocumentElement.InsertAfter (newpalette, xmldoc.DocumentElement.LastChild);
		FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
		xmldoc.Save (fsxml);
	}

	protected virtual void OnColorbutton2ColorSet (object sender, System.EventArgs e)
	{
		//Red      
		double a = colorbutton2.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton2.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();

		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;

		//blue
		double c = colorbutton2.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();

		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;

		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;
		//label1.Text  = Code;

		//code to write data into xml file
		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {
			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);
			}
		}
	}

	protected virtual void OnColorbutton3ColorSet (object sender, System.EventArgs e)
	{
		double a = colorbutton3.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton3.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();

		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;

		//blue
		double c = colorbutton3.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();

		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;

		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;
		//	label1.Text  = Code;

		//code to write data into xml file

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}


	protected virtual void OnColorbutton4ColorSet (object sender, System.EventArgs e)
	{
		//Red      
		double a = colorbutton4.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton4.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;

		//blue
		double c = colorbutton4.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;

		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;
		//label1.Text  = Code;

		//code to write data into xml file
		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}

	//b5
	protected virtual void OnColorbutton5ColorSet (object sender, System.EventArgs e)
	{
		//Red      
		double a = colorbutton5.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton5.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;
		//blue
		double c = colorbutton5.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;
		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;
		//label1.Text  = Code;
		//code to write data into xml file
		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {
			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);
			}
		}
	}
	//b6
	protected virtual void OnColorbutton6ColorSet (object sender, System.EventArgs e)
	{
		//Red      
		double a = colorbutton6.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton6.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();

		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;

		//blue
		double c = colorbutton6.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();

		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;


		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;
		//label1.Text  = Code;
		//code to write data into xml file

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}
	//b7
	protected virtual void OnColorbutton7ColorSet (object sender, System.EventArgs e)
	{
		//Red      
		double a = colorbutton7.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;
		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton7.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;
		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;
		//blue
		double c = colorbutton7.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;


		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;
		//label1.Text  = Code;
		//code to write data into xml file

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);
			}
		}
	}
	//b8
	protected virtual void OnColorbutton8ColorSet (object sender, System.EventArgs e)
	{
		//Red      
		double a = colorbutton8.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)
			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton8.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;
		//blue
		//string bl	= colorbutton1.Color.Blue.ToString();
		double c = colorbutton8.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;


		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;

		//code to write data into xml file

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}
	//b9
	protected virtual void OnColorbutton9ColorSet (object sender, System.EventArgs e)
	{
		//Red      
		double a = colorbutton9.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton9.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;

		//blue
		double c = colorbutton9.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;

		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;

		//code to write data into xml file

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}
	//b10
	protected virtual void OnColorbutton10ColorSet (object sender, System.EventArgs e)
	{
		//Red      
		double a = colorbutton10.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton10.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;
		//blue
		double c = colorbutton10.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;

		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;

		//code to write data into xml file

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}
	//b11
	protected virtual void OnColorbutton11ColorSet (object sender, System.EventArgs e)
	{
		//entry3.Text = "Button2";
		//Red      
		double a = colorbutton11.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton11.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;

		//blue
		double c = colorbutton11.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;


		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;

		//code to write data into xml file
		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);
			}
		}
	}
	//b12
	protected virtual void OnColorbutton12ColorSet (object sender, System.EventArgs e)
	{

		//Red      
		double a = colorbutton12.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton12.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;
		//blue
		double c = colorbutton12.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;

		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;


		//code to write data into xml file
		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}
	//b13
	protected virtual void OnColorbutton13ColorSet (object sender, System.EventArgs e)
	{

		//Red      
		double a = colorbutton13.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton13.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;
		//blue
		double c = colorbutton13.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;

		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;

		//code to write data into xml file

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}
	//b14
	protected virtual void OnColorbutton14ColorSet (object sender, System.EventArgs e)
	{
		// entry3.Text = "Button2";
		//Red      
		double a = colorbutton14.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton14.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;

		//blue
		double c = colorbutton14.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();

		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;

		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;
		//label1.Text  = Code;

		//code to write data into xml file

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);
		XmlElement root = xmldoc.DocumentElement;

		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}
	//b15
	protected virtual void OnColorbutton15ColorSet (object sender, System.EventArgs e)
	{
		// entry3.Text = "Button2";
		//Red      
		double a = colorbutton15.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton15.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;

		//blue
		double c = colorbutton15.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;

		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;
		//	label1.Text  = Code;
		//code to write data into xml file

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}
	//b16
	protected virtual void OnColorbutton16ColorSet (object sender, System.EventArgs e)
	{
		//  entry3.Text = "Button2";
		//Red      
		double a = colorbutton16.Color.Red;
		int mod1 = Convert.ToInt32 (a / 257);

		string red = mod1.ToString ();
		string l1 = Convert.ToString (mod1, 16).ToUpper ();
		string hax1;

		if (l1.Length <= 1)

			hax1 = string.Concat ("0", l1);
		else
			hax1 = l1;
		//Green		
		double b = colorbutton16.Color.Green;
		int mod2 = Convert.ToInt32 (b / 257);

		string green = mod2.ToString ();
		string l2 = Convert.ToString (mod2, 16).ToUpper ();
		string hax2;

		if (l2.Length <= 1)

			hax2 = string.Concat ("0", l2);
		else
			hax2 = l2;
		//blue
		double c = colorbutton16.Color.Blue;
		int mod3 = Convert.ToInt32 (c / 257);

		string blue = mod3.ToString ();
		string l3 = Convert.ToString (mod3, 16).ToUpper ();
		string hax3;
		if (l3.Length <= 1)

			hax3 = string.Concat ("0", l3);
		else
			hax3 = l3;
		string XCode = string.Concat ("#", hax1, hax2, hax3);
		entry4.Text = XCode;

		string Code = string.Concat (red, green, blue);
		entry2.Text = Code;
		//label1.Text  = Code;

		//code to write data into xml file

		FileStream fs = new FileStream ("colors.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.Load (fs);

		XmlElement root = xmldoc.DocumentElement;
		foreach (XmlNode child in root.ChildNodes) {

			if (child.Attributes["name"].Value == entry1.Text) {
				XmlElement xmlNewColor = xmldoc.CreateElement ("Color");
				xmlNewColor.SetAttribute ("COLOR", entry4.Text);
				child.AppendChild (xmlNewColor);
				FileStream fsxml = new FileStream ("colors.xml", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
				xmldoc.Save (fsxml);

			}
		}
	}
	protected virtual void btnCancel (object o, Gtk.ButtonPressEventArgs args)
	{
	}
	protected virtual void btn_Click (object sender, System.EventArgs e)
	{
		//Code to get hexadecimal from decimal
		//int i= Convert.ToInt32(entry3.Text);		
		// label1.Text = Convert.ToString(i, 16).ToUpper();
	}
	protected virtual void Cmb_Changed (object sender, System.EventArgs e)
	{
		colorbutton1.Color = new Gdk.Color (255, 255, 255);
		colorbutton2.Color = new Gdk.Color (255, 255, 255);
		colorbutton3.Color = new Gdk.Color (255, 255, 255);
		colorbutton4.Color = new Gdk.Color (255, 255, 255);
		colorbutton5.Color = new Gdk.Color (255, 255, 255);
		colorbutton6.Color = new Gdk.Color (255, 255, 255);
		colorbutton7.Color = new Gdk.Color (255, 255, 255);
		colorbutton8.Color = new Gdk.Color (255, 255, 255);
		colorbutton9.Color = new Gdk.Color (255, 255, 255);
		colorbutton10.Color = new Gdk.Color (255, 255, 255);
		colorbutton11.Color = new Gdk.Color (255, 255, 255);
		colorbutton12.Color = new Gdk.Color (255, 255, 255);
		colorbutton13.Color = new Gdk.Color (255, 255, 255);
		colorbutton14.Color = new Gdk.Color (255, 255, 255);
		colorbutton15.Color = new Gdk.Color (255, 255, 255);
		colorbutton16.Color = new Gdk.Color (255, 255, 255);


		XmlDocument xdoc = new XmlDocument ();
		xdoc.Load ("colors.xml");
		XmlElement root = xdoc.DocumentElement;

		foreach (XmlNode child in root.ChildNodes) {
			if (child.Attributes["name"].Value == comboboxentry1.ActiveText) {
				int cnt = child.ChildNodes.Count;
				if (cnt >= 1) {
					XmlNode newchild1 = child.FirstChild;
					string clr1 = newchild1.Attributes["COLOR"].Value;
					string r1 = clr1.Substring (1, 2);

					//Code To convert hexadecimal to decimal
					int ir1 = Int32.Parse (r1, NumberStyles.HexNumber);

					string g1 = clr1.Substring (3, 2);
					int ig1 = Int32.Parse (g1, NumberStyles.HexNumber);

					string b1 = clr1.Substring (5, 2);
					int ib1 = Int32.Parse (b1, NumberStyles.HexNumber);
					colorbutton1.Color = new Gdk.Color ((byte)ir1, (byte)ig1, (byte)ib1);

					cnt--;
				} else
					return;

				//2
				if (cnt >= 1) {
					XmlNode newchild2 = child.ChildNodes[1];
					string clr2 = newchild2.Attributes["COLOR"].Value;

					string r2 = clr2.Substring (1, 2);
					int ir2 = Int32.Parse (r2, NumberStyles.HexNumber);

					string g2 = clr2.Substring (3, 2);
					int ig2 = Int32.Parse (g2, NumberStyles.HexNumber);

					string b2 = clr2.Substring (5, 2);
					int ib2 = Int32.Parse (b2, NumberStyles.HexNumber);

					colorbutton2.Color = new Gdk.Color ((byte)ir2, (byte)ig2, (byte)ib2);
					cnt--;
				} else
					return;

				//3 
				if (cnt >= 1) {
					XmlNode newchild3 = child.ChildNodes[2];
					string clr3 = newchild3.Attributes["COLOR"].Value;

					string r3 = clr3.Substring (1, 2);
					int ir3 = Int32.Parse (r3, NumberStyles.HexNumber);

					string g3 = clr3.Substring (3, 2);
					int ig3 = Int32.Parse (g3, NumberStyles.HexNumber);

					string b3 = clr3.Substring (5, 2);
					int ib3 = Int32.Parse (b3, NumberStyles.HexNumber);

					colorbutton3.Color = new Gdk.Color ((byte)ir3, (byte)ig3, (byte)ib3);
					cnt--;
				} else
					return;
				//4
				if (cnt >= 1) {
					XmlNode newchild4 = child.ChildNodes[3];
					string clr4 = newchild4.Attributes["COLOR"].Value;

					string r4 = clr4.Substring (1, 2);
					int ir4 = Int32.Parse (r4, NumberStyles.HexNumber);

					string g4 = clr4.Substring (3, 2);
					int ig4 = Int32.Parse (g4, NumberStyles.HexNumber);

					string b4 = clr4.Substring (5, 2);
					int ib4 = Int32.Parse (b4, NumberStyles.HexNumber);

					colorbutton4.Color = new Gdk.Color ((byte)ir4, (byte)ig4, (byte)ib4);
					cnt--;
				} else
					return;

				//5 
				if (cnt >= 1) {
					XmlNode newchild5 = child.ChildNodes[4];
					string clr5 = newchild5.Attributes["COLOR"].Value;

					string r5 = clr5.Substring (1, 2);
					int ir5 = Int32.Parse (r5, NumberStyles.HexNumber);

					string g5 = clr5.Substring (3, 2);
					int ig5 = Int32.Parse (g5, NumberStyles.HexNumber);

					string b5 = clr5.Substring (5, 2);
					int ib5 = Int32.Parse (b5, NumberStyles.HexNumber);

					colorbutton5.Color = new Gdk.Color ((byte)ir5, (byte)ig5, (byte)ib5);
					cnt--;
				} else
					return;
				//6 
				if (cnt >= 1) {
					XmlNode newchild6 = child.ChildNodes[5];
					string clr6 = newchild6.Attributes["COLOR"].Value;

					string r6 = clr6.Substring (1, 2);
					int ir6 = Int32.Parse (r6, NumberStyles.HexNumber);

					string g6 = clr6.Substring (3, 2);
					int ig6 = Int32.Parse (g6, NumberStyles.HexNumber);

					string b6 = clr6.Substring (5, 2);
					int ib6 = Int32.Parse (b6, NumberStyles.HexNumber);

					colorbutton6.Color = new Gdk.Color ((byte)ir6, (byte)ig6, (byte)ib6);
					cnt--;
				} else
					return;

				//7 
				if (cnt >= 1) {
					XmlNode newchild7 = child.ChildNodes[6];
					string clr7 = newchild7.Attributes["COLOR"].Value;

					string r7 = clr7.Substring (1, 2);
					int ir7 = Int32.Parse (r7, NumberStyles.HexNumber);

					string g7 = clr7.Substring (3, 2);
					int ig7 = Int32.Parse (g7, NumberStyles.HexNumber);

					string b7 = clr7.Substring (5, 2);
					int ib7 = Int32.Parse (b7, NumberStyles.HexNumber);
					colorbutton7.Color = new Gdk.Color ((byte)ir7, (byte)ig7, (byte)ib7);
					cnt--;
				} else
					return;
				//8 
				if (cnt >= 1) {
					XmlNode newchild8 = child.ChildNodes[7];
					string clr8 = newchild8.Attributes["COLOR"].Value;

					string r8 = clr8.Substring (1, 2);
					int ir8 = Int32.Parse (r8, NumberStyles.HexNumber);

					string g8 = clr8.Substring (3, 2);
					int ig8 = Int32.Parse (g8, NumberStyles.HexNumber);

					string b8 = clr8.Substring (5, 2);
					int ib8 = Int32.Parse (b8, NumberStyles.HexNumber);
					colorbutton8.Color = new Gdk.Color ((byte)ir8, (byte)ig8, (byte)ib8);
					cnt--;
				} else
					return;
				//9 
				if (cnt >= 1) {
					XmlNode newchild9 = child.ChildNodes[8];
					string clr9 = newchild9.Attributes["COLOR"].Value;

					string r9 = clr9.Substring (1, 2);
					int ir9 = Int32.Parse (r9, NumberStyles.HexNumber);

					string g9 = clr9.Substring (3, 2);
					int ig9 = Int32.Parse (g9, NumberStyles.HexNumber);

					string b9 = clr9.Substring (5, 2);
					int ib9 = Int32.Parse (b9, NumberStyles.HexNumber);
					colorbutton9.Color = new Gdk.Color ((byte)ir9, (byte)ig9, (byte)ib9);
					cnt--;
				} else
					return;
				//10 
				if (cnt >= 1) {
					XmlNode newchild10 = child.ChildNodes[9];
					string clr10 = newchild10.Attributes["COLOR"].Value;

					string r10 = clr10.Substring (1, 2);
					int ir10 = Int32.Parse (r10, NumberStyles.HexNumber);

					string g10 = clr10.Substring (3, 2);
					int ig10 = Int32.Parse (g10, NumberStyles.HexNumber);

					string b10 = clr10.Substring (5, 2);
					int ib10 = Int32.Parse (b10, NumberStyles.HexNumber);
					colorbutton10.Color = new Gdk.Color ((byte)ir10, (byte)ig10, (byte)ib10);
					cnt--;
				} else
					return;
				//11 
				if (cnt >= 1) {
					XmlNode newchild11 = child.ChildNodes[10];
					string clr11 = newchild11.Attributes["COLOR"].Value;

					string r11 = clr11.Substring (1, 2);
					int ir11 = Int32.Parse (r11, NumberStyles.HexNumber);

					string g11 = clr11.Substring (3, 2);
					int ig11 = Int32.Parse (g11, NumberStyles.HexNumber);

					string b11 = clr11.Substring (5, 2);
					int ib11 = Int32.Parse (b11, NumberStyles.HexNumber);
					colorbutton11.Color = new Gdk.Color ((byte)ir11, (byte)ig11, (byte)ib11);
					cnt--;
				} else
					return;
				//12 
				if (cnt >= 1) {
					XmlNode newchild12 = child.ChildNodes[11];
					string clr12 = newchild12.Attributes["COLOR"].Value;

					string r12 = clr12.Substring (1, 2);
					int ir12 = Int32.Parse (r12, NumberStyles.HexNumber);

					string g12 = clr12.Substring (3, 2);
					int ig12 = Int32.Parse (g12, NumberStyles.HexNumber);

					string b12 = clr12.Substring (5, 2);
					int ib12 = Int32.Parse (b12, NumberStyles.HexNumber);
					colorbutton12.Color = new Gdk.Color ((byte)ir12, (byte)ig12, (byte)ib12);
					cnt--;
				} else
					return;
				//13 
				if (cnt >= 1) {
					XmlNode newchild13 = child.ChildNodes[12];
					string clr13 = newchild13.Attributes["COLOR"].Value;

					string r13 = clr13.Substring (1, 2);
					int ir13 = Int32.Parse (r13, NumberStyles.HexNumber);

					string g13 = clr13.Substring (3, 2);
					int ig13 = Int32.Parse (g13, NumberStyles.HexNumber);

					string b13 = clr13.Substring (5, 2);
					int ib13 = Int32.Parse (b13, NumberStyles.HexNumber);
					colorbutton13.Color = new Gdk.Color ((byte)ir13, (byte)ig13, (byte)ib13);
					cnt--;
				} else
					return;
				//14 
				if (cnt >= 1) {
					XmlNode newchild14 = child.ChildNodes[13];
					string clr14 = newchild14.Attributes["COLOR"].Value;

					string r14 = clr14.Substring (1, 2);
					int ir14 = Int32.Parse (r14, NumberStyles.HexNumber);

					string g14 = clr14.Substring (3, 2);
					int ig14 = Int32.Parse (g14, NumberStyles.HexNumber);

					string b14 = clr14.Substring (5, 2);
					int ib14 = Int32.Parse (b14, NumberStyles.HexNumber);
					colorbutton14.Color = new Gdk.Color ((byte)ir14, (byte)ig14, (byte)ib14);
					cnt--;
				} else
					return;
				//15  
				if (cnt >= 1) {
					XmlNode newchild15 = child.ChildNodes[14];
					string clr15 = newchild15.Attributes["COLOR"].Value;

					string r15 = clr15.Substring (1, 2);
					int ir15 = Int32.Parse (r15, NumberStyles.HexNumber);

					string g15 = clr15.Substring (3, 2);
					int ig15 = Int32.Parse (g15, NumberStyles.HexNumber);

					string b15 = clr15.Substring (5, 2);
					int ib15 = Int32.Parse (b15, NumberStyles.HexNumber);
					colorbutton15.Color = new Gdk.Color ((byte)ir15, (byte)ig15, (byte)ib15);
					cnt--;
				} else
					return;
				//16 
				if (cnt >= 1) {
					XmlNode newchild16 = child.ChildNodes[15];
					string clr16 = newchild16.Attributes["COLOR"].Value;

					string r16 = clr16.Substring (1, 2);
					int ir16 = Int32.Parse (r16, NumberStyles.HexNumber);

					string g16 = clr16.Substring (3, 2);
					int ig16 = Int32.Parse (g16, NumberStyles.HexNumber);

					string b16 = clr16.Substring (5, 2);
					int ib16 = Int32.Parse (b16, NumberStyles.HexNumber);
					colorbutton16.Color = new Gdk.Color ((byte)ir16, (byte)ig16, (byte)ib16);
					cnt--;
				} else
					return;
			}
			//if
		}
		//for
	}

	protected virtual void cbtn_Released (object sender, System.EventArgs e)
	{
	}

	protected virtual void QuitClicked (object sender, System.EventArgs e)
	{
		Application.Quit ();
	}

	protected virtual void AboutusClicked (object sender, System.EventArgs e)
	{
		MessageDialog md = new MessageDialog (this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "Color Manager \n \nVersion : 0.1\n.");
		md.Show ();
	}

}
