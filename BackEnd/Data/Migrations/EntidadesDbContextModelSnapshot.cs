// <auto-generated />
using System;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackEnd.Data.Migrations
{
    [DbContext(typeof(EntidadesDbContext))]
    partial class EntidadesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BackEnd.Models.Clases.Administrador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Tipo_Rol")
                        .HasColumnType("int");

                    b.Property<float?>("billetera")
                        .HasColumnType("real");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Apuesta", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("competenciaId")
                        .HasColumnType("int");

                    b.Property<int>("idGanador")
                        .HasColumnType("int");

                    b.Property<int>("idPuntuacionUsuario")
                        .HasColumnType("int");

                    b.Property<int>("usuarioid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("competenciaId");

                    b.HasIndex("usuarioid");

                    b.ToTable("Apuestas");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("empresaid")
                        .HasColumnType("int");

                    b.Property<int>("usuarioid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("empresaid");

                    b.HasIndex("usuarioid");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Competencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<int?>("Liga_IndividualId")
                        .HasColumnType("int");

                    b.Property<bool>("activa")
                        .HasColumnType("bit");

                    b.Property<DateTime>("fecha_competencia")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ligaI")
                        .HasColumnType("bit");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("topeParticipantes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Liga_IndividualId");

                    b.ToTable("Competencias");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Empresa", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<float?>("billetera")
                        .HasColumnType("real");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tipoRol")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Equipo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("nombreEquipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Equipos");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Historial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Equipoid")
                        .HasColumnType("int");

                    b.Property<int>("tipo_Historial")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Equipoid");

                    b.ToTable("Historials");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Liga_Equipo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<bool>("activa")
                        .HasColumnType("bit");

                    b.Property<string>("nombreLiga")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("topePartidos")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Liga_Equipos");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Liga_Individual", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("activa")
                        .HasColumnType("bit");

                    b.Property<int>("topeCompetencias")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Liga_Individuales");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Mensaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<int?>("Pencaid")
                        .HasColumnType("int");

                    b.Property<string>("mensaje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("Pencaid");

                    b.ToTable("Mensajes");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Nombre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CompetenciaId")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompetenciaId");

                    b.ToTable("Nombres");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Participante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Participantes");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Partido", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("Liga_Equipoid")
                        .HasColumnType("int");

                    b.Property<bool>("enUso")
                        .HasColumnType("bit");

                    b.Property<DateTime>("fechaPartido")
                        .HasColumnType("datetime2");

                    b.Property<int>("resultado")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Liga_Equipoid");

                    b.ToTable("Partidos");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Penca", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("AdministradorId")
                        .HasColumnType("int");

                    b.Property<int?>("Empresaid")
                        .HasColumnType("int");

                    b.Property<string>("color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("entrada")
                        .HasColumnType("real");

                    b.Property<bool>("estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("fecha_Creacion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("liga_Equipoid")
                        .HasColumnType("int");

                    b.Property<int?>("liga_IndividualId")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("pozo")
                        .HasColumnType("real");

                    b.Property<float?>("premio_empresa")
                        .HasColumnType("real");

                    b.Property<int>("tipo_Deporte")
                        .HasColumnType("int");

                    b.Property<int>("tipo_Liga")
                        .HasColumnType("int");

                    b.Property<int>("tipo_Penca")
                        .HasColumnType("int");

                    b.Property<int?>("tipo_Plan")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("AdministradorId");

                    b.HasIndex("Empresaid");

                    b.HasIndex("liga_Equipoid");

                    b.HasIndex("liga_IndividualId");

                    b.ToTable("Pencas");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Prediccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("idPuntuacionUsuario")
                        .HasColumnType("int");

                    b.Property<int>("partidoid")
                        .HasColumnType("int");

                    b.Property<int>("tipo_Resultado")
                        .HasColumnType("int");

                    b.Property<int>("usuarioid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("partidoid");

                    b.HasIndex("usuarioid");

                    b.ToTable("Predicciones");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Puntuacion", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.Property<int>("pencaid")
                        .HasColumnType("int");

                    b.Property<int>("puntos")
                        .HasColumnType("int");

                    b.Property<int>("usuarioid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("pencaid");

                    b.HasIndex("usuarioid");

                    b.ToTable("Puntuaciones");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.SuperAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Tipo_Rol")
                        .HasColumnType("int");

                    b.Property<float>("economia")
                        .HasColumnType("real");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SuperAdmins");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<float?>("billetera")
                        .HasColumnType("real");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tipoRol")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("CompetenciaParticipante", b =>
                {
                    b.Property<int>("competenciasId")
                        .HasColumnType("int");

                    b.Property<int>("participantesId")
                        .HasColumnType("int");

                    b.HasKey("competenciasId", "participantesId");

                    b.HasIndex("participantesId");

                    b.ToTable("CompetenciaParticipante");
                });

            modelBuilder.Entity("EquipoPartido", b =>
                {
                    b.Property<int>("partidosid")
                        .HasColumnType("int");

                    b.Property<int>("visitante_localid")
                        .HasColumnType("int");

                    b.HasKey("partidosid", "visitante_localid");

                    b.HasIndex("visitante_localid");

                    b.ToTable("EquipoPartido");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Apuesta", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Competencia", "competencia")
                        .WithMany("apuestas")
                        .HasForeignKey("competenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Clases.Usuario", "usuario")
                        .WithMany("apuestas")
                        .HasForeignKey("usuarioid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("competencia");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Chat", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Empresa", "empresa")
                        .WithMany("chats")
                        .HasForeignKey("empresaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Clases.Usuario", "usuario")
                        .WithMany("chats")
                        .HasForeignKey("usuarioid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("empresa");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Competencia", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Liga_Individual", null)
                        .WithMany("competencias")
                        .HasForeignKey("Liga_IndividualId");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Historial", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Equipo", null)
                        .WithMany("historiales")
                        .HasForeignKey("Equipoid");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Mensaje", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Chat", null)
                        .WithMany("mensajes")
                        .HasForeignKey("ChatId");

                    b.HasOne("BackEnd.Models.Clases.Penca", null)
                        .WithMany("foro")
                        .HasForeignKey("Pencaid");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Nombre", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Competencia", null)
                        .WithMany("posiciones")
                        .HasForeignKey("CompetenciaId");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Partido", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Liga_Equipo", null)
                        .WithMany("partidos")
                        .HasForeignKey("Liga_Equipoid");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Penca", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Administrador", null)
                        .WithMany("pencas")
                        .HasForeignKey("AdministradorId");

                    b.HasOne("BackEnd.Models.Clases.Empresa", null)
                        .WithMany("pencas_empresa")
                        .HasForeignKey("Empresaid");

                    b.HasOne("BackEnd.Models.Clases.Liga_Equipo", "liga_Equipo")
                        .WithMany("pencas")
                        .HasForeignKey("liga_Equipoid");

                    b.HasOne("BackEnd.Models.Clases.Liga_Individual", "liga_Individual")
                        .WithMany("pencas")
                        .HasForeignKey("liga_IndividualId");

                    b.Navigation("liga_Equipo");

                    b.Navigation("liga_Individual");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Prediccion", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Partido", "partido")
                        .WithMany("predicciones")
                        .HasForeignKey("partidoid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Clases.Usuario", "usuario")
                        .WithMany("predicciones")
                        .HasForeignKey("usuarioid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("partido");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Puntuacion", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Penca", "penca")
                        .WithMany("participantes_puntos")
                        .HasForeignKey("pencaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Clases.Usuario", "usuario")
                        .WithMany("puntos_por_penca")
                        .HasForeignKey("usuarioid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("penca");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("CompetenciaParticipante", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Competencia", null)
                        .WithMany()
                        .HasForeignKey("competenciasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Clases.Participante", null)
                        .WithMany()
                        .HasForeignKey("participantesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EquipoPartido", b =>
                {
                    b.HasOne("BackEnd.Models.Clases.Partido", null)
                        .WithMany()
                        .HasForeignKey("partidosid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Clases.Equipo", null)
                        .WithMany()
                        .HasForeignKey("visitante_localid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Administrador", b =>
                {
                    b.Navigation("pencas");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Chat", b =>
                {
                    b.Navigation("mensajes");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Competencia", b =>
                {
                    b.Navigation("apuestas");

                    b.Navigation("posiciones");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Empresa", b =>
                {
                    b.Navigation("chats");

                    b.Navigation("pencas_empresa");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Equipo", b =>
                {
                    b.Navigation("historiales");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Liga_Equipo", b =>
                {
                    b.Navigation("partidos");

                    b.Navigation("pencas");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Liga_Individual", b =>
                {
                    b.Navigation("competencias");

                    b.Navigation("pencas");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Partido", b =>
                {
                    b.Navigation("predicciones");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Penca", b =>
                {
                    b.Navigation("foro");

                    b.Navigation("participantes_puntos");
                });

            modelBuilder.Entity("BackEnd.Models.Clases.Usuario", b =>
                {
                    b.Navigation("apuestas");

                    b.Navigation("chats");

                    b.Navigation("predicciones");

                    b.Navigation("puntos_por_penca");
                });
#pragma warning restore 612, 618
        }
    }
}
