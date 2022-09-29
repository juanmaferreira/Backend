using BackEnd.Data;
using BackEnd.Models.Clases;
using BackEnd.Models.DataTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Equipo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            DtEquipo dtequipo = new DtEquipo();
            var equipo = await _context.Equipos.FindAsync(id);
            dtequipo.Id = id;
            dtequipo.Name = equipo.nombreEquipo;

            return equipo == null ? NotFound() : Ok(dtequipo);
        }

        [HttpPost("altaEquipo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> altaEquipo(DtEquipo equipo)
        {
            Equipo team = new Equipo();
            team.nombreEquipo = equipo.Name;
            
            await _context.Equipos.AddAsync(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = team.id }, equipo);
        }

        [HttpPut("agregarHistorial/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> actualizarHistorial(int id, [FromBody] Tipo_Historial tipo)
        {
            var team = await _context.Equipos.FindAsync(id);
            if (team == null) return BadRequest("El equipo ingresado es NULL");

            Historial historial = new Historial();
            historial.tipo_Historial = tipo;
            if (team.historiales == null)
            {
                team.historiales = new List<Historial>();
            }
            team.historiales.Add(historial);

            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("mostrarHistorial/{id}")]
        [ProducesResponseType(typeof(Equipo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEquipoById(int id)
        {
            var equipo = _context.Equipos.Include(e => e.historiales);
            Equipo equipo2 = new Equipo();
            foreach(var aux in equipo)
            {
                if(aux.id == id)
                {
                    equipo2 = aux;
                    break;
                }
            }

            List<Historial> historial = new List<Historial>();
            List<Historial> auxList = equipo2.historiales;
            auxList.Reverse();
            int count = 0;
            foreach (var auxH in auxList) { 
                historial.Add(auxH);
                count++;
                if(count == 5)
                {
                    break;
                }
            }

            return Ok(historial);
        }

        
    }

    

}
