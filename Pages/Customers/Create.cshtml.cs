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
    public class Create : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Please enter the name")]
       public string name { get; set; } = "";

       [BindProperty]
        public string? address { get; set; }

        [BindProperty, Phone]
        public string? phone { get; set; }

        [BindProperty, Required, EmailAddress]
        public string email { get; set; } = "";
        public string ErrorMessage { get; private set; } = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if(!ModelState.IsValid) {
                return;
            }

            if(phone == null) phone = "";
            if(address == null) address = "";

            //create a new customer

            try {
                string connectionString = "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                using(SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "INSERT INTO customers" +
                        "(name, address, phone, email) VALUES" +
                        "(@name, @address, @phone, @email);";

                    using(SqlCommand command = new SqlCommand(sql, connection)) {
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
                return;
            }

            Response.Redirect("/Customers/Index");
        }
    }
}