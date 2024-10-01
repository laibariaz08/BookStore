using BookStore.Models;

namespace BookStore.Models
{
    public class Books
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private int stock;

        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        private string picture;

        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }
        private string genre_name;

        public string genre_Name
        {
            get { return genre_name; }
            set { genre_name = value; }
        }
        private string author_name;

        public string Author_name
        {
            get { return author_name; }
            set { author_name = value; }
        }
    }
}
