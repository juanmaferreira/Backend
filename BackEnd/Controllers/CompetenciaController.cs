﻿using BackEnd.Data;
using BackEnd.Models.Clases;
using BackEnd.Models.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
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
    }
}
