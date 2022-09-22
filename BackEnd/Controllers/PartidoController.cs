using BackEnd.Data;
using BackEnd.Models.Clases;
using BackEnd.Models.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidoController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public PartidoController(EntidadesDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Partido>> Get()
        {
            return await _context.Partidos.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Partido), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var partido = await _context.Partidos.FindAsync(id);

            return partido == null ? NotFound() : Ok(partido.visitante_local);
        }

        [HttpPost("/altaPartido")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaEquipo(DtPartido dtpartido)
        {
            Partido partido = new Partido();
            Equipo? local = await _context.Equipos.FindAsync(dtpartido.local.id);
            Equipo? visitante = await _context.Equipos.FindAsync(dtpartido.visitante.id);

            if (local == null || visitante == null) return BadRequest();

            partido.fechaPartido = dtpartido.fecha;
            partido.asignarEquipos(local, visitante);




            await _context.Partidos.AddAsync(partido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = partido.id }, partido);
        }
    }
}
