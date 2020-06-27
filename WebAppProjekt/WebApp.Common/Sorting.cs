using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Common
{
    public class Sorting
    {
        public string sortOrder { get; set; }
        public string sortProperty {get;set;}

        public Sorting(string sortProperty,string sortOrder)
        {
            this.sortProperty = sortProperty;
            this.sortOrder = sortOrder;
            CheckSort();
        }

        void CheckSort()
        {
            
            if (sortOrder==null ) { sortOrder = "asc"; }
            if (sortProperty ==null) { sortProperty = "Name"; }
        }
    }
}
