using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace THCNTT2_Tutor.Pages.Customers
{
    public class Edit : PageModel
    {
        [BindProperty]
       public int id { get; set; }

        [BindProperty, Required(ErrorMessage = "Please enter the name")]
       public string name { get; set; } = "";

       [BindProperty]
        public string? address { get; set; }

        [BindProperty, Phone]
        public string? phone { get; set; }

        [BindProperty, Required, EmailAddress]
        public string email { get; set; } = "";
        public string ErrorMessage { get; private set; } = "";

        public void OnGet(int id)
        {
            try {
                string connectionString = "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                using(SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "SELECT * FROM customers WHERE id = @id";

                    using(SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                this.id = reader.GetInt32(0);
                                name = reader.GetString(1);
                                address = reader.GetString(2);
                                phone = reader.GetString(3);
                                email = reader.GetString(4);
                            }
                            else {
                                Response.Redirect("/Customers/Index");
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                ErrorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            if(!ModelState.IsValid) {
                return;
            }

            if(phone == null) phone = "";
            if(address == null) address = "";

            try {
                string connectionString = "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                using(SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "UPDATE customers SET name = @name, address = @address, phone = @phone, email = @email WHERE id = @id";

                    using(SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@address", address);
                        command.Parameters.AddWithValue("@phone", phone);
                        command.Parameters.AddWithValue("@email", email);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex) {
                ErrorMessage = ex.Message;
            }

            Response.Redirect("/Customers/Index");
        }
    }
}