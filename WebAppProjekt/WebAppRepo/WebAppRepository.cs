using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Models;
using WebApp.Common;
using WebAppRepo.Common;

namespace WebAppRepo {
	public class WebAppRepository :IRepo {

		
		public  async Task AddAsync(Osoba person) {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Osoba;Integrated Security=True");

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
                $"Insert into Osoba values ({Id},-1,'{name}','{surname}',{age});";  //po defaultu svaka nova osoba ima job_id=-1 kao nezaposlena osoba
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                DataSet newPerson = new DataSet();  
                adapter.Fill(newPerson, "Osoba");
            }

            
            

            
            connection.Close();
            return;

		}

		public async Task<string> GetJobDescAsync(int posao_id) {
			 SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Osoba;Integrated Security=True");
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

		public async Task<List<Osoba>> HasRowsAsync(Filtering filter, Sorting sorter, Paging pager) {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Osoba;Integrated Security=True");
            List<Osoba> popis = new List<Osoba>();
            string name = "",
            surname=""; 
            int age=-1;
            int id = -1;
            int posao_id = -1;
            Osoba person;
            
            
            using (connection) {
                SqlCommand command = new SqlCommand(
                            //ovi tu brojevi +1 i -1 su samo da se pravilno pomice granica da na savkoj stranici bude odredeni broj elementa i da zadnji el s prve stranice ne bude prvi na sljedecoj
                $"WITH Ordered AS(SELECT *, ROW_NUMBER() OVER(ORDER BY {sorter.sortProperty} {sorter.sortOrder}) AS 'RowNumber'FROM Osoba where {filter.filterProperty} like '%{filter.filterCondition}%') SELECT Name,Surname,Age,Id,Posao_Id FROM Ordered WHERE RowNumber BETWEEN {pager.CurrentPage-1}*{pager.RecordsPerPage}+1 AND {pager.CurrentPage - 1}*{pager.RecordsPerPage}+{pager.RecordsPerPage};",
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
                    
                } 
                    
                
                reader.Close();
                connection.Close();
            }


            return popis;
        }

        public async Task DeleteAsync(int Id) {
			SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Osoba;Integrated Security=True");
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

        public async Task EditAsync(int Id,string newName,string newSurname,int newAge) {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Osoba;Integrated Security=True");
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

        public async Task AddToJobAsync(int personid, int jobId) {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Osoba;Integrated Security=True");
            string queryString;
			using (connection) {


                        queryString = $"update Osoba set PosaoId={jobId} where PosaoId=-1;";
                        SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                        DataSet Person = new DataSet();
                        adapter.Fill(Person, "Osoba");
            }

                    
                
        }

		public bool IDIsInDatabase(int Id,int jobId) {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Osoba;Integrated Security=True");
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
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Osoba;Integrated Security=True");
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
