using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppRepo.Common {
	public interface IRepo {

		  bool IDIsInDatabase(int Id,int jobId=0);
	}
}
