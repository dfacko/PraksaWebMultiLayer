using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppRepo {
	public class OsobaDeleteRepo {

		public void Delete(int Id) {
			SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PraksaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			using (connection) {     

                string queryString =
                $"Delete from Osoba where id={Id}";  
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                DataSet newPerson = new DataSet();  
                adapter.Fill(newPerson, "Osoba");
            }

            
           
            
            connection.Close();
            return;

		}

			public bool IDIsInDatabase(int Id) {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PraksaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            int dbID=0;
            
            SqlCommand command = new SqlCommand(
                $"SELECT Id FROM Osoba where Id={Id};",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) {
                    while (reader.Read()) {

                        dbID = reader.GetInt32(0);

                    }
                }

                reader.Close();
                connection.Close();
			if (dbID != 0) {
                return true;
			} else {
                return false;
			}
		}
	}
}
