﻿using BackEnd.Data;
using BackEnd.Models.Clases;
using BackEnd.Models.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Net.Mail;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetenciaController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public CompetenciaController(EntidadesDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Competencia>> Get()
        {
            return await _context.Competencias.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Competencia), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var competencia = await _context.Competencias.FindAsync(id);

            return competencia == null ? NotFound() : Ok(competencia);
        }

        [HttpPost("altaCompetencia")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaEquipo(DtCompetencia dtcompetencia)
        {
            Competencia competencia = new Competencia();
            if (dtcompetencia.topeParticipantes < 2) return BadRequest();

            competencia.Area = dtcompetencia.Area;
            competencia.fecha_competencia = dtcompetencia.fecha_competencia;
            competencia.nombre = dtcompetencia.nombre;
            competencia.ligaI = false;
            //competencia.participantes = new List<Participante>();
            competencia.topeParticipantes = dtcompetencia.topeParticipantes;

            await _context.Competencias.AddAsync(competencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = competencia.Id }, competencia);
        }

        [HttpPut("agregarParticipante/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> agregarParticipante(int id, DtParticipante participante)
        {
            var competencia = await _context.Competencias.FindAsync(id);
            //Si la competencia no existe
            if (competencia == null) return BadRequest();

            var compAux = _context.Competencias.Include(e => e.participantes);
            Competencia competencia2 = new Competencia();

            List<Participante> auxList = new List<Participante>();

            foreach (var aux in compAux)
            {
                if (aux.Id == id)
                {
                    competencia2 = aux;
                    auxList = competencia2.participantes;
                    break;
                }
            }

            var part = await _context.Participantes.FindAsync(participante.Id);
            if (part == null) return BadRequest();

            //Comp.ForEachAsync(Console.WriteLine);
            if (auxList.Count == 0)
            {
                competencia.participantes = new List<Participante>();
            }

            if (auxList.Count < competencia.topeParticipantes) 
            {

                competencia.participantes.Add(part);

                _context.Entry(competencia).State = EntityState.Modified;

                part.competencias.Add(competencia);
                _context.Entry(part).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            return BadRequest();
        }

        [HttpGet("mostrarParticipantes/{id}")]
        [ProducesResponseType(typeof(Competencia), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetParticipantesByCompetencia(int id)
        {
            var competencia = _context.Competencias.Include(e => e.participantes);
            Competencia competencia2 = new Competencia();

            List<Participante> auxList = new List<Participante>();

            foreach (var aux in competencia)
            {
                if (aux.Id == id)
                {
                    competencia2 = aux;
                    auxList = competencia2.participantes;
                    break;
                }
            }

            List<DtParticipante> dtPart = new List<DtParticipante>();
            foreach (var dt in auxList)
            {
                DtParticipante part = new DtParticipante();
                part.Id = dt.Id;
                part.nombre = dt.nombre;
                dtPart.Add(part);
            }


            return Ok(dtPart);
        }

        [HttpPut("terminarCompetencia/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> terminarCompetencia(int id)
        {
            var competencia = _context.Competencias.Include(e => e.participantes);
            Competencia competencia2 = new Competencia();

            List<Participante> auxList = new List<Participante>();

            foreach (var aux in competencia)
            {
                if (aux.Id == id)
                {
                    competencia2 = aux;
                    auxList = competencia2.participantes;
                    break;
                }
            }
            var rnd = new Random();
            var randomized = auxList.OrderBy(item => rnd.Next());

            if (randomized.Count() < 2) return BadRequest();

            var competenciaaux = await _context.Competencias.FindAsync(id);
            //Si la competencia no existe
            if (competenciaaux == null) return BadRequest();

            //competenciaaux.posiciones = new List<Nombre>();
            foreach (var aux in randomized)
            {
                Nombre nombre = new Nombre();
                //nombre.Id = aux.Id;
                nombre.nombre = aux.nombre;
                competenciaaux.posiciones.Add(nombre);
                //nombreList.Add(nombre);
            }


            _context.Entry(competenciaaux).State = EntityState.Modified;

            var competenciPuntos =  _context.Competencias.Include(a => a.apuestas);
            foreach (var item in competenciPuntos)
            {
                if (item.Id == id)
                {
                    competencia2 = item;
                    break;
                }
            }

            foreach (var item in competencia2.apuestas)
            {
                var participante = await _context.Participantes.FindAsync(item.idGanador);
                if (participante.nombre == competenciaaux.posiciones.ElementAt(0).nombre)
                {
                    var puntos = await _context.Puntuaciones.FindAsync(item.idPuntuacionUsuario);
                    if (puntos != null)
                    {
                        puntos.puntos+=5;
                        _context.Entry(puntos).State = EntityState.Modified;
                    }
                }
                if (participante.nombre == competenciaaux.posiciones.ElementAt(1).nombre)
                {
                    var puntos = await _context.Puntuaciones.FindAsync(item.idPuntuacionUsuario);
                    if (puntos != null)
                    {
                        puntos.puntos += 3;
                        _context.Entry(puntos).State = EntityState.Modified;
                    }
                }
                if (participante.nombre == competenciaaux.posiciones.ElementAt(2).nombre)
                {
                    var puntos = await _context.Puntuaciones.FindAsync(item.idPuntuacionUsuario);
                    if (puntos != null)
                    {
                        puntos.puntos += 1;
                        _context.Entry(puntos).State = EntityState.Modified;
                    }
                }
            }

            //enviar correo
            //var partido3 = _context.Partidos.Include(p => p.predicciones);
            List<Apuesta> bet = new List<Apuesta>();
            var apuestasUsuarios = _context.Usuario.Include(u => u.apuestas);
            foreach (var competenciaAux in competenciPuntos)
            {
                if (competenciaAux.Id == id)
                {
                    bet = competenciaAux.apuestas;
                }
            }

            foreach (var usuarioAux in apuestasUsuarios)
            {
                foreach (var ApuestaUsu in usuarioAux.apuestas)
                {
                    foreach (var betComp in bet)
                    {
                        if (ApuestaUsu.id == betComp.id)
                        {
                            string texto = "Saludos " + usuarioAux.nombre + ", le avisamos que la competencia en la que ha apostado finalizó.";
                            MailMessage mensaje = new MailMessage("penqueapp@gmail.com", usuarioAux.email, "[PenqueApp] Finalizó la competencia esperada.", texto);
                            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                            smtpClient.EnableSsl = true;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Host = "smtp.gmail.com";
                            smtpClient.Port = 587;
                            smtpClient.Credentials = new System.Net.NetworkCredential("penqueapp@gmail.com", "qwknavxpudbjjtfr");

                            smtpClient.Send(mensaje);
                            smtpClient.Dispose();
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(competenciaaux.posiciones);
        }

        [HttpGet("mostrarResultados/{id}")]
        [ProducesResponseType(typeof(Competencia), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MostrarResultados(int id)
        {
            var competencia = _context.Competencias.Include(c => c.posiciones);
            Competencia competencia2 = new Competencia();

            List<Nombre> auxList = new List<Nombre>();

            foreach (var aux in competencia)
            {
                if (aux.Id == id)
                {
                    competencia2 = aux;
                    auxList = competencia2.posiciones;
                    break;
                }
            }

            List<Nombre> dtPart = new List<Nombre>();
            foreach (var dt in auxList)
            {
                Nombre part = new Nombre();
                part.Id = dt.Id;
                part.nombre = dt.nombre;
                dtPart.Add(part);
            }


            return Ok(dtPart);
        }

        [HttpGet("mostrarParticipantesHabilitados/{id}")]
        [ProducesResponseType(typeof(Competencia), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> mostrarParticipantesHabilitados(int id)
        {
            var competencias = _context.Competencias.Include(e => e.participantes);
            if (competencias == null) return BadRequest("No existe la competencia");
            List<Participante> habilitados = new List<Participante>();
            var participantes = _context.Participantes.ToList();
            foreach (var competencia in competencias)
            {
                if (competencia.Id == id)
                {           
                    foreach (var participante in participantes) 
                    {
                        if (!competencia.participantes.Contains(participante) && participante.Area == competencia.Area)
                        { 
                            habilitados.Add(participante);
                        }

                    }
                    return Ok(habilitados);
                }
            }
            return BadRequest();
        }
    }
}
