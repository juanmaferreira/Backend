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
    public class PencaController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public PencaController(EntidadesDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Penca>> Get()
        {
            return await _context.Pencas.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Penca), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var pencas = await _context.Pencas.FindAsync(id);

            return pencas == null ? NotFound() : Ok(pencas);
        }

        [HttpPost("altaPencaCompartida/equipo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaPencaCompartidaEquipo(DtPencasCompartida dtPC)
        {
            Penca penca = new Penca();

            penca.nombre = dtPC.nombre;
            penca.tipo_Deporte = dtPC.tipoDeporte;
            penca.tipo_Penca = Tipo_Penca.Compartida;
            penca.entrada = dtPC.entrada;
            penca.pozo = 0;
            penca.fecha_Creacion = new DateTime();
            penca.estado = true;
            penca.color = "";
            penca.tipo_Liga = Tipo_Liga.Equipo;
            penca.liga_Individual = null;
            var LigaE = await _context.Liga_Equipos.FindAsync(dtPC.idLiga);
            if (LigaE == null) return BadRequest("No existe la liga");
            
            penca.liga_Equipo = LigaE;
            if(LigaE.pencas == null) { LigaE.pencas = new List<Penca>(); }
            LigaE.pencas.Add(penca);
            await _context.Pencas.AddAsync(penca);
            await _context.SaveChangesAsync();

            return Ok(dtPC);
        }

        [HttpPost("altaPencaCompartida/individual")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaPencaCompartidaIndividual(DtPencasCompartida dtPC)
        {
            Penca penca = new Penca();

            penca.nombre = dtPC.nombre;
            penca.tipo_Deporte = dtPC.tipoDeporte;
            penca.tipo_Penca = Tipo_Penca.Compartida;
            penca.entrada = dtPC.entrada;
            penca.pozo = 0;
            penca.fecha_Creacion = new DateTime();
            penca.estado = true;
            penca.color = "";
            penca.tipo_Liga = Tipo_Liga.Individual;
            penca.liga_Equipo = null;
            var LigaI = await _context.Liga_Individuales.FindAsync(dtPC.idLiga);
            if (LigaI == null) return BadRequest("No existe la liga");

            penca.liga_Individual = LigaI;
            if (LigaI.pencas == null) { LigaI.pencas = new List<Penca>(); }
            LigaI.pencas.Add(penca);
            await _context.Pencas.AddAsync(penca);
            await _context.SaveChangesAsync();

            return Ok(dtPC);
        }

        [HttpPost("altaPencaEmpresa/equipo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaPencaEmpresa(DtPencaEmpresa dtPE)
        {
            Penca penca = new Penca();

            penca.nombre = dtPE.nombre;
            penca.tipo_Deporte = dtPE.tipoDeporte;
            penca.tipo_Penca = Tipo_Penca.Empresa;
            penca.entrada = dtPE.entrada;
            penca.pozo = dtPE.premioFinal;
            penca.fecha_Creacion = new DateTime();
            penca.estado = true;
            penca.color = "";
            penca.tipo_Liga = Tipo_Liga.Equipo;
            penca.liga_Individual = null;

            var LigaE = await _context.Liga_Equipos.FindAsync(dtPE.idLiga);
            var empresa = await _context.Empresas.FindAsync(dtPE.idEmpresa);
            if (LigaE == null) return BadRequest("No existe la liga");
            if (empresa == null) return BadRequest("No existe la empresa");

            if (LigaE != null)
            {
                penca.liga_Equipo = LigaE;
                if (LigaE.pencas == null)
                {
                    LigaE.pencas = new List<Penca>();
                }
                LigaE.pencas.Add(penca);
                
            }

            await _context.Pencas.AddAsync(penca);
            
            if(empresa.pencas_empresa == null)
            {
                empresa.pencas_empresa = new List<Penca>();
            }
            empresa.pencas_empresa.Add(penca);
            await _context.SaveChangesAsync();

            return Ok(dtPE);
        }

        [HttpPost("altaPencaEmpresa/individual")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaPencaEmpresaIndividual(DtPencaEmpresa dtPE)
        {
            Penca penca = new Penca();

            penca.nombre = dtPE.nombre;
            penca.tipo_Deporte = dtPE.tipoDeporte;
            penca.tipo_Penca = Tipo_Penca.Empresa;
            penca.entrada = dtPE.entrada;
            penca.pozo = dtPE.premioFinal;
            penca.fecha_Creacion = new DateTime();
            penca.estado = true;
            penca.color = "";
            penca.tipo_Liga = Tipo_Liga.Equipo;
            penca.liga_Equipo = null;

            var LigaI = await _context.Liga_Individuales.FindAsync(dtPE.idLiga);
            var empresa = await _context.Empresas.FindAsync(dtPE.idEmpresa);
            if (LigaI == null) return BadRequest("No existe la liga");
            if (empresa == null) return BadRequest("No existe la empresa");

            if (LigaI != null)
            {
                penca.liga_Individual = LigaI;
                if (LigaI.pencas == null)
                {
                    LigaI.pencas = new List<Penca>();
                }
                LigaI.pencas.Add(penca);

            }

            await _context.Pencas.AddAsync(penca);

            if (empresa.pencas_empresa == null)
            {
                empresa.pencas_empresa = new List<Penca>();
            }
            empresa.pencas_empresa.Add(penca);
            await _context.SaveChangesAsync();

            return Ok(dtPE);
        }


        [HttpGet("verForo/{id}")]
        [ProducesResponseType(typeof(Penca), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMensajesForo(int id)
        {
            var penca = _context.Pencas.Include(e => e.foro);
            List<string> auxList = new List<string>();

            foreach (var aux in penca)
            {
                if (aux.id == id)
                {
                    foreach (var foros in aux.foro)
                    {
                        auxList.Add(foros.mensaje);
                    }
                    return Ok(auxList);
                }
            }
            return BadRequest("No existe la Penca");
        }

        [HttpPut("chequearLigaEquipoFinalizada/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> chequearLigaEquipoFinalizada(int id)
        {
            var penca = await _context.Pencas.FindAsync(id);
            if (penca == null) return BadRequest("No existe la Penca");
            penca.chequearEstadoLigaIndividual();
            _context.Entry(penca).State = EntityState.Modified;
            return NoContent();
        }

        [HttpPut("chequearLigaIndividualFinalizada/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> chequearLigaIndividualFinalizada(int id)
        {
            var penca = await _context.Pencas.FindAsync(id);
            if (penca == null) return BadRequest("No existe la Penca");
            penca.chequearEstadoLigaIndividual();
            _context.Entry(penca).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
