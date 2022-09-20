using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Data.Migrations
{
    public partial class createdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    billetera = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Liga_Equipos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreLiga = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liga_Equipos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Liga_Individuales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liga_Individuales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    billetera = table.Column<float>(type: "real", nullable: false),
                    tipoRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreEquipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Liga_Equipoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Equipos_Liga_Equipos_Liga_Equipoid",
                        column: x => x.Liga_Equipoid,
                        principalTable: "Liga_Equipos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaPartido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    resultado = table.Column<int>(type: "int", nullable: false),
                    idPenca = table.Column<int>(type: "int", nullable: false),
                    Liga_Equipoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Partidos_Liga_Equipos_Liga_Equipoid",
                        column: x => x.Liga_Equipoid,
                        principalTable: "Liga_Equipos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Pencas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tipo_Penca = table.Column<int>(type: "int", nullable: false),
                    tipo_Deporte = table.Column<int>(type: "int", nullable: false),
                    premio_empresa = table.Column<float>(type: "real", nullable: true),
                    tipo_Plan = table.Column<int>(type: "int", nullable: true),
                    entrada = table.Column<float>(type: "real", nullable: true),
                    pozo = table.Column<float>(type: "real", nullable: true),
                    liga_Equipoid = table.Column<int>(type: "int", nullable: false),
                    tipo_Liga = table.Column<int>(type: "int", nullable: false),
                    Empresaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pencas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pencas_Empresas_Empresaid",
                        column: x => x.Empresaid,
                        principalTable: "Empresas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Pencas_Liga_Equipos_liga_Equipoid",
                        column: x => x.liga_Equipoid,
                        principalTable: "Liga_Equipos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<int>(type: "int", nullable: false),
                    fecha_competencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idPenca = table.Column<int>(type: "int", nullable: false),
                    Liga_IndividualId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competencias_Liga_Individuales_Liga_IndividualId",
                        column: x => x.Liga_IndividualId,
                        principalTable: "Liga_Individuales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuarioid = table.Column<int>(type: "int", nullable: false),
                    empresaid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Empresas_empresaid",
                        column: x => x.empresaid,
                        principalTable: "Empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Usuario_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Predicciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_Resultado = table.Column<int>(type: "int", nullable: false),
                    partidoid = table.Column<int>(type: "int", nullable: false),
                    usuarioid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predicciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Predicciones_Partidos_partidoid",
                        column: x => x.partidoid,
                        principalTable: "Partidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Predicciones_Usuario_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Puntuaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    puntos = table.Column<int>(type: "int", nullable: false),
                    pencaid = table.Column<int>(type: "int", nullable: false),
                    usuarioid = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntuaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_Puntuaciones_Pencas_pencaid",
                        column: x => x.pencaid,
                        principalTable: "Pencas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Puntuaciones_Usuario_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Apuestas",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    competenciaId = table.Column<int>(type: "int", nullable: false),
                    usuarioid = table.Column<int>(type: "int", nullable: false),
                    idGanador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apuestas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Apuestas_Competencias_competenciaId",
                        column: x => x.competenciaId,
                        principalTable: "Competencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apuestas_Usuario_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nombres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompetenciaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nombres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nombres_Competencias_CompetenciaId",
                        column: x => x.CompetenciaId,
                        principalTable: "Competencias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompetenciaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participantes_Competencias_CompetenciaId",
                        column: x => x.CompetenciaId,
                        principalTable: "Competencias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: true),
                    Pencaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensajes_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mensajes_Pencas_Pencaid",
                        column: x => x.Pencaid,
                        principalTable: "Pencas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apuestas_competenciaId",
                table: "Apuestas",
                column: "competenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Apuestas_usuarioid",
                table: "Apuestas",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_empresaid",
                table: "Chats",
                column: "empresaid");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_usuarioid",
                table: "Chats",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Competencias_Liga_IndividualId",
                table: "Competencias",
                column: "Liga_IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_Liga_Equipoid",
                table: "Equipos",
                column: "Liga_Equipoid");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_ChatId",
                table: "Mensajes",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_Pencaid",
                table: "Mensajes",
                column: "Pencaid");

            migrationBuilder.CreateIndex(
                name: "IX_Nombres_CompetenciaId",
                table: "Nombres",
                column: "CompetenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_CompetenciaId",
                table: "Participantes",
                column: "CompetenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_Liga_Equipoid",
                table: "Partidos",
                column: "Liga_Equipoid");

            migrationBuilder.CreateIndex(
                name: "IX_Pencas_Empresaid",
                table: "Pencas",
                column: "Empresaid");

            migrationBuilder.CreateIndex(
                name: "IX_Pencas_liga_Equipoid",
                table: "Pencas",
                column: "liga_Equipoid");

            migrationBuilder.CreateIndex(
                name: "IX_Predicciones_partidoid",
                table: "Predicciones",
                column: "partidoid");

            migrationBuilder.CreateIndex(
                name: "IX_Predicciones_usuarioid",
                table: "Predicciones",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Puntuaciones_pencaid",
                table: "Puntuaciones",
                column: "pencaid");

            migrationBuilder.CreateIndex(
                name: "IX_Puntuaciones_usuarioid",
                table: "Puntuaciones",
                column: "usuarioid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apuestas");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "Nombres");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Predicciones");

            migrationBuilder.DropTable(
                name: "Puntuaciones");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Competencias");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropTable(
                name: "Pencas");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Liga_Individuales");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Liga_Equipos");
        }
    }
}
