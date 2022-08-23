using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Model;
using System;
using System.Collections.Generic;
using MiPrimeraAPI.Repository;
using System.Linq;
using System.Threading.Tasks;
using MiPrimeraAPI.Controllers.DTOS;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductoController:ControllerBase
    {
        [HttpGet(Name ="GetProductos")]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }

        
        [HttpPost(Name ="PostProductos")]
        public bool altaProductos([FromBody] Producto producto)
        {
            return ProductoHandler.altaProductos(new Producto
            {
                Descripciones = producto.Descripciones,
                Costo = producto.Costo,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock
            });

        }

        
        [HttpPut(Name ="ModificarProductos")]
        public bool ModificarUsuario([FromBody] PutProducto producto)
        {
            return ProductoHandler.ModificarProducto(new Producto
            {
                Id = producto.Id,
                Descripciones = producto.Descripciones
            });
        }


        [HttpDelete (Name ="EliminarProducto")]
        public bool EliminarProducto([FromBody] int id)
        {
            try
            {
                return ProductoHandler.EliminarProducto(id);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
