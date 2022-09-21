using BackEnd.Data;
using BackEnd.Models.Clases;
using BackEnd.Models.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public UsuarioController(EntidadesDbContext context) => _context = context;

        [HttpGet("id")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            DtUsuario dtUsuario = new DtUsuario();
            dtUsuario.tipo_rol = usuario.tipoRol;
            dtUsuario.email = usuario.email;
            dtUsuario.Nombre = usuario.nombre;
            dtUsuario.billetera = usuario.billetera;

            return usuario == null ? NotFound() : Ok(dtUsuario);
        }

        [HttpPost("/login")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> login(DtLogin dtLogin)
        {
            DbSet<Usuario> users = _context.Usuario;
            foreach (var aux in users)
            {
                if (aux.email == dtLogin.email)
                {
                    if (aux.password == dtLogin.password)
                    {
                        DtUsuario dtUsuario = new DtUsuario();
                        dtUsuario.Id = aux.id;
                        dtUsuario.tipo_rol = aux.tipoRol;
                        dtUsuario.email = aux.email;
                        dtUsuario.Nombre = aux.nombre;
                        dtUsuario.billetera = aux.billetera;

                        return aux == null ? NotFound() : Ok(dtUsuario);
                    }
                    else
                    {
                        return BadRequest();
                    }
                        
                }    
            }
            return BadRequest();
        }

        [HttpPost("/Registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrarse(DtRegistro dtRegistro)
        {
            DbSet<Usuario> users = _context.Usuario;
            foreach(var aux in users)
            {
                if (aux.email == dtRegistro.email) return BadRequest();
            }
            

            Usuario usuario = new Usuario();
            usuario.email = dtRegistro.email;
            usuario.nombre = dtRegistro.nombre;
            usuario.password = dtRegistro.password;
            usuario.billetera = 0;
            usuario.tipoRol = Tipo_Rol.Comun;

            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = usuario.id }, usuario);
        }
    }
}
