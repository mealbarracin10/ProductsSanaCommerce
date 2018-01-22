using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using ProductSanaCommerce.Models;
using System.Xml.Linq;
using System.Linq;

namespace ProductSanaCommerce.Controllers
{
    public class ProductsController : Controller
    {
        public string pathFile = WebConfigurationManager.AppSettings["pathFile"];
        public string nameFile = WebConfigurationManager.AppSettings["NameFile"];
        public string extensionFile = WebConfigurationManager.AppSettings["ExtensionFile"];
        public string SettingsFile = WebConfigurationManager.AppSettings["SettingsFile"];
        public string ExtensionSettingsFile = WebConfigurationManager.AppSettings["ExtensionSettingsFile"];
        public string DELIMITER = ",";


        [HttpGet]
        public ActionResult Redirect(ProductList products)
        {
            return View(products);
        }

        [HttpGet]
        public ActionResult Index()
        {
            ProductList productList = new ProductList();
            productList.productList = new List<Product>();
            string Settings = SettingsFile + ExtensionSettingsFile;
            XElement doc = XElement.Load(Settings);
            string OptionStorage = doc.Descendants("OptionStorage")
                .First()
                .Value;
            if(OptionStorage == "0")
            {
                var a = OptionStorage;
                return View(productList);
            }
            else if (OptionStorage == "1")
            {
            string fileCSV = pathFile + nameFile + extensionFile;
            string productCsv = string.Empty;
            
            using (StreamReader r = new StreamReader(fileCSV))
            {
                while (r.Peek() >= 0)
                {
                    productCsv = r.ReadLine();
                    string[] values = productCsv.Split(',');
                    productList.productList.Insert(0, new Product { Name = values[0], Price = float.Parse(values[1]), Quantity = Convert.ToInt32(values[2]), Description = values[3] });
                }
            }
            return View(productList);
            }

            else
            {
                var c = OptionStorage;
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddProduct(ProductList products)
        {
            ProductList productList = new ProductList();
            productList.productList = new List<Product>(); 
            string Settings = SettingsFile + ExtensionSettingsFile;
            XElement doc = XElement.Load(Settings);
            string OptionStorage = doc.Descendants("OptionStorage")
                .First()
                .Value;
            if (OptionStorage == "0")
            {
                ModelState.Clear();
                productList.productList.Insert(0, new Product { Name = products.product.Name, Price = products.product.Price, Quantity = products.product.Quantity, Description = products.product.Description });
                return View("Redirect", productList);
            }
            else if (OptionStorage == "1")
            {

                string productCsv = string.Empty;
                List<Product> Products = new List<Product>();
                string fileCSV = pathFile + nameFile + extensionFile;
                using (StreamWriter wfile = new StreamWriter(fileCSV, true))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(products.product.Name).Append(DELIMITER);
                    builder.Append(products.product.Price).Append(DELIMITER);
                    builder.Append(products.product.Quantity).Append(DELIMITER);
                    builder.Append(products.product.Description).Append(DELIMITER);
                    wfile.WriteLine(builder);
                }

                using (StreamReader r = new StreamReader(fileCSV))
                {
                    while (r.Peek() >= 0)
                    {
                        productCsv = r.ReadLine();
                        string[] values = productCsv.Split(',');
                        Products.Insert(0, new Product { Name = values[0], Price = float.Parse(values[1]), Quantity = Convert.ToInt32(values[2]), Description = values[3] });
                    }
                }
                return Json(new { data = Products }, JsonRequestBehavior.AllowGet);
            }
            else
                return View();
        }
    }
}
