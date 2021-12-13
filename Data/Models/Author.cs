using System.Collections.Generic;

namespace My_Book.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        //Navigation many_to_many
        public List<Book_Author> Book_Authors { get; set; }

    }
}
