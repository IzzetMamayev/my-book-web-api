using Microsoft.EntityFrameworkCore;
using My_Book.Data.Models;
using My_Book.Data.Paging;
using My_Book.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace My_Book.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartWithANumber(publisher.Name))
            {
                throw new PublisherNameException($"Name starts with number, publisher name: {publisher.Name}");
            }
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public List<Publisher> GetAllPulishers(string sortBy, string searchStr, int? pageNumber)
        {
            var allPublisher = _context.Publishers.OrderBy(n => n.Name).ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublisher = allPublisher.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(searchStr))
            {
                allPublisher = allPublisher.Where(n => n.Name.Contains(searchStr, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            //Paging
            int pageSize = 3;
            allPublisher = PaginatedList<Publisher>.Create(allPublisher.AsQueryable(), pageNumber ?? 1, pageSize);

            return allPublisher;
        }


        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);

        public PublisherWithBooks GetPublisherWithBooks(int id)
        {
            var data = _context.Publishers.Where(n => n.Id == id).
                Select(n => new PublisherWithBooks()
                {
                    Name = n.Name,
                    Books = n.Books.ToList()
                })
                .FirstOrDefault();
            return data;
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisher = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    Books = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publisher;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);

            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id: {id} does not exist");
            }
        }

        private bool StringStartWithANumber(string name)
        {
            if (Regex.IsMatch(name, @"^\d"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
