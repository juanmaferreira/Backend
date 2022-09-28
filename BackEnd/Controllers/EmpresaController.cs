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
    public class EmpresaController : ControllerBase
    {

        private readonly EntidadesDbContext _context;
        public EmpresaController(EntidadesDbContext context) => _context = context;

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id) { 

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null) return BadRequest("No existe la empresa");

            DtEmpresa dtEmpresa = new DtEmpresa();
            dtEmpresa.Name = empresa.nombre;
            dtEmpresa.Id = empresa.id;
            dtEmpresa.Billetera = empresa.billetera;
            dtEmpresa.tipo_rol = empresa.tipoRol;
            dtEmpresa.Password = empresa.pass;
            dtEmpresa.Email = empresa.email;

            return empresa == null ? NotFound() : Ok(dtEmpresa);
        }

        [HttpPost("altaEmpresa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> altaEmpresa(DtEmpresa dtEmpresa)
        {
            DbSet<Usuario> usuarios = _context.Usuario;
            DbSet<Empresa> empresas = _context.Empresas;
            DbSet<Administrador> administradores = _context.Administradores;
            DbSet<SuperAdmin> superadmins = _context.SuperAdmins;
            foreach (var aux in empresas) { 
                if(aux.email == dtEmpresa.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in usuarios)
            {
                if (aux.email == dtEmpresa.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in administradores)
            {
                if (aux.email == dtEmpresa.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in superadmins)
            {
                if (aux.email == dtEmpresa.Email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            Empresa empresa = new Empresa();
            empresa.email = dtEmpresa.Email;
            empresa.nombre = dtEmpresa.Name;
            empresa.billetera = 0;
            empresa.tipoRol = Tipo_Rol.Admin;
            empresa.pass = dtEmpresa.Password;

            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();

            return Ok(dtEmpresa);
        }
    }

    
}
