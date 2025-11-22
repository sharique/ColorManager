using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace ColorManager
{
	public class PaletteManager
	{
		protected XDocument doc;
		protected string file;

		public PaletteManager ()
		{
			file = GetFilePath ();
			doc = XDocument.Load (file);
		}
		
		/// <summary>
		/// Gets the full path to colors.xml next to the executable
		/// </summary>
		protected string GetFilePath ()
		{
			string exePath = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location);
			return Path.Combine (exePath, "colors.xml");
		}
		
		protected void LoadXml ()
		{
			doc = XDocument.Load (file);
		}

		public IEnumerable<XElement> GetPalette (string name)
		{
			XElement root = doc.Root;			
            IEnumerable<XElement> node = from e in root.Elements("Palette")
                                         where e.Attribute("name").Value == name
                                         select e;
			return node;
		}

		
		public List<string> GetList ()
		{
			var names = from p in doc.Root.Elements ("Palette")
				select p.Attribute ("name").Value;
			return names.ToList ();
		}
		
		public void CreatePalette(string name)
		{
			XElement root = doc.Root;//.Elements("Palette");
			
			root.Add(new XElement("Palette",new XAttribute("name",name)));
			save();
		}
		
		public void DeletePalette(string name)
		{
			this.GetPalette(name).Single().Remove();
			save();
		}
		/// <summary>
		/// save the xml file next to the executable
		/// </summary>
		public void save()
		{
			doc.Save(file);
		}
	}
}

