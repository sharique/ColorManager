using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace ColorManager
{
	public partial class MainViewModel : ObservableObject
	{
		private PaletteManager paletteManager;

		[ObservableProperty]
		private ObservableCollection<string> palettes = new();

		[ObservableProperty]
		private string selectedPalette = string.Empty;

		[ObservableProperty]
		private ObservableCollection<MyColor> colors = new();

		[ObservableProperty]
		private string newPaletteName = string.Empty;

		public MainViewModel()
		{
			try
			{
				paletteManager = new PaletteManager();
				LoadPalettesCommand.Execute(null);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error initializing MainViewModel: {ex.Message}");
			}
		}

		[RelayCommand]
		private void LoadPalettes()
		{
			try
			{
				Palettes.Clear();
				var paletteList = paletteManager.GetList();
				foreach (var palette in paletteList)
				{
					Palettes.Add(palette);
				}

				if (Palettes.Count > 0 && string.IsNullOrEmpty(SelectedPalette))
				{
					SelectedPalette = Palettes[0];
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error loading palettes: {ex.Message}");
			}
		}

		partial void OnSelectedPaletteChanged(string value)
		{
			if (value != null)
			{
				LoadColors(value);
			}
		}

		private void LoadColors(string paletteName)
		{
			try
			{
				Colors.Clear();
				var colorElements = paletteManager.GetPalette(paletteName);
				foreach (var element in colorElements)
				{
					var colorList = element.Elements("Color");
					foreach (var colorElement in colorList)
					{
						var hexAttr = colorElement.Attribute("hex");
						if (hexAttr != null)
						{
							var myColor = new MyColor { Hex = hexAttr.Value };
							Colors.Add(myColor);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error loading colors: {ex.Message}");
			}
		}

		[RelayCommand]
		private void CreatePalette()
		{
			if (string.IsNullOrWhiteSpace(NewPaletteName))
			{
				Debug.WriteLine("Palette name cannot be empty");
				return;
			}

			try
			{
				paletteManager.CreatePalette(NewPaletteName);
				NewPaletteName = string.Empty;
				LoadPalettesCommand.Execute(null);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error creating palette: {ex.Message}");
			}
		}

		[RelayCommand]
		private void DeletePalette()
		{
			if (string.IsNullOrWhiteSpace(SelectedPalette))
			{
				Debug.WriteLine("No palette selected");
				return;
			}

			try
			{
				paletteManager.DeletePalette(SelectedPalette);
				LoadPalettesCommand.Execute(null);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error deleting palette: {ex.Message}");
			}
		}

		[RelayCommand]
		private void UpdateColor(MyColor color)
		{
			if (color == null)
				return;

			int index = Colors.IndexOf(color);
			if (index >= 0)
			{
				// Color updated via binding
				SaveColorsInternal();
			}
		}

		public void SaveColorsInternal()
		{
			try
			{
				if (SelectedPalette == null)
					return;

				var paletteElement = paletteManager.GetPalette(SelectedPalette).FirstOrDefault();
				if (paletteElement == null)
					return;

				// Remove existing colors
				paletteElement.Elements("Color").Remove();

				// Add updated colors
				foreach (var color in Colors)
				{
					paletteElement.Add(new XElement("Color", new XAttribute("hex", color.Hex)));
				}

				paletteManager.save();
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error saving colors: {ex.Message}");
			}
		}

		[RelayCommand]
		private void SaveColors()
		{
			SaveColorsInternal();
		}
	}
}
