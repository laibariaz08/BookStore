using BookStore.Models;
using Humanizer;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace BookStore.Models
{
    public class OrderDetails
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int OrderID { get; set; }
        public int BookID {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

