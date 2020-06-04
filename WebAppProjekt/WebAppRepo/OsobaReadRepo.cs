using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WebAppRepo
{
    public class OsobaReadRepo
    {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PraksaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private List<Osoba> popis = new List<Osoba>();
        public  List<Osoba> HasRows() {
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
    }
}
