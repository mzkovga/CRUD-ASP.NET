using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace CRUD.Pages.PRODUCTOS
{
    public class IndexModel : PageModel
    {
        public List<ProductoInfo> listProductos = new List<ProductoInfo>();
        public void OnGet()
        {

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=crud;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "SELECT * FROM productos";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                    
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                        
                            while (reader.Read()) 
                            {
                                ProductoInfo productoInfo = new ProductoInfo();
                                productoInfo.id = "" + reader.GetInt32(0);
                                productoInfo.nombre = reader.GetString(1);
                                productoInfo.descripcion = reader.GetString(2);
                                productoInfo.marca = reader.GetString(3);
                                productoInfo.precio = reader.GetDouble(4);
                                productoInfo.stock = reader.GetInt32(5);
                                productoInfo.creado = reader.GetDateTime(6).ToString();

                                listProductos.Add(productoInfo);

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

        public class ProductoInfo
        {
            public string id;
            public string nombre;
            public string descripcion;
            public string marca;
            public double precio;
            public int stock;
            public string creado;
        }



    }
}
