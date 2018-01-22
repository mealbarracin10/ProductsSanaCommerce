using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductSanaCommerce.Models;
using System.Web.Configuration;
using System.Xml.Linq; 


namespace ProductSanaCommerce.Controllers
{
    public class SettingsController : Controller
    {
        public string SettingsFile = WebConfigurationManager.AppSettings["SettingsFile"];
        public string ExtensionSettingsFile = WebConfigurationManager.AppSettings["ExtensionSettingsFile"];

        public ActionResult Index()
        {
            Settings settings = new Settings();
            string Settings = SettingsFile + ExtensionSettingsFile;
            XElement doc = XElement.Load(Settings);
            var OptionStorage = doc.Descendants("OptionStorage")
                .First()
                .Value;
            settings.OptionStorage = Convert.ToInt32(OptionStorage);
            return View (settings);
        }

        public ActionResult SaveOptions(Settings setting)
        {
			string Settings = SettingsFile + ExtensionSettingsFile;
            XDocument xDoc = XDocument.Load(Settings); 
            xDoc.Descendants("OptionStorage").First().Value = setting.OptionStorage.ToString();
            xDoc.Save(Settings);
            return RedirectToAction("Index", "Settings");
        }
    }
}