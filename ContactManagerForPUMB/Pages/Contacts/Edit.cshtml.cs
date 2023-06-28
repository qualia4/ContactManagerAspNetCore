using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace ContactManagerForPUMB.Contacts
{
    public class EditModel : PageModel
    {
        public Contact contact = new Contact();
        public string successMessage = "";
        public string errorMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string connectionName = "Data Source=WIN-6TSL2R0LRG9\\SQLEXPRESS;Initial Catalog=contactsdata;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "SELECT * FROM clients WHERE id=@id;";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@id", id);
                        using SqlDataReader reader = sqlCommand.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                contact.Id = reader.GetInt32(0);
                                contact.Name = reader.GetString(1);
                                contact.Surname = reader.GetString(2);
                                contact.Email = reader.GetString(3);
                                contact.Phone = reader.GetString(4);
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
            contact.Id = Convert.ToInt32(Request.Form["id"]);
            contact.Name = Request.Form["name"];
            contact.Surname = Request.Form["surname"];
            contact.Email = Request.Form["email"];
            contact.Phone = Request.Form["phone"];
            if (contact == null)
            {
                errorMessage = "Something went wrong";
                return;
            }
            try
            {
                string connectionName = "Data Source=WIN-6TSL2R0LRG9\\SQLEXPRESS;Initial Catalog=contactsdata;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "UPDATE clients SET name=@name, surname=@surname, email=@email, phone=@phone WHERE id=@id";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@name", contact.Name);
                        sqlCommand.Parameters.AddWithValue("@surname", contact.Surname);
                        sqlCommand.Parameters.AddWithValue("@email", contact.Email);
                        sqlCommand.Parameters.AddWithValue("@phone", contact.Phone);
                        sqlCommand.Parameters.AddWithValue("@id", contact.Id);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Contacts/Main");
        }
    }
}
