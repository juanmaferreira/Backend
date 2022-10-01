using BackEnd.Data;
using BackEnd.Models.Clases;
using BackEnd.Models.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;

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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Partido), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var partido = await _context.Partidos.FindAsync(id);
            DtPartido dtpartido = new DtPartido();
            dtpartido.fecha = partido.fechaPartido;
            dtpartido.resultado = partido.resultado;

            return partido == null ? NotFound() : Ok(dtpartido);
        }

        [HttpPost("altaPartido")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaEquipo(DtPartido dtpartido)
        {
            Partido partido = new Partido();
            Equipo? local = await _context.Equipos.FindAsync(dtpartido.Idlocal);
            Equipo? visitante = await _context.Equipos.FindAsync(dtpartido.Idvisitante);

            if (local == null || visitante == null) return BadRequest("Uno de los equipos ingresados es NULL");
            if (local.id == visitante.id) return BadRequest("No puedes elejir que compita contra si mismo");

            partido.fechaPartido = dtpartido.fecha;
            partido.visitante_local = new List<Equipo> {visitante, local};
            partido.enUso = false;
            partido.resultado = Tipo_Resultado.Indefinido;

            if(local.partidos == null)
            {
                local.partidos = new List<Partido>();
            }
            local.partidos.Add(partido);
            if (visitante.partidos == null)
            {
                visitante.partidos = new List<Partido>();
            }
            visitante.partidos.Add(partido);

            await _context.Partidos.AddAsync(partido);
            _context.Entry(visitante).State = EntityState.Modified;
            _context.Entry(local).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = dtpartido.Id }, dtpartido);
        }

        [HttpGet("EquiposGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEquipoById(int id)
        {
            var partido =  _context.Partidos.Include(p => p.visitante_local);
            List<DtEquipo> equipoList = new List<DtEquipo>();
            foreach(var item in partido)
            {
                if(item.id == id)
                {
                    foreach (var aux in item.visitante_local)
                    {
                        DtEquipo equipo = new DtEquipo();
                        equipo.Id = aux.id;
                        equipo.Name = aux.nombreEquipo;
                        equipoList.Add(equipo);
                    }
                    return Ok(equipoList);
                }
            }
          
            return  NotFound();
        }

        [HttpPut("actualizarResultado/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> actualizarResultado(int id)
        {

            var partido = await _context.Partidos.FindAsync(id);
            if (partido == null) return BadRequest("No existe el partido");

            var random = new Random();
            partido.resultado = (Tipo_Resultado)random.Next(Enum.GetNames(typeof(Tipo_Resultado)).Length -1);

            _context.Entry(partido).State = EntityState.Modified;

            var partido2 = _context.Partidos.Include(p => p.visitante_local);

            Partido play = new Partido();
            foreach (var item in partido2)
            {
                if (item.id == id)
                {
                    play = item;
                    break;
                }
            }
            if(play.resultado == Tipo_Resultado.Empate) {
                play.visitante_local.ElementAt(0).agregarHistorial(Tipo_Historial.Empato);
                play.visitante_local.ElementAt(1).agregarHistorial(Tipo_Historial.Empato);
            }
            if(play.resultado == Tipo_Resultado.Local)
            {
                play.visitante_local.ElementAt(0).agregarHistorial(Tipo_Historial.Perdio);
                play.visitante_local.ElementAt(1).agregarHistorial(Tipo_Historial.Gano);
            }
            if (play.resultado == Tipo_Resultado.Visitante)
            {
                play.visitante_local.ElementAt(0).agregarHistorial(Tipo_Historial.Gano);
                play.visitante_local.ElementAt(1).agregarHistorial(Tipo_Historial.Perdio);
            }

            _context.Entry(play).State = EntityState.Modified;
            

            //SumarPuntos();
            var partido3 = _context.Partidos.Include(p => p.predicciones);
            foreach (var item in partido3)
            {
                if (item.id == id)
                {
                    play = item;
                    break;
                }
            }
 
            foreach (var item in play.predicciones)
            {
                if(item.tipo_Resultado == partido.resultado)
                {
                    var puntos = await _context.Puntuaciones.FindAsync(item.idPuntuacionUsuario);
                    if(puntos != null)
                    {
                        puntos.puntos++;
                        _context.Entry(puntos).State = EntityState.Modified;
                    }
                }
            }


            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("estadisticasPartido/{id}")]
        [ProducesResponseType(typeof(Partido), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> estadisticasPartido(int id)
        {
            var partidos =  _context.Partidos.Include(p => p.predicciones);
            if (partidos == null) return BadRequest("No hay partidos  en la lista");
            Partido partido = new Partido();
            foreach(var aux in partidos){ 
                if(aux.id == id)
                {
                    partido = aux;
                    break;
                }
            }
            int visitante = 0;
            int local = 0;
            int empate = 0;

            foreach(var aux2 in partido.predicciones)
            {
                if(aux2.tipo_Resultado == Tipo_Resultado.Local)
                {
                    local++;
                }
                if (aux2.tipo_Resultado == Tipo_Resultado.Visitante)
                {
                    visitante++;
                }
                if (aux2.tipo_Resultado == Tipo_Resultado.Empate)
                {
                    empate++;
                }
            }
            DtEstadisticas dtE = new DtEstadisticas();
            dtE.local = local;
            dtE.visitante = visitante;
            dtE.empate = empate;

            return Ok(dtE);
        }
    }
}
