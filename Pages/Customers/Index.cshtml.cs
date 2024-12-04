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
    public class Index : PageModel
    {
        public List<CustomerInfo> CustomersList {get; set;} = [];

        public int SearchId { get; set; }
 
        public void OnGet()
        {
            try {
                string connectionString = "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                using(SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "SELECT * FROM customers ORDER BY id DESC";

                    using(SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                CustomerInfo customerInfo = new CustomerInfo();
                                customerInfo.id = reader.GetInt32(0);
                                customerInfo.name = reader.GetString(1);
                                customerInfo.address = reader.GetString(2);
                                customerInfo.phone = reader.GetString(3);
                                customerInfo.email = reader.GetString(4);

                                CustomersList.Add(customerInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("We have an error: " + ex.Message);
            }
        }
    }

    public class CustomerInfo {
        public int id { get; set; }
        public string name { get; set; } ="";
        public string address { get; set; } ="";
        public string phone { get; set; } ="";
        public string email { get; set; }  ="";
    }
}