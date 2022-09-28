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
    public class SuperAdminController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public SuperAdminController(EntidadesDbContext context) => _context = context;

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id) { 
        
            var superAdmin = await _context.SuperAdmins.FindAsync(id);
            if (superAdmin == null) return BadRequest("No existe el super administrador");

            DtSuperAdmin dtSuperAdmin = new DtSuperAdmin();
            dtSuperAdmin.Name = superAdmin.nombre;
            dtSuperAdmin.Id = superAdmin.Id;
            dtSuperAdmin.tipo_rol = superAdmin.Tipo_Rol;
            dtSuperAdmin.Email = superAdmin.email;
            dtSuperAdmin.Password = superAdmin.password;
            dtSuperAdmin.Economia = superAdmin.economia;

            return superAdmin == null ? NotFound() : Ok(dtSuperAdmin);
        }

        [HttpPost("altaSuperAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaSuperAdmin(DtSuperAdmin dtSuperAdmin)
        {

            DbSet<Usuario> usuarios = _context.Usuario;
            DbSet<Empresa> empresas = _context.Empresas;
            DbSet<Administrador> administradores = _context.Administradores;
            DbSet<SuperAdmin> superadmins = _context.SuperAdmins;
            foreach (var aux in empresas)
            {
                if (aux.email == dtSuperAdmin.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in usuarios)
            {
                if (aux.email == dtSuperAdmin.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in administradores)
            {
                if (aux.email == dtSuperAdmin.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in superadmins)
            {
                if (aux.email == dtSuperAdmin.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            SuperAdmin superAdmin = new SuperAdmin();
            superAdmin.nombre = dtSuperAdmin.Name;
            superAdmin.email = dtSuperAdmin.Email;
            superAdmin.password = dtSuperAdmin.Password;
            superAdmin.economia = 0;
            superAdmin.Tipo_Rol = Tipo_Rol.Admin;

            await _context.SuperAdmins.AddAsync(superAdmin);
            await _context.SaveChangesAsync();

            return Ok(dtSuperAdmin);
        }
    }
}
