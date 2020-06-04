using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Common;

namespace WebApp {
	public class OsobaRest : IOsoba {

        public string Name { get; set; }
		public int Age { get; set; }
		public string Surname { get; set; }


		public  OsobaRest(string name,string prezime,int age)
		{
			this.Name = name;
			this.Age = age;
			this.Surname = prezime;

		}
	}
}