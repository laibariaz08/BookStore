namespace BookStore.Models
{
    public class Author
    {
		private int author_id;

		public int Author_id
        {
			get { return author_id; }
			set { author_id = value; }
		}

		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private string bio;

		public string Bio
		{
			get { return bio; }
			set { bio = value; }
		}

		private string nationality;

		public string Nationality
		{
			get { return nationality; }
			set { nationality = value; }
		}

		private string picture;

		public string Picture
		{
			get { return picture; }
			set { picture = value; }
		}


	}
}
