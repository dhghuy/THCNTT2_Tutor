using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace THCNTT2_Tutor.Pages.Customers
{
    public class Delete : PageModel
    {
        

        public void OnGet()
        {
        }

        public void OnPost(int id)
        {
            deleteCustomers(id);
            Response.Redirect("/Customers/Index");
        }

        private void deleteCustomers(int id)
        {
            try
            {
                string connectionString = "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM customers WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("We have an error: " + ex.Message);
            }
        }
    }
}