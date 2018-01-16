using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ProductSanaCommerce.Models;

namespace ProductSanaCommerce.Controllers
{
    public class ProductsController : Controller
    {
        public string pathFile = WebConfigurationManager.AppSettings["pathFile"];
        public string nameFile = WebConfigurationManager.AppSettings["NameFile"];
        public string extensionFile = WebConfigurationManager.AppSettings["ExtensionFile"];
        public string DELIMITER = ",";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product products)
        {
            string NameOfFile = pathFile + nameFile + extensionFile;
            StringBuilder builder = new StringBuilder();
            builder.Append(products.Name).Append(DELIMITER);
            builder.Append(products.Price).Append(DELIMITER);
            builder.Append(products.Quantity).Append(DELIMITER);
            builder.Append(products.Description).Append(DELIMITER); ;
            System.IO.File.WriteAllText(NameOfFile, builder.ToString());
            return this.Json(new { EnableSuccess = true, SuccessTitle = "Success", SuccessMsg = "awesome" });
        }
    }
}
