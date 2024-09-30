using BookStore.Models;

public class GenreAuthor
{
    public List<Genres> Genres { get; set; } // Assuming 'Category' represents Genres
    public List<Author> Authors { get; set; } // Assuming you have an 'Author' model
}