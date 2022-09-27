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
    public class LigaEquipoController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public LigaEquipoController(EntidadesDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Liga_Equipo>> Get()
        {
            return await _context.Liga_Equipos.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Liga_Equipo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var ligaE = await _context.Liga_Equipos.FindAsync(id);

            return ligaE == null ? NotFound() : Ok(ligaE);
        }

        [HttpPost("altaLigaEquipo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaEquipo(DtLigaEquipo dtLigaEquipo)
        {
            Liga_Equipo ligaE = new Liga_Equipo();

            ligaE.nombreLiga = dtLigaEquipo.nombreLiga;
            ligaE.topePartidos = dtLigaEquipo.tope;

            await _context.Liga_Equipos.AddAsync(ligaE);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ligaE.id }, ligaE);
        }

        [HttpPut("agregarPartido/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> actualizarResultado(int id, DtPartido dtPartido)
        {
            var ligaE = await _context.Liga_Equipos.FindAsync(id);
            if (ligaE == null) return BadRequest("No existe la liga");

            if (ligaE.partidos != null){ 
                if(ligaE.partidos.Count >= ligaE.topePartidos) {    
                    return BadRequest("Ya esta completo todos los slots");
                }
            }
            var partido = await _context.Partidos.FindAsync(dtPartido.Id);
            if (partido == null) return BadRequest("No existe el partido");
            if (ligaE.partidos == null)
            {
                ligaE.partidos = new List<Partido>();
            }
            ligaE.partidos.Add(partido);

            _context.Entry(ligaE).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
