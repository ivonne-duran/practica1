using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1.Models;
using Microsoft.EntityFrameworkCore;

namespace practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase

    {
        private readonly equiposContext _equiposContexto;
        public equiposController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }
        ///<sumary>
        /// EndPoint que retorna el liustado de todos los equipos existentes
        /// </sumary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<equipos> ListadoEquipo = (from e in _equiposContexto.equipos select e).ToList();

            if (ListadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(ListadoEquipo);
        }
      
        [HttpGet]
        [Route("GetByID/{id}")]
        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Delete(int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }

            _equiposContexto.equipos.Attach(equipo);
            _equiposContexto.equipos.Remove(equipo);
            _equiposContexto.SaveChanges();

            return Ok(equipo);
        }
        [HttpPost]
        [Route("Add")]

        public IActionResult Add([FromBody] equipos equipo)
        {
            try
            {
                _equiposContexto.equipos.Add(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
            
            public IActionResult ActionResultEquipo(int id, [FromBody]equipos equiposModificar)
        {
            equipos? equipoActual = ( from e in _equiposContexto.equipos
                                      where e.id_equipos == id
                                      select e).FirstOrDefault();
            if(equipoActual == null)
            {
                return NotFound();
            }
            equipoActual.nombre= equiposModificar.nombre;
            equipoActual.descripcion= equiposModificar.descripcion;
            equipoActual.marca_id =equiposModificar.marca_id;
            equipoActual.tipo_equipo_id= equiposModificar.tipo_equipo_id;
            equipoActual.anio_compra= equiposModificar.anio_compra;
            equipoActual.costo=equiposModificar.costo;

            _equiposContexto.Entry(equipoActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();
            return Ok(equiposModificar);
        }
    }
}
