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
    public class LigaIndividualController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public LigaIndividualController(EntidadesDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Liga_Individual>> Get()
        {
            return await _context.Liga_Individuales.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Liga_Individual), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var ligaI = await _context.Liga_Individuales.FindAsync(id);
            Console.Out.WriteLine(ligaI.competencias);

            return ligaI == null ? NotFound() : Ok(ligaI);
        }

        [HttpPost("altaLigaIndividual")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> altaLigaI(DtLiga_Individual ligaI)
        {
            Liga_Individual liga = new Liga_Individual();
            liga.Nombre = ligaI.Nombre;
            liga.topeCompetencias = ligaI.topeCompetencias;
            await _context.Liga_Individuales.AddAsync(liga);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ligaI.Id }, ligaI);
        }
        
        [HttpPut("agregarCompetencia")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> agregarCompetencia(int idLiga, int idCompetencia)
        {
            var ligaI = await _context.Liga_Individuales.FindAsync(idLiga);
            if (ligaI == null) return BadRequest();

            var ligaIAux = _context.Liga_Individuales.Include(l => l.competencias);
            
            Liga_Individual liga2 = new Liga_Individual();

            List<Competencia> comp = new List<Competencia>();

            foreach(var aux in ligaIAux)
            {
                if (aux.Id == idLiga)
                {
                    liga2 = aux;
                    comp = liga2.competencias;
                    break;

                }
            }

            var competencia = await _context.Competencias.FindAsync(idCompetencia);
            
            if (competencia == null) return BadRequest();

            if(comp.Count == 0)
            {
                ligaI.competencias = new List<Competencia>();
            }
            if (competencia.ligaI) return BadRequest();
            if(comp.Count < ligaI.topeCompetencias)
            {
                competencia.ligaI = true;
                ligaI.competencias.Add(competencia);
                _context.Entry(ligaI).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpGet("mostrarCompetencias/{id}")]
        [ProducesResponseType(typeof(Equipo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCompetenciasByLiga(int id)
        {
            var ligaI = _context.Liga_Individuales.Include(l => l.competencias);
            Liga_Individual ligaI2 = new Liga_Individual();

            foreach(var aux in ligaI)
            {
                if(aux.Id == id)
                {
                    ligaI2 = aux;
                    break;
                }
            }
            List<DtCompetencia> dtcomp = new List<DtCompetencia>();
            foreach(var dt in ligaI2.competencias)
            {
                DtCompetencia comp = new DtCompetencia();
                comp.Id = dt.Id;
                comp.nombre = dt.nombre;
                comp.Area = dt.Area;
                comp.topeParticipantes = dt.topeParticipantes;
                comp.fecha_competencia = dt.fecha_competencia;
                dtcomp.Add(comp);
            }
            return Ok(dtcomp);
        }
    }
}
