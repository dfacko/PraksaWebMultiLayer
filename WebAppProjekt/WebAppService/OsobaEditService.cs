using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using WebAppRepo;

namespace WebAppService {
	public class OsobaEditService {
		public int Edit(int Id,string newName,string newSurname,int newAge) {


			OsobaEditRepo person = new OsobaEditRepo();
			if (person.IDIsInDatabase(Id,0)) {
				person.Edit(Id, newName,newSurname,newAge);
				return 1;
			}
			
			return 0;
		}
	}
}
