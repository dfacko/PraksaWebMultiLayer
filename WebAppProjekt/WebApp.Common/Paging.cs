using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Common
{
    public class Paging
    {
        public int RecordsPerPage { get;  set; }
        public int CurrentPage { get; set; }

        public Paging (int CurrentPage,int RecordsPerPage)
        {
            this.CurrentPage = CurrentPage;
            this.RecordsPerPage = RecordsPerPage;
        }
    }
}
