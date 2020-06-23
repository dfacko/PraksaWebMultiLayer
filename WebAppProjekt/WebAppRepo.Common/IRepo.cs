using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using WebApp.Common;

namespace WebAppRepo.Common {
	public interface IRepo {

		bool IDIsInDatabase(int Id,int jobId=0);
		Task AddAsync(Osoba person);
		Task<string> GetJobDescAsync(int posao_id);
		Task<List<Osoba>> HasRowsAsync(Filtering filter, Sorting sorter, Paging pager);

		Task DeleteAsync(int Id);
		Task EditAsync(int Id, string newName, string newSurname, int newAge);
		Task AddToJobAsync(int personid, int jobId);
		bool IDIsInDatabase(int Id);
	}
}
