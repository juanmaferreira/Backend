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
    public class AdminController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public AdminController(EntidadesDbContext context) => _context = context;

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {

            var admin = await _context.Administradores.FindAsync(id);
            if (admin == null) return BadRequest("No existe el administrador");

            DtAdmin dtAdmin = new DtAdmin();
            dtAdmin.Name = admin.nombre;
            dtAdmin.Id = admin.Id;
            dtAdmin.Billetera = admin.billetera;
            dtAdmin.tipo_rol = admin.Tipo_Rol;
            dtAdmin.Email = admin.email;
            dtAdmin.Password = admin.password;

            return admin == null ? NotFound() : Ok(dtAdmin);
        }

        [HttpPost("altaAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaAdmin(DtAdmin dtAdmin)
        {

            DbSet<Usuario> usuarios = _context.Usuario;
            DbSet<Empresa> empresas = _context.Empresas;
            DbSet<Administrador> administradores = _context.Administradores;
            DbSet<SuperAdmin> superadmins = _context.SuperAdmins;
            foreach (var aux in empresas)
            {
                if (aux.email == dtAdmin.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in usuarios)
            {
                if (aux.email == dtAdmin.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in administradores)
            {
                if (aux.email == dtAdmin.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in superadmins)
            {
                if (aux.email == dtAdmin.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            Administrador administrador = new Administrador();
            administrador.nombre = dtAdmin.Name;
            administrador.email = dtAdmin.Email;
            administrador.password = dtAdmin.Password;
            administrador.billetera = 0;
            administrador.Tipo_Rol = Tipo_Rol.Admin;

            await _context.Administradores.AddAsync(administrador);
            await _context.SaveChangesAsync();

            return Ok(dtAdmin);
        }
        [HttpPut("agregarPenca/{idAdmin}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> agregarPenca(int idADmin, int idPenca)
        {

            var penca = await _context.Pencas.FindAsync(idPenca);
            if (penca == null) return BadRequest("No existe la penca");

            var admin = await _context.Administradores.FindAsync(idADmin);
            if (admin == null) return BadRequest("No existe el administrador");

            if (admin.pencas == null)
            {
                admin.pencas = new List<Penca>();
            }
            admin.pencas.Add(penca);

            _context.Entry(admin).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();

        }
        [HttpGet("PencasCompartidasGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> listarPencasCompartidas(int id)
        {

            var admin = _context.Administradores.Include(a => a.pencas);
            List<DtPencasCompartida> pencasComp = new List<DtPencasCompartida>();
            foreach (var a in admin)
            {
                if (a.Id == id)
                {

                    foreach (var aux in a.pencas)
                    {
                        DtPencasCompartida pencasaCompartidas = new DtPencasCompartida();
                        pencasaCompartidas.id = aux.id;
                        pencasaCompartidas.nombre = aux.nombre;
                        pencasaCompartidas.tipoDeporte = aux.tipo_Deporte;
                        pencasaCompartidas.entrada = aux.entrada;
                        pencasaCompartidas.pozo = aux.pozo;
                        pencasComp.Add(pencasaCompartidas);
                    }
                    return Ok(pencasComp);
                }
                
            }
            return NotFound();
        }
    }
}


