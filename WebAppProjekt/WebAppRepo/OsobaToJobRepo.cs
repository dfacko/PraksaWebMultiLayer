using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppRepo.Common;

namespace WebAppRepo  {
	public class OsobaToJobRepo :IRepo {
		public void AddToJob(int personid, int jobId) {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PraksaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string queryString;
			using (connection) {


                        queryString = $"update Osoba set PosaoId={jobId} where PosaoId=-1;";
                        SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                        DataSet Person = new DataSet();
                        adapter.Fill(Person, "Osoba");
            }

                    
                
        }



	

		public bool IDIsInDatabase(int Id,int jobId) {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PraksaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            int dbID=0;
            int dbJobID = 0;
            
            SqlCommand command = new SqlCommand(
                $"select oId.Id, pId.Id from Osoba as oId,Posao as pId where oId.ID={Id} and pId.ID={jobId};",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) {
                    while (reader.Read()) {

                        dbID = reader.GetInt32(0);
                        dbJobID = reader.GetInt32(1);

                    }
                }

                reader.Close();
                connection.Close();
			if (dbID != 0 & dbID!=0) {
                return true;
			} else {
                return false;
			}
		}
	}
}
