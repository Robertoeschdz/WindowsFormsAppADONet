using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.CodeDom.Compiler;

namespace WindowsFormsAppADONet
{
    public class PeopleDB
    {
        private string connectionString 
            = "Server=localhost\\SQLEXPRESS;Database=CRUDWithADONet;Trusted_Connection=True;TrustServerCertificate=True;";

        public bool Ok()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
            }catch
            {
                return false;
            }
            return true;
        }

        public List<People> Get()
        {
            List<People> people = new List<People>();

            string query = "select id,name,age from people";

            // Here we are making the connection to the db:
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                // A SqlCommand object called command is created that will be used to execute queries against the database
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    // The query is executed:
                    SqlDataReader reader = command.ExecuteReader();
                    // Read reader while there are more rows in the result to read:
                    while (reader.Read()) 
                    {
                        People oPeople = new People();
                        oPeople.Id = reader.GetInt32(0);
                        oPeople.Name = reader.GetString(1);
                        oPeople.Age = reader.GetInt32(2);
                        people.Add(oPeople);
                    }

                    reader.Close();

                    connection.Close();
                }
                catch(Exception ex)
                {
                    throw new Exception("Hay un error en la base de datos:" + ex.Message);
                }
            }

           return people;
        }

        public People Get(int Id)
        {
            string query = "select id,name,age from people" + " where id=@id";

            // Here we are making the connection to the db:
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // A SqlCommand object called command is created that will be used to execute queries against the database
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);

                try
                {
                    connection.Open();
                    // The query is executed:
                    SqlDataReader reader = command.ExecuteReader();
                    // Read reader while there are more rows in the result to read:
                    reader.Read();
                    
                    People oPeople = new People();
                    oPeople.Id = reader.GetInt32(0);
                    oPeople.Name = reader.GetString(1);
                    oPeople.Age = reader.GetInt32(2);

                    reader.Close();
                    connection.Close();

                    return oPeople;
                    
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la base de datos:" + ex.Message);
                }
            }
        }

        public void Add(string Name, int Age)
        {
            string query = "insert into people(name, age) values" + "(@name, @age)";

            // Here we are making the connection to the db:
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // A SqlCommand object called command is created that will be used to execute queries against the database
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", Name);
                command.Parameters.AddWithValue("@age", Age);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la base de datos:" + ex.Message);
                }
            }
        }

        public void Update(string Name, int Age, int Id)
        {
            string query = "update people set name=@name, Age=@age" + 
                " where id=@id";

            // Here we are making the connection to the db:
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // A SqlCommand object called command is created that will be used to execute queries against the database
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", Name);
                command.Parameters.AddWithValue("@age", Age);
                command.Parameters.AddWithValue("@id", Id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la base de datos:" + ex.Message);
                }
            }
        }

        public void Delete(int Id)
        {
            string query = "delete from people" +
                " where id=@id";

            // Here we are making the connection to the db:
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // A SqlCommand object called command is created that will be used to execute queries against the database
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la base de datos:" + ex.Message);
                }
            }
        }
    }

    public class People
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public int Age { get; set;}
    }
}
