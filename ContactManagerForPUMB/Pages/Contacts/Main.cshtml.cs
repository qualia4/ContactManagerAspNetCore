using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ContactManagerForPUMB.Contacts
{
    public class IndexModel : PageModel
    {
        public List<Contact> contactsList = new List<Contact>();

        public void OnGet()
        {
            try
            {
                string connectionName = "Data Source=WIN-6TSL2R0LRG9\\SQLEXPRESS;Initial Catalog=contactsdata;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "SELECT * FROM clients";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        using SqlDataReader reader = sqlCommand.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string name = reader.GetString(1);
                                string surname = reader.GetString(2);
                                string email = reader.GetString(3);
                                string phone = reader.GetString(4);

                                Contact contact = new Contact(id, name, surname, email, phone);
                                contactsList.Add(contact);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }
    }
}
