using System.Collections.Generic;
using Models;
using WebAppRepo;

namespace WebAppService {
	public class WebAppServices {
		public void Add (Osoba person){

		

			WebAppRepository osoba = new WebAppRepository();
			osoba.Add(person);
			
			return;
			}

		public int Delete(int Id) {


			WebAppRepository person = new WebAppRepository();

			if (person.IDIsInDatabase(Id)) {
				person.Delete(Id);
				return 1;
			}
			
			return 0;
		}

		public int Edit(int Id,string newName,string newSurname,int newAge) { //edits person with a given id


			WebAppRepository person = new WebAppRepository();
			if (person.IDIsInDatabase(Id)) {
				person.Edit(Id, newName,newSurname,newAge);
				return 1;
			}
			
			return 0;
		}

		public string InsertJob (int posao_id) {  //gets and return job Description from db for a jobId
			                                      //ne treba provjera jer ovo se poziva pri kreiranju Restmodela, koji se kreira iz vec postojuceg Osoba modela
			WebAppRepository person = new WebAppRepository();
			return person.GetJobDesc(posao_id);
			
		}

		public List<Osoba> Read() {

			WebAppRepository Osoba = new WebAppRepository();
			List<Osoba> popis = new List<Osoba>();

           popis=Osoba.HasRows();
			if (popis.Count == 0) {
                  //tu treba returnat error 
			}
            return popis;
		}


		public int AddToJob(int personid,int jobId) { // adds a person to a job

			WebAppRepository person = new WebAppRepository();
			if (person.IDIsInDatabase(personid, jobId)) {  // provejra ako postoji posao koji se zeli dodat i korisnik koji se zeli dodat da radi taj posao
				person.AddToJob(personid, jobId);
				return 1;
			}

			return 0;
		}
	}
}
