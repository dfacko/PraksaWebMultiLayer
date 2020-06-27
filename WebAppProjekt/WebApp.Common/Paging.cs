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

        public Paging (int RecordsPerPage,int CurrentPage=1)
        {
            this.CurrentPage = CurrentPage;
            this.RecordsPerPage = RecordsPerPage;
            CheckPaging();
        }

        void CheckPaging()
        {
            //if (CurrentPage < 1 ) { CurrentPage = 1; }
            if (RecordsPerPage<1) { RecordsPerPage = 1; } // ovdje mogu namjestit da ubijek ,ako ima, bude minimalno 5 elementa na stranici
        }
    }
}
