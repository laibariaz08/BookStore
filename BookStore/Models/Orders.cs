using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Mono.TextTemplating;
using System.Runtime.InteropServices;
using System;

namespace BookStore.Models
{
    public class Orders
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string U_Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

    }
}
