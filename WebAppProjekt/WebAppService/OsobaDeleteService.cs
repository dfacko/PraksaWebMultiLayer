using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppRepo;

namespace WebAppService {
	public class OsobaDeleteService {

		public int Delete(int Id) {


			OsobaDeleteRepo person = new OsobaDeleteRepo();

			if (person.IDIsInDatabase(Id)) {
				person.Delete(Id);
				return 1;
			}
			
			return 0;
		}
	}
}
