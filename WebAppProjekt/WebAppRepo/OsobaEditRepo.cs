using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppRepo.Common;

namespace WebAppRepo {
	
	public class  OsobaEditRepo :IRepo  {
		
        
		

        
        public void Edit(int Id,string newName,string newSurname,int newAge) {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PraksaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string queryString;
			using (connection) {

                    if (newName != "") {
                        queryString = $"Update Osoba set PersonName='{newName}' where Id={Id};";
                        SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                        DataSet Person = new DataSet();
                        adapter.Fill(Person, "Osoba");
                    }

                    if (newSurname != "") {
                        queryString = $"Update Osoba set Surname='{newSurname}'" +
                            $"where Id={Id};";
                        SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                        DataSet Person = new DataSet();
                        adapter.Fill(Person, "Osoba");
                    }

                    if (newAge != -1) {
                        queryString = $"Update Osoba set Age={newAge}" +
                            $"where Id={Id};";
                        SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                        DataSet Person = new DataSet();
                        adapter.Fill(Person, "Osoba");
                    }
                
            }


            return;

        }

		public bool IDIsInDatabase(int Id,int def) {
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

