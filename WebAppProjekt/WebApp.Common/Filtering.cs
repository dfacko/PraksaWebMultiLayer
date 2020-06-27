using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Common
{
    public class Filtering
    {
        public string filterProperty { get; set; }
        public string filterCondition { get; set; }
        public Filtering(string filterProperty, string filterCondition)
        {
            this.filterProperty = filterProperty;
            this.filterCondition = filterCondition;
        }
    }

  
}
