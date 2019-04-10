using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Class57.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class Manager
    {
        private string _connectionString;

        public Manager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddPerson(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO People (FirstName, LastName, Age)
                                  VALUES (@firstName, @lastName, @age) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePerson(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"UPDATE People
                                    SET FirstName = @firstName, LastName = @lastName, Age = @age
                                    WHERE Id = @id";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                cmd.Parameters.AddWithValue("@id", person.Id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePerson(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM People
                                    WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Person> GetPeople()
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM People";
                connection.Open();
                var reader = cmd.ExecuteReader();
                var people = new List<Person>();
                while (reader.Read())
                {
                    people.Add(new Person
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"]
                    });
                }
                return people;
            }
        }
    }
}
