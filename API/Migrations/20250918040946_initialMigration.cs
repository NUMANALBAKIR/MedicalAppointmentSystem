using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionDetails_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionDetails_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dr. Smith" },
                    { 2, "Dr. Brown" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Paracetamol" },
                    { 2, "Metformin" },
                    { 3, "Amoxicillin" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "john@email.com", "John Doe" },
                    { 2, "jane@email.com", "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "Diagnosis", "DoctorId", "Notes", "PatientId", "VisitType" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Tension headache", 1, "Patient complains of headache", 1, "First Visit" },
                    { 2, new DateTime(2024, 1, 20, 14, 30, 0, 0, DateTimeKind.Unspecified), "Hypertension - stable", 2, "Blood pressure check", 2, "Follow-up" },
                    { 3, new DateTime(2024, 2, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Improved condition", 1, "Follow-up for headache treatment", 1, "Follow-up" }
                });

            migrationBuilder.InsertData(
                table: "PrescriptionDetails",
                columns: new[] { "Id", "AppointmentId", "Dosage", "EndDate", "MedicineId", "Notes", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, "500mg twice daily", new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Take with food", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "500mg once daily", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Take before breakfast", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, "250mg three times daily", new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Complete full course", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetails_AppointmentId",
                table: "PrescriptionDetails",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetails_MedicineId",
                table: "PrescriptionDetails",
                column: "MedicineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionDetails");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
