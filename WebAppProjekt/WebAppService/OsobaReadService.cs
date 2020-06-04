using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using WebAppRepo;

namespace WebAppService
{
    public class OsobaReadService
    {
        OsobaReadRepo Osoba = new OsobaReadRepo();
        private List<Osoba> popis = new List<Osoba>();
        
        public List<Osoba> Read() {
           popis=Osoba.HasRows();
			if (popis.Count == 0) {
                  //tu treba returnat error 
			}
            return popis;
		}
    }
}
