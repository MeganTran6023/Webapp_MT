using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace TestApp.Pages.ViolinStudents
{
    public class CreateModel : PageModel
    {
        public violinInfo violinInfo = new violinInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // user inputs
            violinInfo.name = Request.Form["name"];
            violinInfo.email = Request.Form["email"];
            violinInfo.age = Request.Form["age"];
            violinInfo.yearsPlayed = Request.Form["yearsPlayed"];

            if (violinInfo.name.Length == 0 || violinInfo.email.Length == 0
                || violinInfo.age.Length == 0 || violinInfo.yearsPlayed.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            // save new client into database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ViolinStudents;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    // add new client into sql database
                    String sql = "INSERT INTO students " +
                                 "(name, email, age, yearsPlayed) VALUES " +
                                 "(@name, @email, @age, @yearsPlayed);";

                    using (SqlCommand command = new SqlCommand(sql, connection))  // Use SqlCommand instead of SqlConnection here
                    {
                        command.Parameters.AddWithValue("@name", violinInfo.name);
                        command.Parameters.AddWithValue("@email", violinInfo.email);
                        command.Parameters.AddWithValue("@age", violinInfo.age);
                        command.Parameters.AddWithValue("@yearsPlayed", violinInfo.yearsPlayed);

                        command.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = "Error adding Client: " + ex.Message;
                return;
            }

            violinInfo.name = "";
            violinInfo.email = "";
            violinInfo.age = "";
            violinInfo.yearsPlayed = "";
            successMessage = "New Student Successfully Added.";

            //return success add message
            Response.Redirect("/ViolinStudents/Index");

        }
    }
}