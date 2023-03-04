using L01_2020HM601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020HM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {

        private readonly restauranteContext _restauranteContext;

        public platosController(restauranteContext restauranteContexto)
        {
            _restauranteContext = restauranteContexto;

        }


        /// <summary>
        /// All registros de platos
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerPedidos()
        {
            List<platos> ListadoPedidos = (from e in _restauranteContext.platos
                                            select e).ToList();

            if (ListadoPedidos.Count == 0)
            {
                return NotFound();
            }

            return Ok(ListadoPedidos);
        }



        /// <summary>
        /// Crear plato
        /// </summary>
        /// <param name="platoNuevo"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] platos platoNuevo)
        {
            try
            {
                _restauranteContext.platos.Add(platoNuevo);
                _restauranteContext.SaveChanges();

                return Ok(platoNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Actualizar platos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="platosModificar"></param>
        /// <returns></returns>
        /// 

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] platos platosModificar)
        {
            platos? platosExiste = (from e in _restauranteContext.platos
                                      where e.platoId == id
                                      select e).FirstOrDefault();
            if (platosExiste == null)
                return NotFound();

            platosExiste.nombrePlato = platosModificar.nombrePlato;
            platosExiste.precio = platosModificar.precio;


            _restauranteContext.Entry(platosExiste).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(platosExiste);
        }



        /// <summary>
        /// Eliminar platos
        /// /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminar(int id)
        {
            platos? platosExiste = (from e in _restauranteContext.platos
                                      where e.platoId == id
                                      select e).FirstOrDefault();

            if (platosExiste == null) return NotFound();


            _restauranteContext.platos.Attach(platosExiste);
            _restauranteContext.platos.Remove(platosExiste);
            _restauranteContext.SaveChanges();

            return Ok(platosExiste);
        }


        /// <summary>
        /// filtro por una palabra en el nombre del cliente 
        /// </summary>
        /// <param name="palabra"></param>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("GetPlatoByPalabraNombre")]
        public IActionResult filtradoPalabra(string palabra)
        {
            List<platos> ListadoPlatos = (from e in _restauranteContext.platos
                                            where e.nombrePlato.Contains(palabra)
                                            select e).ToList();

            if (ListadoPlatos.Count == 0)
            {
                return NotFound();
            }

            return Ok(ListadoPlatos);
        }

    }
}
