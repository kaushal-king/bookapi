﻿
using System.Collections.Generic;
using System.Linq;
using bookapi.Models.Models;
using bookapi.Models.ViewModels;

namespace bookapi.Repository
{
    public class CategoryRepository : BaseRepostiory
    {
        public ListResponce<Category> GetCategories(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Categories.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalReocrds = query.Count();
            List<Category> categories = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponce<Category>()
            {
                Results = categories,
                TotalRecords = totalReocrds,
            };
        }



        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }



        public Category AddCategory(Category category)
        {
            var entry = _context.Categories.Add(category);
            _context.SaveChanges();
            return entry.Entity;
        }




        public Category UpdateCategory(Category category)
        {
            var entry = _context.Categories.Update(category);
            _context.SaveChanges();
            return entry.Entity;
        }


        public bool DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }

    }
}