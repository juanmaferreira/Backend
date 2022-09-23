using Microsoft.EntityFrameworkCore;
using BackEnd.Models.Clases;

namespace BackEnd.Data
{
    public class EntidadesDbContext : DbContext
    {
        
            public EntidadesDbContext(DbContextOptions<EntidadesDbContext> options)
                : base(options)
            {
            }

        public DbSet<Apuesta> Apuestas { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Competencia> Competencias { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Liga_Equipo> Liga_Equipos { get; set; }
        public DbSet<Liga_Individual> Liga_Individuales { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<Nombre> Nombres { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Penca> Pencas { get; set; }
        public DbSet<Prediccion> Predicciones { get; set; }
        public DbSet<Puntuacion> Puntuaciones { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Historial> Historials { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
    }
}
