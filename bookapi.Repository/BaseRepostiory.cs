using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookapi.Models.ViewModels;

namespace bookapi.Repository
{
    public class BaseRepostiory
    {
        protected readonly BookStoreContext _context = new BookStoreContext();
    }
}
