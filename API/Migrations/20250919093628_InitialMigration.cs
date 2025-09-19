using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                    { 2, "Dr. Brown" },
                    { 3, "Dr. Taylor" },
                    { 4, "Dr. Wilson" },
                    { 5, "Dr. Carter" },
                    { 6, "Dr. Adams" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Paracetamol" },
                    { 2, "Metformin" },
                    { 3, "Amoxicillin" },
                    { 4, "Ibuprofen" },
                    { 5, "Atorvastatin" },
                    { 6, "Omeprazole" },
                    { 7, "Losartan" },
                    { 8, "Levothyroxine" },
                    { 9, "Azithromycin" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "john@email.com", "John Doe" },
                    { 2, "jane@email.com", "Jane Smith" },
                    { 3, "alice@email.com", "Alice Johnson" },
                    { 4, "bob@email.com", "Bob Lee" },
                    { 5, "clara@email.com", "Clara Oswald" },
                    { 6, "david@email.com", "Numan" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "Diagnosis", "DoctorId", "Notes", "PatientId", "VisitType" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Tension headache", 1, "Headache", 1, "First Visit" },
                    { 2, new DateTime(2024, 1, 20, 14, 30, 0, 0, DateTimeKind.Unspecified), "Hypertension", 2, "BP check", 2, "Follow-up" },
                    { 3, new DateTime(2024, 2, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Improved", 1, "Headache follow-up", 1, "Follow-up" },
                    { 4, new DateTime(2024, 2, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "Bronchitis", 3, "Cough", 3, "First Visit" },
                    { 5, new DateTime(2024, 2, 15, 13, 0, 0, 0, DateTimeKind.Unspecified), "Healthy", 4, "Annual checkup", 4, "Follow-up" },
                    { 6, new DateTime(2024, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Gastritis", 5, "Stomach pain", 5, "First Visit" },
                    { 7, new DateTime(2024, 3, 5, 15, 0, 0, 0, DateTimeKind.Unspecified), "Hypothyroidism", 6, "Thyroid check", 6, "Follow-up" },
                    { 8, new DateTime(2024, 3, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), "Improving", 2, "Cough persists", 3, "Follow-up" },
                    { 9, new DateTime(2024, 3, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "High cholesterol", 1, "Cholesterol review", 4, "First Visit" }
                });

            migrationBuilder.InsertData(
                table: "PrescriptionDetails",
                columns: new[] { "Id", "AppointmentId", "Dosage", "EndDate", "MedicineId", "Notes", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, "500mg twice daily", new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "With food", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "500mg once daily", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Before breakfast", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, "250mg three times daily", new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Full course", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, "200mg twice daily", new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "After meals", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, "10mg daily", new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Monitor cholesterol", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 6, "20mg daily", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Before meals", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 7, "100mcg daily", new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Morning dose", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 8, "500mg once daily", new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "With water", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 9, "10mg daily", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Monitor lipid profile", new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 4, "250mg twice daily", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Complete course", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 6, "20mg daily", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Before meals", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 7, "100mcg daily", new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Morning dose", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
