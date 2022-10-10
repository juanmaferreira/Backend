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
            //try
            //{
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
                cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId, activa) values (1, '2022-10-06', 'Competencia1', 0, 3, NULL, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Nombres(nombre, competenciaId) values ('Competencia1', 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId, activa) values (1, '2022-10-06', 'Competencia2', 0, 3, NULL, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Nombres(nombre, competenciaId) values ('Competencia2', 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId, activa) values (2, '2022-10-06', 'Competencia3', 0, 5, NULL,1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Nombres(nombre, competenciaId) values ('Competencia3', 3)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId,activa) values (2, '2022-10-06', 'Competencia4', 0, 8, NULL,1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Nombres(nombre, competenciaId) values ('Competencia4', 4)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId,activa) values (3, '2022-10-06', 'Competencia5', 0, 3, NULL, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Nombres(nombre, competenciaId) values ('Competencia5', 5)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Competencias(Area, fecha_competencia, nombre, ligaI, topeParticipantes, Liga_IndividualId,activa) values (3, '2022-10-06', 'Competencia6', 0, 4, NULL,1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Nombres(nombre, competenciaId) values ('Competencia6', 6)";
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
                cadena = "update Competencias SET ligaI = 1, Liga_IndividualId = 1 WHERE Id = 1;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Competencias SET ligaI = 1, Liga_IndividualId = 1 WHERE Id = 2;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Competencias SET ligaI = 1, Liga_IndividualId = 1 WHERE Id = 3;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Competencias SET ligaI = 1, Liga_IndividualId = 2 WHERE Id = 4;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Competencias SET ligaI = 1, Liga_IndividualId = 2 WHERE Id = 5;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Competencias SET ligaI = 1, Liga_IndividualId = 2 WHERE Id = 3;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Competencias SET ligaI = 1, Liga_IndividualId = 3 WHERE Id = 6;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();

                cadena = "insert into Equipos(nombreEquipo) values ('Equipo1')";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Equipos(nombreEquipo) values ('Equipo2')";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Equipos(nombreEquipo) values ('Equipo3')";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Equipos(nombreEquipo) values ('Equipo4')";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Equipos(nombreEquipo) values ('Equipo5')";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Equipos(nombreEquipo) values ('Equipo6')";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Equipos(nombreEquipo) values ('Equipo7')";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Equipos(nombreEquipo) values ('Equipo8')";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Partidos(fechaPartido, resultado, enUso, Liga_EquipoId) values ('2022-10-06', 3, 0, NULL)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Partidos(fechaPartido, resultado, enUso, Liga_EquipoId) values ('2022-10-06', 3, 0, NULL)"; 
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Partidos(fechaPartido, resultado, enUso, Liga_EquipoId) values ('2022-10-06', 3, 0, NULL)"; 
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Partidos(fechaPartido, resultado, enUso, Liga_EquipoId) values ('2022-10-06', 3, 0, NULL)"; 
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Partidos(fechaPartido, resultado, enUso, Liga_EquipoId) values ('2022-10-06', 3, 0, NULL)"; 
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Partidos(fechaPartido, resultado, enUso, Liga_EquipoId) values ('2022-10-06', 3, 0, NULL)"; 
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Partidos(fechaPartido, resultado, enUso, Liga_EquipoId) values ('2022-10-06', 3, 0, NULL)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (1, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (1, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (2, 3)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (2, 4)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (3, 5)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (3, 6)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (4, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (4, 5)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (5, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (5, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (6, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (6, 4)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (7, 5)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into EquipoPartido(partidosId, visitante_localId) values (7, 8)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Liga_Equipos(nombreliga, topePartidos, activa) values ('LigaE1', 3, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Liga_Equipos(nombreliga, topePartidos, activa) values ('LigaE2', 3, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Liga_Equipos(nombreliga, topePartidos, activa) values ('LigaE3', 5, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Liga_Equipos(nombreliga, topePartidos, activa) values ('LigaE4', 4, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Partidos SET enUso = 1, Liga_EquipoId = 1 WHERE Id = 1;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Partidos SET enUso = 1, Liga_EquipoId = 1 WHERE Id = 2;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Partidos SET enUso = 1, Liga_EquipoId = 1 WHERE Id = 3;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Partidos SET enUso = 1, Liga_EquipoId = 2 WHERE Id = 4;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Partidos SET enUso = 1, Liga_EquipoId = 2 WHERE Id = 5;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Partidos SET enUso = 1, Liga_EquipoId = 2 WHERE Id = 6;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "update Partidos SET enUso = 1, Liga_EquipoId = 3 WHERE Id = 7;";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();

                cadena = "insert into Usuario(email, nombre, password, billetera, tipoRol) values ('usuario1@gmail.com', 'Usuario1', 'pass1', 1000, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Usuario(email, nombre, password, billetera, tipoRol) values ('usuario2@gmail.com', 'Usuario2', 'pass2', 1000, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Usuario(email, nombre, password, billetera, tipoRol) values ('usuario3@gmail.com', 'Usuario3', 'pass3', 1000, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Usuario(email, nombre, password, billetera, tipoRol) values ('usuario4@gmail.com', 'Usuario4', 'pass4', 1000, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Usuario(email, nombre, password, billetera, tipoRol) values ('usuario5@gmail.com', 'Usuario5', 'pass5', 1000, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Usuario(email, nombre, password, billetera, tipoRol) values ('usuario6@gmail.com', 'Usuario6', 'pass6', 1000, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Usuario(email, nombre, password, billetera, tipoRol) values ('usuario7@gmail.com', 'Usuario7', 'pass7', 1000, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Usuario(email, nombre, password, billetera, tipoRol) values ('usuario8@gmail.com', 'Usuario8', 'pass8', 1000, 2)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Administradores(email, nombre, password, billetera, tipo_Rol) values ('admin1@gmail.com', 'Admin1', 'pass1', 1000, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Administradores(email, nombre, password, billetera, tipo_Rol) values ('admin2@gmail.com', 'Admin2', 'pass2', 1000, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Administradores(email, nombre, password, billetera, tipo_Rol) values ('admin3@gmail.com', 'Admin3', 'pass3', 1000, 1)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Empresas(email, nombre, pass, billetera, tipoRol) values ('empresa1@gmail.com', 'Empresa1', 'pass1', 1000, 3)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Empresas(email, nombre, pass, billetera, tipoRol) values ('empresa2@gmail.com', 'Empresa2', 'pass2', 1000, 3)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Empresas(email, nombre, pass, billetera, tipoRol) values ('empresa3@gmail.com', 'Empresa3', 'pass3', 1000, 3)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Empresas(email, nombre, pass, billetera, tipoRol) values ('empresa4@gmail.com', 'Empresa4', 'pass4', 1000, 3)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Empresas(email, nombre, pass, billetera, tipoRol) values ('empresa5@gmail.com', 'Empresa5', 'pass5', 1000, 3)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Empresas(email, nombre, pass, billetera, tipoRol) values ('empresa6@gmail.com', 'Empresa6', 'pass6', 1000, 3)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Empresas(email, nombre, pass, billetera, tipoRol) values ('empresa7@gmail.com', 'Empresa7', 'pass7', 1000, 3)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                cadena = "insert into Empresas(email, nombre, pass, billetera, tipoRol) values ('empresa8@gmail.com', 'Empresa8', 'pass8', 1000, 3)";
                comando = new SqlCommand(cadena, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
            //}
            //catch(Exception ex) { }
        }
    }
}
