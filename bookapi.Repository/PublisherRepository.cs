using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookapi.Models.Models;
using bookapi.Models.ViewModels;

namespace bookapi.Repository
{
    public class PublisherRepository : BaseRepostiory
    {
        public ListResponce<Publisher> GetPublisherlist(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Publishers.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalReocrds = query.Count();
            List<Publisher> publishers = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponce<Publisher>()
            {
                Results = publishers,
                TotalRecords = totalReocrds,
            };
        }



        public Publisher GetPublisher(int id)
        {
            return _context.Publishers.FirstOrDefault(c => c.Id == id);
        }



        public Publisher AddPublisher(Publisher publishers)
        {
            var entry = _context.Publishers.Add(publishers);
            _context.SaveChanges();
            return entry.Entity;
        }




        public Publisher UpdatePublisher(Publisher publishers)
        {
            var entry = _context.Publishers.Update(publishers);
            _context.SaveChanges();
            return entry.Entity;
        }


        public bool DeletePublisher(int id)
        {
            var category = _context.Publishers.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return false;

            _context.Publishers.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}
