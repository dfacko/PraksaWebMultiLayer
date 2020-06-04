using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using WebAppRepo;

namespace WebAppService {
	public class OsobaAddService {

		public void Add (Osoba person){

		

			OsobaAddRepo osoba = new OsobaAddRepo();
			osoba.Add(person);
			
			return;
			}
	}
}
