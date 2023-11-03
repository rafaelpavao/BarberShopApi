using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Barbearia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PersonCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PersonType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    District = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    City = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    State = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Complement = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    CustomerPersonId = table.Column<int>(type: "integer", nullable: true),
                    SupplierPersonId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Role_Person_CustomerPersonId",
                        column: x => x.CustomerPersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_Role_Person_SupplierPersonId",
                        column: x => x.SupplierPersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "Telephone",
                columns: table => new
                {
                    TelephoneId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephone", x => x.TelephoneId);
                    table.ForeignKey(
                        name: "FK_Telephone_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDay",
                columns: table => new
                {
                    WorkingDayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    WorkDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    FinishTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDay", x => x.WorkingDayId);
                    table.ForeignKey(
                        name: "FK_WorkingDay_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleEmployee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEmployee", x => new { x.EmployeeId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RoleEmployee_Person_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleEmployee_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkingDayId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedule_WorkingDay_WorkingDayId",
                        column: x => x.WorkingDayId,
                        principalTable: "WorkingDay",
                        principalColumn: "WorkingDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeOff",
                columns: table => new
                {
                    TimeOffId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkingDayId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    FinishTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOff", x => x.TimeOffId);
                    table.ForeignKey(
                        name: "FK_TimeOff_WorkingDay_WorkingDayId",
                        column: x => x.WorkingDayId,
                        principalTable: "WorkingDay",
                        principalColumn: "WorkingDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "PersonId", "BirthDate", "Cnpj", "Cpf", "Email", "Gender", "Name", "PersonType", "Status" },
                values: new object[,]
                {
                    { 1, new DateOnly(1999, 8, 7), "", "73473943096", "veio@hotmail.com", 1, "Linus Torvalds", 2, 0 },
                    { 2, new DateOnly(2000, 1, 1), "", "73473003096", "bill@gmail.com", 2, "Bill Gates", 2, 0 },
                    { 3, new DateOnly(1973, 2, 1), "73473003096986", "", "josefacraft@hotmail.com", 0, "Josefina", 3, 1 },
                    { 4, new DateOnly(1975, 4, 4), "73473003096986", "", "micro@so.ft", 0, "Microsoft", 3, 2 },
                    { 5, new DateOnly(2000, 8, 7), "", "73473943096", "joao@hotmail.com", 1, "João cabeça", 4, 1 },
                    { 6, new DateOnly(1990, 1, 1), "", "73473003096", "billdoidao@gmail.com", 1, "Bill Maluco", 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "CustomerPersonId", "Name", "SupplierPersonId" },
                values: new object[,]
                {
                    { 1, null, "Barbeiro", null },
                    { 2, null, "Barbeiro Mestre?Sla", null }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "Cep", "City", "Complement", "District", "Number", "PersonId", "State", "Street" },
                values: new object[,]
                {
                    { 1, "88888888", "Bc", "Perto de la", "Teste", 100, 1, "SC", "Rua logo ali" },
                    { 2, "88888888", "Itajaí", "Longe de la", "Perto", 300, 2, "SC", "Rua longe" },
                    { 3, "80888088", "Bc", "Perto", "Asilo", 100, 3, "SC", "Rua velha" },
                    { 4, "88123888", "Floripa", "Longe", "soft", 300, 4, "SC", "Rua micro" }
                });

            migrationBuilder.InsertData(
                table: "RoleEmployee",
                columns: new[] { "EmployeeId", "RoleId" },
                values: new object[,]
                {
                    { 5, 1 },
                    { 6, 2 }
                });

            migrationBuilder.InsertData(
                table: "Telephone",
                columns: new[] { "TelephoneId", "Number", "PersonId", "Type" },
                values: new object[,]
                {
                    { 1, "47988887777", 1, 0 },
                    { 2, "47988887777", 2, 1 },
                    { 3, "47944887777", 3, 0 },
                    { 4, "55988844777", 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "WorkingDay",
                columns: new[] { "WorkingDayId", "FinishTime", "PersonId", "StartTime", "WorkDate" },
                values: new object[,]
                {
                    { 1, new TimeOnly(18, 30, 0), 5, new TimeOnly(7, 23, 11), new DateOnly(2023, 10, 10) },
                    { 2, new TimeOnly(19, 30, 0), 5, new TimeOnly(8, 23, 11), new DateOnly(2023, 11, 11) }
                });

            migrationBuilder.InsertData(
                table: "Schedule",
                columns: new[] { "ScheduleId", "Status", "WorkingDayId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "TimeOff",
                columns: new[] { "TimeOffId", "FinishTime", "StartTime", "WorkingDayId" },
                values: new object[,]
                {
                    { 1, new TimeOnly(14, 0, 0), new TimeOnly(11, 30, 0), 1 },
                    { 2, new TimeOnly(15, 0, 0), new TimeOnly(12, 0, 0), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonId",
                table: "Address",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_CustomerPersonId",
                table: "Role",
                column: "CustomerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_SupplierPersonId",
                table: "Role",
                column: "SupplierPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleEmployee_RoleId",
                table: "RoleEmployee",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_WorkingDayId",
                table: "Schedule",
                column: "WorkingDayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Telephone_PersonId",
                table: "Telephone",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOff_WorkingDayId",
                table: "TimeOff",
                column: "WorkingDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDay_PersonId",
                table: "WorkingDay",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "RoleEmployee");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Telephone");

            migrationBuilder.DropTable(
                name: "TimeOff");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "WorkingDay");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
