using System;
using System.Collections.Generic;
using System.Linq;
using bookapi.Models.Models;
using bookapi.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace bookapi.Repository
{
    public class BookRepository : BaseRepostiory
    {

        public ListResponce<Book> GetBookList(int pageIndex ,int pageSize,string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Books.Include(c=>c.Category).Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRecord = query.Count();
            List<Book> books = query.Skip((pageIndex - 1) * pageSize).Take(50).ToList();

            return new ListResponce<Book>()
            {
                Results=books,
                TotalRecords=totalRecord

            };
        }



        public Book GetBook(int id)
        {
            return _context.Books.FirstOrDefault(c => c.Id == id);
        }



        public Book AddBook(Book book)
        {
            var entry = _context.Books.Add(book);
            _context.SaveChanges();
            return entry.Entity;
        }




        public Book UpdateBook(Book book)
        {
            var entry = _context.Books.Update(book);
            _context.SaveChanges();
            return entry.Entity;
        }


        public bool DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(c => c.Id == id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }


    }
}
