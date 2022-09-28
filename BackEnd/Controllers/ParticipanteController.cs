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
    public class ParticipanteController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public ParticipanteController(EntidadesDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Participante>> Get()
        {
            return await _context.Participantes.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Participante), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);

            return participante == null ? NotFound() : Ok(participante);
        }

        [HttpPost("altaParticipante")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> altaParticipante(DtParticipante participante)
        {
            Participante part = new Participante();
            part.Id = participante.Id;
            part.nombre = participante.nombre;

            DbSet<Participante> p = _context.Participantes;
            foreach (var aux in p)
            {
                if (aux.nombre.Equals(participante.nombre)) return BadRequest();
            }

            await _context.Participantes.AddAsync(part);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = participante.Id }, participante);
        }
    }
}
