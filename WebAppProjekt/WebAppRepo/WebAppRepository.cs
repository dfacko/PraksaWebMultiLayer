using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;
using WebAppRepo.Common;

namespace WebAppRepo {
	public class WebAppRepository :IRepo {

		
		public void Add(Osoba person) {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PraksaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
                $"Insert into Osoba values ({Id},-1,{age},'{name}','{surname}');";  //po defaultu svaka nova osoba ima job_id=-1 kao nezaposlena osoba
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                DataSet newPerson = new DataSet();  
                adapter.Fill(newPerson, "Osoba");
            }

            
            

            
            connection.Close();
            return;

		}

		public string GetJobDesc(int posao_id) {
			 SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PraksaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string JobDesc="";

            using (connection) {
                SqlCommand command = new SqlCommand(
                $"SELECT opis_posla FROM Posao where Posao.id={posao_id};",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) {                    
                    while (reader.Read()) {

                        JobDesc = reader.GetString(0);
                        
                    }
                }
                reader.Close();

            }

            
            connection.Close();
            return JobDesc;
		}

		public  List<Osoba> HasRows() {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PraksaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            List<Osoba> popis = new List<Osoba>();
            string name = "",
            surname=""; 
            int age=-1;
            int id = -1;
            int posao_id = -1;
            Osoba person;
            
            
            using (connection) {
                SqlCommand command = new SqlCommand(
                  "SELECT PersonName,Surname,Age,Id,PosaoId FROM Osoba;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) {
                    while (reader.Read()) {

                        name = reader.GetString(0);
                        surname=reader.GetString(1);
                        age = reader.GetInt32(2);
                        id = reader.GetInt32(3);
                        posao_id = reader.GetInt32(4);
                        person = new Osoba(name, surname, age);
                        person.Id = id;
                        person.Posao_id = posao_id;
                        popis.Add(person);
                        
                    }
                } else {
                    
                }
                reader.Close();
                connection.Close();
            }


            return popis;
        }

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
