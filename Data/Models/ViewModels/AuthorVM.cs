using System.Collections.Generic;

namespace My_Book.Data.Models
{
    public class AuthorVM
    {
        public string FullName { get; set; }
        public int MyProperty { get; set; }
    }

    public class AuthorWithBooksVM
    {
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
