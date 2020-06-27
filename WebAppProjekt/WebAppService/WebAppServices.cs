using System.Collections.Generic;
using Models;
using WebAppRepo;
using System.Threading.Tasks;
using WebAppService.Common;
using WebAppRepo.Common;
using WebApp.Common;

namespace WebAppService {
	public class WebAppServices : IWebAppService{


		protected IRepo Repository { get; private set; }


        public  WebAppServices(IRepo repo)
        {
            this.Repository = repo;
        }





		public async Task AddAsync (Osoba person){

		

			
			await Repository.AddAsync(person);
			
			return;
			}

		public async Task<int> DeleteAsync(int Id) {


			

			if (Repository.IDIsInDatabase(Id)) {
				await Repository.DeleteAsync(Id);
				return 1;
			}
			
			return 0;
		}

		public async Task<int> EditAsync(int Id,string newName,string newSurname,int newAge) { //edits person with a given id


			
			if (Repository.IDIsInDatabase(Id)) {
				await Repository.EditAsync(Id, newName,newSurname,newAge);
				return 1;
			}
			
			return 0;
		}

		/*public async Task<string> InsertJobAsync (int posao_id) {  //gets and return job Description from db for a jobId
			                                      //ne treba provjera jer ovo se poziva pri kreiranju Restmodela, koji se kreira iz vec postojuceg Osoba modela
			
			return await Repository.GetJobDescAsync(posao_id);
			
		}*/

		public async Task<List<Osoba>> ReadAsync(Filtering filter,Sorting sorter,Paging pager) {

			
			List<Osoba> popis = new List<Osoba>();
			popis = await Repository.ReadOsobasAsync(filter,sorter,pager);
			if (popis.Count == 0) {
				//tu treba returnat error 

			}
			return popis;
		}

		public async Task<int> AddToJobAsync(int personid,int jobId) { // adds a person to a job

			
			if ( Repository.IDIsInDatabase(personid, jobId)) {  // provejra ako postoji posao koji se zeli dodat i korisnik koji se zeli dodat da radi taj posao
				await Repository.AddToJobAsync(personid, jobId);
				return 1;
			}

			return 0;
		}

	}
}
