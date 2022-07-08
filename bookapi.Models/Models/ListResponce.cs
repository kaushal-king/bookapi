using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookapi.Models.Models
{
    public class ListResponce<T> where T:class
    {
        public IEnumerable<T> Results { get; set; }

        public int TotalRecords { get; set; }
    }
}
