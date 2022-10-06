using BackEnd.Data;
using BackEnd.Models.Clases;
using BackEnd.Models.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        [HttpPost("meterDatos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void meterDatos()
        {
            SqlConnection conexion = new SqlConnection("server=DESKTOP-AKJETIH\\SQLEXPRESS ; database=PenqueApp ; integrated security = true");
            conexion.Open();
            string cadena = "insert into participantes(nombre,Area) values ('Participante1', 1)";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into participantes(nombre,Area) values ('Participante2', 1)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into participantes(nombre,Area) values ('Participante3', 1)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into participantes(nombre, Area) values ('Participante4', 1)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into participantes(nombre, Area) values ('Participante5', 2)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into participantes(nombre, Area) values ('Participante6', 2)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into participantes(nombre, Area) values ('Participante7', 2)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into participantes(nombre, Area) values ('Participante8', 3)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into participantes(nombre, Area) values ('Participante9', 3)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId) values (1, '2022-10-06', 'Competencia1', 0, 3, NULL)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId) values (1, '2022-10-06', 'Competencia2', 0, 3, NULL)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId) values (2, '2022-10-06', 'Competencia3', 0, 5, NULL)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId) values (2, '2022-10-06', 'Competencia4', 0, 8, NULL)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId) values (3, '2022-10-06', 'Competencia5', 0, 3, NULL)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId) values (3, '2022-10-06', 'Competencia6', 0, 4, NULL)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into CompetenciaParticipante(competenciasId, participantesId) values (1, 1)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into CompetenciaParticipante(competenciasId, participantesId) values (1, 2)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into CompetenciaParticipante(competenciasId, participantesId) values (1, 3)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into CompetenciaParticipante(competenciasId, participantesId) values (3, 4)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into CompetenciaParticipante(competenciasId, participantesId) values (3, 5)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into Liga_Individuales(Nombre, topeCompetencias, activa) values ('LigaI1', 3, 1)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into Liga_Individuales(Nombre, topeCompetencias, activa) values ('LigaI2', 4, 1)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into Liga_Individuales(Nombre, topeCompetencias, activa) values ('LigaI3', 3, 1)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            cadena = "insert into Liga_Individuales(Nombre, topeCompetencias, activa) values ('LigaI1', 3, 1)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();


            conexion.Close();
        }
    }
}
