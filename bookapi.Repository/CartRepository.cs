using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookapi.Models.Models;
using bookapi.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace bookapi.Repository
{
    public class CartRepository:BaseRepostiory
    {
        public ListResponce<Cart> GetCartList(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Carts.Include(c=>c.Book).Where(c => keyword == null || c.Book.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalReocrds = query.Count();
            List<Cart> carts = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponce<Cart>()
            {
                Results = carts,
                TotalRecords = totalReocrds,
            };
        }



        public ListResponce<Cart> GetCartListall(int UserId)
        {
          
            var query = _context.Carts.Include(c => c.Book).Where(c => c.Userid==UserId).AsQueryable();
            
            int totalReocrds = query.Count();
            List<Cart> carts = query.ToList();

            return new ListResponce<Cart>()
            {
                Results = carts,
                TotalRecords = totalReocrds,
            };
        }



        public Cart GetCartItem(int id)
        {
            return _context.Carts.FirstOrDefault(c => c.Id == id);
        }



        public Cart AddItem(Cart cart)
        {
            var entry = _context.Carts.Add(cart);
            _context.SaveChanges();
            return entry.Entity;
        }




        public Cart UpdateCart(Cart cart)
        {
            var entry = _context.Carts.Update(cart);
            _context.SaveChanges();
            return entry.Entity;
        }


        public bool DeleteCart(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
                return false;

            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}
