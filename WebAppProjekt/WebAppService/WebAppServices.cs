using System.Collections.Generic;
using Models;
using WebAppRepo;
using System.Threading.Tasks;

namespace WebAppService {
	public class WebAppServices {
		public async Task AddAsync (Osoba person){

		

			WebAppRepository osoba = new WebAppRepository();
			await osoba.AddAsync(person);
			
			return;
			}

		public async Task<int> DeleteAsync(int Id) {


			WebAppRepository person = new WebAppRepository();

			if (person.IDIsInDatabase(Id)) {
				await person.DeleteAsync(Id);
				return 1;
			}
			
			return 0;
		}

		public async Task<int> EditAsync(int Id,string newName,string newSurname,int newAge) { //edits person with a given id


			WebAppRepository person = new WebAppRepository();
			if (person.IDIsInDatabase(Id)) {
				await person.EditAsync(Id, newName,newSurname,newAge);
				return 1;
			}
			
			return 0;
		}

		public async Task<string> InsertJobAsync (int posao_id) {  //gets and return job Description from db for a jobId
			                                      //ne treba provjera jer ovo se poziva pri kreiranju Restmodela, koji se kreira iz vec postojuceg Osoba modela
			WebAppRepository person = new WebAppRepository();
			return await person.GetJobDescAsync(posao_id);
			
		}

		public async Task<List<Osoba>> ReadAsync() {

			WebAppRepository Osoba = new WebAppRepository();
			List<Osoba> popis = new List<Osoba>();

			popis = await Osoba.HasRowsAsync();
			if (popis.Count == 0) {
				//tu treba returnat error 

			}
			return popis;
		}

		public async Task<int> AddToJobAsync(int personid,int jobId) { // adds a person to a job

			WebAppRepository person = new WebAppRepository();
			if ( person.IDIsInDatabase(personid, jobId)) {  // provejra ako postoji posao koji se zeli dodat i korisnik koji se zeli dodat da radi taj posao
				await person.AddToJobAsync(personid, jobId);
				return 1;
			}

			return 0;
		}

	}
}
