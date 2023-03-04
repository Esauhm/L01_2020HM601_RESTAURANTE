using L01_2020HM601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020HM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientesController : ControllerBase
    {

        private readonly restauranteContext _restauranteContext;

        public clientesController(restauranteContext restauranteContexto)
        {
            _restauranteContext = restauranteContexto;

        }


        /// <summary>
        /// All registros
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerPedidos()
        {
            List<Clientes> ListadoClientes = (from e in _restauranteContext.clientes
                                           select e).ToList();

            if (ListadoClientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(ListadoClientes);
        }



        /// <summary>
        /// Crear 
        /// </summary>
        /// <param name="ClienteNuevo"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] Clientes ClienteNuevo)
        {
            try
            {
                _restauranteContext.clientes.Add(ClienteNuevo);
                _restauranteContext.SaveChanges();

                return Ok(ClienteNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientesModificar"></param>
        /// <returns></returns>
        /// 

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] Clientes clientesModificar)
        {
            Clientes? clienteExiste = (from e in _restauranteContext.clientes
                                    where e.clienteId == id
                                    select e).FirstOrDefault();
            if (clienteExiste == null)
                return NotFound();

            clienteExiste.nombreCliente = clientesModificar.nombreCliente;
            clienteExiste.direccion = clientesModificar.direccion;


            _restauranteContext.Entry(clienteExiste).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(clienteExiste);
        }



        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminar(int id)
        {
            Clientes? clienteExiste = (from e in _restauranteContext.clientes
                                    where e.clienteId == id
                                    select e).FirstOrDefault();

            if (clienteExiste == null) return NotFound();


            _restauranteContext.clientes.Attach(clienteExiste);
            _restauranteContext.clientes.Remove(clienteExiste);
            _restauranteContext.SaveChanges();

            return Ok(clienteExiste);
        }


        /// <summary>
        /// filtro por palabra en direccion 
        /// </summary>
        /// <param name="palabra"></param>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("GetClienteByPalabraDireccion")]
        public IActionResult filtradoPalabra(string palabra)
        {
            List<Clientes> ListadoClientes = (from e in _restauranteContext.clientes
                                          where e.direccion.Contains(palabra)
                                          select e).ToList();

            if (ListadoClientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(ListadoClientes);
        }




    }
}
