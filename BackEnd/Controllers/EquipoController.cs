using BackEnd.Data;
using BackEnd.Models.Clases;
using BackEnd.Models.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public EquipoController(EntidadesDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Equipo>> Get()
        {
            return await _context.Equipos.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Equipo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            Console.Out.WriteLine(equipo.historiales);

            return equipo == null ? NotFound() : Ok(equipo.historiales);
        }

        [HttpPost("/altaEquipo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> altaEquipo(Equipo equipo)
        {
            
            await _context.Equipos.AddAsync(equipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = equipo.id }, equipo);
        }

        [HttpPut("/agregarHistorial/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> actualizarHistorial(int id, Tipo_Historial tipo)
        {
            var team = await _context.Equipos.FindAsync(id);
            if (team == null) return BadRequest();

            for(var i = 4; 0 < i; i--)
            {
                team.historiales[i] = team.historiales[i-1];
            }
            team.historiales[0] = tipo;


           
            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    

}
