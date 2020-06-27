using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using WebApp.Common;

namespace WebAppService.Common {
	public interface IWebAppService {
		 Task<List<Osoba>> ReadAsync(Filtering filter,Sorting sorter,Paging pager);
		 Task AddAsync(Osoba person);
		Task<int> DeleteAsync(int Id);
		Task<int> EditAsync(int Id, string newName, string newSurname, int newAge);
		//Task<string> InsertJobAsync(int posao_id);

		Task<int> AddToJobAsync(int personid, int jobId);
	}
}
