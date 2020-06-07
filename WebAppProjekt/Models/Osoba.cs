using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Common;

namespace Models
{
    public class Osoba : IOsoba
    {
		public int Posao_id { get; set; }
		public int Id { get; set; }
        public string Name { get; set; }
		public int Age { get; set; }
		public string Surname { get; set; }


		public  Osoba(string name="",string prezime="",int age=-1)
		{
			this.Name = name;
			this.Age = age;
			this.Surname = prezime;

		}
    }
}
