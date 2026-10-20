using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgenda.Infraestrutura.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class TBCompromissos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBCompromissos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Assunto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataOcorrencia = table.Column<DateTime>(type: "date", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraTermino = table.Column<TimeOnly>(type: "time", nullable: false),
                    TipoCompromissoEnum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCompromissos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBCompromissos_Assunto",
                table: "TBCompromissos",
                column: "Assunto",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBCompromissos");
        }
    }
}
