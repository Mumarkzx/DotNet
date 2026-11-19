using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amparo.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AMPARO_API_CIDADAO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "DATE", nullable: false),
                    TELEFONE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ENDERECO = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: false),
                    CONDICAO = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: true),
                    ATIVO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMPARO_API_CIDADAO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AMPARO_API_CUIDADOR",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    TELEFONE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    PARENTESCO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PRIORIDADE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CIDADAO_ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMPARO_API_CUIDADOR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AMP_API_CUID_CID",
                        column: x => x.CIDADAO_ID,
                        principalTable: "AMPARO_API_CIDADAO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AMP_API_CUID_CID",
                table: "AMPARO_API_CUIDADOR",
                column: "CIDADAO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AMPARO_API_CUIDADOR");

            migrationBuilder.DropTable(
                name: "AMPARO_API_CIDADAO");
        }
    }
}
