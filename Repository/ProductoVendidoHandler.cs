using Microsoft.Data.SqlClient;
using MiPrimeraAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAPI.Repository
{
    public class ProductoVendidoHandler
    {
        public const string ConnectionString =
            "Server=DESKTOP-P6TBBSQ;" +
            "Initial Catalog=SistemaGestion;" +
            "Trusted_Connection=True";

        //CONSULTA DE PRODUCTO VENDIDO
        public static List<PostProductoVendido> GetProductoVendido()
        {
            List<PostProductoVendido> resultados = new List<PostProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * FROM ProductoVendido", sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        //Me aseguro que haya filas
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                PostProductoVendido productoVendido = new PostProductoVendido();

                                productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
                                
                                resultados.Add(productoVendido);
                            }
                        }
                    }
                    sqlConnection.Close();

                }
            }
            return resultados;
        }

        //CREAR PRODUCTOVENDIDO
        public static bool CrearProductoVendido(PostProductoVendido productovendido)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSET INTO [SistemaGestion].[dbo].[ProductoVendido]" +
                    "(Stock) VALUES" +
                    "(@stockParameter)";

                SqlParameter IdParameter = new SqlParameter("IdParameter", System.Data.SqlDbType.VarChar) { Value = productovendido.Id };
                SqlParameter StockParameter = new SqlParameter("StockParameter", System.Data.SqlDbType.VarChar) { Value =productovendido.Stock };
                
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(IdParameter);
                    sqlCommand.Parameters.Add(StockParameter);
      
                   int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }
    }
}
