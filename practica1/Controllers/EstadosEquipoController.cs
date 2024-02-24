using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica1.Models;

namespace practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosEquipoController : ControllerBase
    {
        private readonly EstadosEquipoContext _estadosEquipoContext;
        public EstadosEquipoController(EstadosEquipoContext estadosEquipoContext)
        {
            _estadosEquipoContext = estadosEquipoContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<EstadosEquipo> ListadoEstadosEquipos = (from e in _estadosEquipoContext.EstadosEquipo select e).ToList();
            if (ListadoEstadosEquipos.Count() == 0)
            {
                return NotFound();

            }
            return Ok(ListadoEstadosEquipos);


        }

        [HttpGet]
        [Route("GetByID/{id}")]
        public IActionResult Get(int id)
        {
            EstadosEquipo? estadosEquipo = (from e in _estadosEquipoContext.EstadosEquipo
                                            where e.id_estados_equipo == id
                                            select e).FirstOrDefault();
            if (estadosEquipo == null)
            {
                return NotFound();
            }
            return Ok(estadosEquipo);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Delete(int id)
        {
            EstadosEquipo? estadosEquipo = (from e in _estadosEquipoContext.EstadosEquipo
                                            where e.id_estados_equipo == id
                                            select e).FirstOrDefault();

            if (estadosEquipo == null)
            {
                return NotFound();
            }
            _estadosEquipoContext.EstadosEquipo.Attach(estadosEquipo);
            _estadosEquipoContext.EstadosEquipo.Remove(estadosEquipo);
            _estadosEquipoContext.SaveChanges();
            return Ok(estadosEquipo);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Add([FromBody] EstadosEquipo estadosEquipo)
        {
            try
            {
                _estadosEquipoContext.EstadosEquipo.Add(estadosEquipo);
                _estadosEquipoContext.SaveChanges();
                return Ok(estadosEquipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActionResultEstadosEquipo(int id, [FromBody] EstadosEquipo EstadosEquipoModificar)
        {
            EstadosEquipo? EstadosEquipoActual = (from e in _estadosEquipoContext.EstadosEquipo
                                            where e.id_estados_equipo == id
                                            select e).FirstOrDefault();
            if (EstadosEquipoActual == null)
            {
                return NotFound();
            }
            EstadosEquipoActual.descripcion = EstadosEquipoModificar.descripcion;
            EstadosEquipoActual.estado = EstadosEquipoModificar.estado;

            _estadosEquipoContext.Entry(EstadosEquipoActual).State = EntityState.Modified;
            _estadosEquipoContext.SaveChanges();
            return Ok(EstadosEquipoActual);

        }

    }
}
