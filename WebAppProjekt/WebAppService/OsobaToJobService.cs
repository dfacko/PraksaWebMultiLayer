using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppRepo;

namespace WebAppService {
	public class OsobaToJobService {

		public int AddToJob(int personid,int jobId) {

			OsobaToJobRepo person = new OsobaToJobRepo();
			if (person.IDIsInDatabase(personid, jobId)) {  // provejra ako postoji posao koji se zeli dodat i korisnik koji se zeli dodat da radi taj posao
				person.AddToJob(personid, jobId);
				return 1;
			}

			return 0;
		}

	}
	
}
