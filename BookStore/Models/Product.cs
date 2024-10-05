namespace BookStore.Models
{
    public class Product
    {
        private string Name;

		public string name
		{
			get { return Name; }
			set { Name = value; }
		}
		private int price;

		public int Price
		{
			get { return price; }
			set { price = value; }
		}

		private int Stock;

		public int stock
		{
			get { return Stock; }
			set { Stock = value; }
		}

		private string ImageUrll;

		public string ImageUrl
        {
			get { return ImageUrll; }
			set { ImageUrll = value; }
		}

		public int CategoryID;
        public int CategoryId
		{
			get { return CategoryID; }	
			set { CategoryID = value; }
		}
	}
}
