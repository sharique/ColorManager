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
			EnsureColorsFileExists ();
			doc = XDocument.Load (file);
		}
		
		/// <summary>
		/// Gets the full path to colors.xml in ApplicationData folder
		/// </summary>
		protected string GetFilePath ()
		{
			string appDataPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"ColorManager");
			return Path.Combine (appDataPath, "colors.xml");
		}

		/// <summary>
		/// Ensures colors.xml exists. If not, extracts from embedded resource.
		/// Also migrates from old execution directory location if it exists there.
		/// </summary>
		protected void EnsureColorsFileExists()
		{
			if (File.Exists(file))
				return;

			// Try to migrate from old location (execution directory)
			string oldLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "colors.xml");
			if (File.Exists(oldLocation))
			{
				File.Copy(oldLocation, file);
				return;
			}

			// Extract embedded resource
			ExtractEmbeddedResource("colors.xml", file);
		}

		/// <summary>
		/// Extracts an embedded resource to a file
		/// </summary>
		private void ExtractEmbeddedResource(string resourceName, string outputPath)
		{
			try
			{
				var assembly = Assembly.GetExecutingAssembly();
				var fullResourceName = $"{assembly.GetName().Name}.{resourceName}";

				using (var stream = assembly.GetManifestResourceStream(fullResourceName))
				{
					if (stream == null)
						throw new FileNotFoundException($"Embedded resource '{fullResourceName}' not found.");

					Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
					using (var fileStream = File.Create(outputPath))
					{
						stream.CopyTo(fileStream);
					}
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error extracting embedded resource: {ex.Message}");
				throw;
			}
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

