﻿namespace BookStore.Models
{
    public class CategoryProducts
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Category> Category { get; set; } = new List<Category>();
    }
}
