using System.ComponentModel.DataAnnotations;
namespace ProductSanaCommerce.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Please enter the name of the product.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the minimum (default) price for this room.")]
        [RegularExpression(@"^\$?([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$", ErrorMessage = "{0} must be a Number.")]
        [Display(Name = "Minimum price")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Please enter the quantity of product.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        [Display(Name = "Quantity")]
        public  int Quantity{get;set;}

        [Required(ErrorMessage = "Please enter the description of the product.")]
        [Display(Name = "Description")]
        public string Description{ get; set;}
    }
}
