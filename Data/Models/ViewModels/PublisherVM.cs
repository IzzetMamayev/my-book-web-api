using System.Collections.Generic;

namespace My_Book.Data.Models
{
    public class PublisherVM
    {
        public string Name { get; set; }
    }

    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }
        public List<BookAuthorVM> Books { get; set; }
    }

    public class PublisherWithBooks
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }

    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}
