using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace TestApp.Pages.ViolinStudents
{
    public class IndexModel : PageModel
    {
        public List<violinInfo> listStudents = new List<violinInfo>();
        public void OnGet()
        {
            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ViolinStudents;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM students";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                violinInfo info = new violinInfo();
                                info.id = "" + reader.GetInt32(0);
                                info.name = reader.GetString(1);
                                info.email = reader.GetString(2);
                                info.age = reader.GetString(3);
                                info.yearsPlayed = reader.GetString(4);
                                info.createdAt = reader.GetDateTime(5).ToString();

                                listStudents.Add(info);

                            }

                        }
                    }

                }
            }
            catch(Exception ex)
                { 

                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class violinInfo
    {

        public String id;
        public String name;
        public String email;
        public String age;
        public String yearsPlayed;
        public String createdAt;
    
    }
}
