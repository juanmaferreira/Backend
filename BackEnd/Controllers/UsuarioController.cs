using BackEnd.Data;
using BackEnd.Models.Clases;
using BackEnd.Models.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Runtime.Intrinsics.X86;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly EntidadesDbContext _context;
        public UsuarioController(EntidadesDbContext context) => _context = context;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return BadRequest("No existe el usuario");

            DtUsuario dtUsuario = new DtUsuario();
            dtUsuario.tipo_rol = usuario.tipoRol;
            dtUsuario.email = usuario.email;
            dtUsuario.Nombre = usuario.nombre;
            dtUsuario.billetera = usuario.billetera;
            dtUsuario.Id = usuario.id;

            return usuario == null ? NotFound() : Ok(dtUsuario);
        }

        [HttpPost("login")]
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

        [HttpPost("Registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrarse([FromBody] DtRegistro dtRegistro)
        {
            DbSet<Usuario> usuarios = _context.Usuario;
            DbSet<Empresa> empresas = _context.Empresas;
            DbSet<Administrador> administradores = _context.Administradores;
            DbSet<SuperAdmin> superadmins = _context.SuperAdmins;
            foreach (var aux in empresas)
            {
                if (aux.email == dtRegistro.email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in usuarios)
            {
                if (aux.email == dtRegistro.email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in administradores)
            {
                if (aux.email == dtRegistro.email) return BadRequest("Ya existe alguien registrada con ese mail");
            }

            foreach (var aux in superadmins)
            {
                if (aux.email == dtRegistro.email) return BadRequest("Ya existe alguien registrada con ese mail");
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

        [HttpPut("recuperarContraseña")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> recuperarContraseña([FromBody] string email)
        {
            DbSet<Usuario> users = _context.Usuario;
            Usuario usuario = new Usuario();
            foreach (var aux in users)
            {
                if (aux.email == email)
                {
                    usuario = aux;
                }
            }
            usuario.password = usuario.generarContraseña();

 
            string texto = "Hola, aqui te dejamos tu nueva contraseña. Esperemos no te vuelvas a olvidarte de ella :P. Contraseña: " + usuario.password;
            MailMessage mensaje = new MailMessage("penqueapp@gmail.com",email, "[PenqueApp] Nueva contraseña",texto);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("penqueapp@gmail.com", "qwknavxpudbjjtfr");

            smtpClient.Send(mensaje);
            smtpClient.Dispose();
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
