//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Xml.Linq;

namespace ColorManager
{
	public class MyColor
	{
		public string colorName { get; set; }
        public string colorCode { get; set; }
		
        public MyColor()
        {
        }

        public XElement ToXml()
        {
            return new XElement("Color", new XAttribute("name", colorName), new XAttribute("code", colorCode));
        }

        public void FromXml(XElement xelem)
        {
            colorCode = xelem.Attribute("code").Value;
            colorName = xelem.Attribute("name").Value;
        }		
	}
}
