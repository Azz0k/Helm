using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Helm.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipmentModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RenderTemplateKey = table.Column<string>(type: "text", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedById = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedById = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentTemplates_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentTemplates_Users_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipmentTemplates_Users_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EquipmentReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TemplateId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IssuedBy = table.Column<string>(type: "text", nullable: false),
                    Returned = table.Column<bool>(type: "boolean", nullable: false),
                    ReturnedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    AcceptedById = table.Column<int>(type: "integer", nullable: true),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedById = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentReceipts_EquipmentTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "EquipmentTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentReceipts_Users_AcceptedById",
                        column: x => x.AcceptedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipmentReceipts_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentReceipts_Users_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsIssued = table.Column<bool>(type: "boolean", nullable: false),
                    IssuedBy = table.Column<string>(type: "text", nullable: false),
                    IsLost = table.Column<bool>(type: "boolean", nullable: false),
                    IsBulk = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedById = table.Column<int>(type: "integer", nullable: true),
                    EquipmentReceiptId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_EquipmentReceipts_EquipmentReceiptId",
                        column: x => x.EquipmentReceiptId,
                        principalTable: "EquipmentReceipts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_Users_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CreatedById",
                table: "Equipment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentReceiptId",
                table: "Equipment",
                column: "EquipmentReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_LastModifiedById",
                table: "Equipment",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Name",
                table: "Equipment",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentReceipts_AcceptedById",
                table: "EquipmentReceipts",
                column: "AcceptedById");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentReceipts_CreatedById",
                table: "EquipmentReceipts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentReceipts_LastModifiedById",
                table: "EquipmentReceipts",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentReceipts_TemplateId",
                table: "EquipmentReceipts",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTemplates_CreatedById",
                table: "EquipmentTemplates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTemplates_DeletedById",
                table: "EquipmentTemplates",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTemplates_LastModifiedById",
                table: "EquipmentTemplates",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTemplates_Name",
                table: "EquipmentTemplates",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "EquipmentReceipts");

            migrationBuilder.DropTable(
                name: "EquipmentTemplates");
        }
    }
}
