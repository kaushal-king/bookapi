using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookapi.Models.ViewModels;

namespace bookapi.Models.Models
{
    public class BookModel
    {
       
        public BookModel()
        {

        }
        public BookModel(Book c)
        {
            Id=c.Id ;
            Name=c.Name;
            Price = c.Price;
            Description = c.Description;
            Base64image = c.Base64image;
            Categoryid = c.Categoryid;
            Publisherid = c.Publisherid;
            Quantity = c.Quantity;
            Category = c.Category==null?"": c.Category.Name;


        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Base64image { get; set; }
        public int Categoryid { get; set; }
        public string Category { get; set; }
        public int? Publisherid { get; set; }
        public int? Quantity { get; set; }
        
    }
}
