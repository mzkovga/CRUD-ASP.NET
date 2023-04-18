using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
//Agrego clase Modelo ProductoInfo
using static CRUD.Pages.PRODUCTOS.IndexModel;

namespace CRUD.Pages.PRODUCTOS
{
    public class createModel : PageModel
    {
        
        public ProductoInfo productoInfo = new ProductoInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost() 
        {
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

              //if (productoInfo.nombre.Length == 0 || agregar otros campos)

            if (productoInfo.nombre.Length == 0)
            {
                errorMessage = "El campo Nombre es requerido";
                return;
            }

            //Guardando datos

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=crud;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO PRODUCTOS " +
                                 "(nombre, descripcion, marca, precio, stock) VALUES " +
                                 "(@nombre,@descripcion,@marca,@precio,@stock);";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@nombre", productoInfo.nombre);
                        cmd.Parameters.AddWithValue("@descripcion", productoInfo.descripcion);
                        cmd.Parameters.AddWithValue("@marca", productoInfo.marca);
                        cmd.Parameters.AddWithValue("@precio", productoInfo.precio);
                        cmd.Parameters.AddWithValue("@stock", productoInfo.stock);

                        cmd.ExecuteNonQuery();
                        
                        connection.Close(); 
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
         
            }

            productoInfo.nombre = "";
            productoInfo.descripcion = "";
            productoInfo.marca = "";
            productoInfo.precio=0;
            productoInfo.stock=0;

            successMessage = "Nuevo Producto Agregado Correctamente";

            Response.Redirect("/PRODUCTOS/Index");

        }

    }
}
