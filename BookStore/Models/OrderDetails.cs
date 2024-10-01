using BookStore.Models;
using Humanizer;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace BookStore.Models
{
    public class OrderDetails
    {
        public int id { get; set; }
        public int OrderID { get; set; }
        public int BookID {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

