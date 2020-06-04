using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WebAppRepo {
	public class OsobaAddRepo  {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PraksaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
		public void Add(Osoba person) {

			string name = person.Name;
            string surname = person.Surname;
            int age = person.Age;
            int Id=0;

            using (connection) {
                SqlCommand command = new SqlCommand(
                  "SELECT MAX(Id) FROM Osoba;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) {                    
                    while (reader.Read()) {             

                        Id = reader.GetInt32(0);
                        
                    }
                }
                Id = Id + 1;
                reader.Close();

                string queryString =
                $"Insert into Osoba values ({Id},-1,{age},'{name}','{surname}');";  
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                DataSet newPerson = new DataSet();  
                adapter.Fill(newPerson, "Osoba");
            }

            
            

            
            connection.Close();
            return;

		}

	}
}
