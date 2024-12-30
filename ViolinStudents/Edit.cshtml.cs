using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestApp.Pages.ViolinStudents
{
    public class EditModel : PageModel
    {
        public violinInfo violinInfo = new violinInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ViolinStudents;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                { 
                
                    connection.Open();
                    String sql = "SELECT * FROM students WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                violinInfo.id = "" + reader.GetInt32(0);
                                violinInfo.name = reader.GetString(1);
                                violinInfo.email = reader.GetString(2);
                                violinInfo.age = reader.GetString(3);
                                violinInfo.yearsPlayed = reader.GetString(4);
                            }
                        }
                    }
                
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            violinInfo.id = Request.Form["id"];
            violinInfo.name = Request.Form["name"];
            violinInfo.email = Request.Form["email"];
            violinInfo.age = Request.Form["age"];
            violinInfo.yearsPlayed = Request.Form["yearsPlayed"];


            if (violinInfo.id.Length == 0 || violinInfo.name.Length == 0 || violinInfo.email.Length == 0
      || violinInfo.age.Length == 0 || violinInfo.yearsPlayed.Length == 0)
            {
                errorMessage = "All fields are required";
                return;

            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ViolinStudents;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Editing client -> sql update
                    connection.Open();
                    // add new client into sql database
                    String sql = "UPDATE students " +
                                  "SET name=@name, email=@email, age=@age, yearsPlayed=@yearsPlayed " + // Added space before WHERE
                                  "WHERE id=@id";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", violinInfo.name);
                        command.Parameters.AddWithValue("@email", violinInfo.email);
                        command.Parameters.AddWithValue("@age", violinInfo.age);
                        command.Parameters.AddWithValue("@yearsPlayed", violinInfo.yearsPlayed);
                        command.Parameters.AddWithValue("@id", violinInfo.id);
                        command.ExecuteNonQuery();
                    }

                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/ViolinStudents/Index");
        }
    }
}

