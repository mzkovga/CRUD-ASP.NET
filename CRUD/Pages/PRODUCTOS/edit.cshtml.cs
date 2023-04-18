using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data.SqlClient;
using static CRUD.Pages.PRODUCTOS.IndexModel;


namespace CRUD.Pages.PRODUCTOS
{
    public class editModel : PageModel
    {
        public ProductoInfo productoInfo = new ProductoInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=crud;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM productos WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                productoInfo.id = "" + reader.GetInt32(0); //probar en la validacion
                                productoInfo.nombre = reader.GetString(1);
                                productoInfo.descripcion = reader.GetString(2);
                                productoInfo.marca = reader.GetString(3);
                                productoInfo.precio = reader.GetDouble(4);
                                productoInfo.stock = reader.GetInt32(5);

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

            productoInfo.id = Request.Form["id"];
            productoInfo.nombre = Request.Form["nombre"];
            productoInfo.descripcion = Request.Form["descripcion"];
            productoInfo.marca = Request.Form["marca"];
            //productoInfo.precio = Request.Form["precio"];
            //productoInfo.stock = Request.Form["stock"];

            if (double.TryParse(Request.Form["precio"], out double precio))
            {
                productoInfo.precio = precio;
            }
            else
            {
                // Manejo error
            }

            if (int.TryParse(Request.Form["stock"], out int stock))
            {
                productoInfo.stock = stock;
            }
            else
            {
                // Manejo error
            }

            if (productoInfo.nombre.Length == 0)
            {
                errorMessage = "El campo Nombre es requerido";
                return;
            }


            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=crud;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE PRODUCTOS " +
                                 "SET nombre=@nombre, descripcion=@descripcion, marca=@marca, precio=@precio, stock=@stock " +
                                 "WHERE id=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@nombre", productoInfo.nombre);
                        cmd.Parameters.AddWithValue("@descripcion", productoInfo.descripcion);
                        cmd.Parameters.AddWithValue("@marca", productoInfo.marca);
                        cmd.Parameters.AddWithValue("@precio", productoInfo.precio);
                        cmd.Parameters.AddWithValue("@stock", productoInfo.stock);
                        cmd.Parameters.AddWithValue("@id", productoInfo.id);

                        cmd.ExecuteNonQuery();

                        connection.Close();
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
                
            }

            Response.Redirect("/PRODUCTOS/Index");

        }



    }
}
