﻿using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet(Name = "GetProductoVendido")]
        public List<PostProductoVendido> GetProductoVendido()
        {
            return ProductoVendidoHandler.GetProductoVendido();
        }

        
        [HttpPost(Name = "AltaProductoVendido")]
        public bool CrearProductoVendido([FromBody] PostProductoVendido productovendido)
        {
            return ProductoVendidoHandler.CrearProductoVendido(new PostProductoVendido
            {
                Stock = productovendido.Stock
            });

        }

        //[HttpPut(Name = "ModificarUsuario")]
        //public bool ModificarUsuario([FromBody] PutUsuario usuario)
        //{
        //    return UsuarioHandler.ModificarUSuario(new Usuario
        //    {
        //        Id = usuario.Id,
        //        Nombre = usuario.Nombre
        //    });
        //}


        //[HttpDelete(Name = "BorrarUsuarios")]
        //public bool EliminarUsuario([FromBody] int id)
    }
}
