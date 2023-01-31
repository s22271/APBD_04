using System.Data.SqlClient;
public class Class1
{
    public class DatabaseService : IDatabaseService
    {
        private IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            var animalList = new List<Animal>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection.Open();
                com.CommandText =
                                      "SELECT * FROM Animal ORDER BY CASE @columnName " +
                                      "WHEN 'Name' THEN [Name] " +
                                      "WHEN 'Description' THEN [Description] " +
                                      "WHEN 'Category' THEN [Category] " +
                                      "WHEN 'Area' THEN [Area] " +
                                      "END";

                com.Parameters.AddWithValue("@columnName", orderBy);


                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    animalList.Add(new Animal
                    {
                        IdAnimal = (int)dr["IdAnimal"],
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                    });
                }
            }

            return animalList;
        }

        public int AddAnimal(Animal newAnimal)
        {
            var rowsAffected = 0;

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection.Open();

                com.CommandText = "INSERT INTO Animal VALUES (@Name, @Description, @Category, @Area);";

                com.Parameters.AddWithValue("@Name", newAnimal.Name);
                com.Parameters.AddWithValue("@Description", newAnimal.Description);
                com.Parameters.AddWithValue("@Category", newAnimal.Category);
                com.Parameters.AddWithValue("@Area", newAnimal.Area);


                rowsAffected = com.ExecuteNonQuery();
            }

            return rowsAffected;
        }

        public int DeleteAnimal(int idAnimal)
        {
            int rowsAffected = 0;

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                string query = "DELETE FROM Animal WHERE IdAnimal = @idAnimal;";
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@idAnimal", idAnimal);

                con.Open();
                rowsAffected = com.ExecuteNonQuery();
            }

            return rowsAffected;

        }

        public int UpdateAnimal(int idAnimal, Animal animal)
        {
            int rowsAffected = 0;

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                string query = "UPDATE Animal" +
                               "SET Name = @Name, Description = @Description," +
                               "Category = @Category, Area = @Area" +
                               "WHERE IdAnimal = @idAnimal";
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@IdAnimal", idAnimal);
                com.Parameters.AddWithValue("@Name", animal.Name);
                com.Parameters.AddWithValue("@Description", animal.Description);
                com.Parameters.AddWithValue("@Category", animal.Category);
                com.Parameters.AddWithValue("@Area", animal.Area);

                con.Open();
                rowsAffected = com.ExecuteNonQuery();
            }

            return rowsAffected;
        }


    }
}
