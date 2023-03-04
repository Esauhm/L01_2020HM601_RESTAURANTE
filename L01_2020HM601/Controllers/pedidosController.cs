using L01_2020HM601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020HM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {

        private readonly restauranteContext _restauranteContext;

        public pedidosController(restauranteContext restauranteContexto)
        {
            _restauranteContext = restauranteContexto;

        }

        /// <summary>
        /// All registros de pedidos
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerPedidos()
        {
            List<pedidos> ListadoPedidos = (from e in _restauranteContext.pedidos
                                            select e).ToList();

            if (ListadoPedidos.Count == 0)
            {
                return NotFound();
            }

            return Ok(ListadoPedidos);
        }



        /// <summary>
        /// Crear un nuevo pedido
        /// </summary>
        /// <param name="pedidoNuevo"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] pedidos pedidoNuevo)
        {
            try
            {
                _restauranteContext.pedidos.Add(pedidoNuevo);
                _restauranteContext.SaveChanges();

                return Ok(pedidoNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Actualizar un pedido
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pedidosModificar"></param>
        /// <returns></returns>
        /// 

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] pedidos pedidosModificar)
        {
            pedidos? pedidosExiste = (from e in _restauranteContext.pedidos
                                      where e.pedidoId == id
                                      select e).FirstOrDefault();
            if (pedidosExiste == null)
                return NotFound();

            pedidosExiste.motoristaId = pedidosModificar.motoristaId;
            pedidosExiste.clienteId = pedidosModificar.clienteId;
            pedidosExiste.platoId = pedidosModificar.platoId;
            pedidosExiste.cantidad = pedidosModificar.cantidad;
            pedidosExiste.precio = pedidosModificar.precio;

            _restauranteContext.Entry(pedidosExiste).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(pedidosExiste);
        }



        /// <summary>
        /// Eliminar un pedido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminar(int id)
        {
            pedidos? pedidosExiste = (from e in _restauranteContext.pedidos
                                      where e.pedidoId == id
                                      select e).FirstOrDefault();

            if (pedidosExiste == null) return NotFound();


            _restauranteContext.pedidos.Attach(pedidosExiste);
            _restauranteContext.pedidos.Remove(pedidosExiste);
            _restauranteContext.SaveChanges();

            return Ok(pedidosExiste);
        }



        /// <summary>
        /// filtrar pedidos por cliente 
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("GetPedidoByCliente")]
        public IActionResult pedidosCliente(int clienteId)
        {
            List<pedidos> ListadoPedidos = (from e in _restauranteContext.pedidos
                                            where e.clienteId == clienteId
                                            select e).ToList();

            if (ListadoPedidos.Count == 0)
            {
                return NotFound();
            }

            return Ok(ListadoPedidos);
        }


        /// <summary>
        /// filtrar pedidos por motorista
        /// </summary>
        /// <param name="motoristaId"></param>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("GetPedidoByMotorista")]
        public IActionResult pedidosMotorista(int motoristaId)
        {
            List<pedidos> ListadoPedidos = (from e in _restauranteContext.pedidos
                                            where e.motoristaId == motoristaId
                                            select e).ToList();

            if (ListadoPedidos.Count == 0)
            {
                return NotFound();
            }

            return Ok(ListadoPedidos);
        }


    }
}
