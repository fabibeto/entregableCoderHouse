using Microsoft.Data.SqlClient;
using MiPrimeraAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAPI.Repository
{
    public static class ProductoHandler
    {
        public const string ConnectionString =
            "Server=DESKTOP-P6TBBSQ;" +
            "Initial Catalog=SistemaGestion;" +
            "Trusted_Connection=True";

        //CONSULTA DE PRODUCTOS
        public static List<Producto> GetProductos()
        {
            List<Producto> resultados = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * FROM Producto", sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        //Me aseguro que haya filas
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUSuario"]);


                                resultados.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();

                }
            }
            return resultados;
        }

        //ALTA DE PRODUCTOS
        public static bool altaProductos(Producto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSET INTO [SistemaGestion].[dbo].[Prodcuto]" +
                    "(Descripciones,Costo,PrecioVenta,Stock) VALUES" +
                    "(@descripcionesParameter,@costoParameter,@precioVentaParameter," +
                    "@stockParameter)";

                SqlParameter descripcionesParameter = new SqlParameter("DescripcionesParameter", System.Data.SqlDbType.VarChar) { Value = producto.Descripciones};
                SqlParameter costoParameter = new SqlParameter("CostoParameter", System.Data.SqlDbType.VarChar) { Value = producto.Costo};
                SqlParameter precioVentaParameter = new SqlParameter("PrecioVentaParameter", System.Data.SqlDbType.VarChar) { Value = producto.PrecioVenta};
                SqlParameter stockParameter = new SqlParameter("StockParameter", System.Data.SqlDbType.VarChar) { Value = producto.Stock};
                
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    
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

        //MODIFICAR PRODUCTOS
        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE INTO [SistemaGestion].[dbo].[Producto]" +
                    "SET Descripciones = @descripciones" +
                    "WHERE Id=@id";

                SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = producto.Id };
                SqlParameter descripcionesParameter = new SqlParameter("Descripciones", System.Data.SqlDbType.VarChar) { Value = producto.Descripciones};



                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(descripcionesParameter);


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


        //BORRAR PRODUCTOS
        public static bool EliminarProducto(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE * FROM Producto WHERE Id=@id";

                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
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
