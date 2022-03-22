using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IF3250_2022_24_APPTS_Backend.Migrations
{
    public partial class _001_init_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "job_application",
                columns: table => new
                {
                    application_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    job_id = table.Column<int>(type: "integer", nullable: true),
                    applicant_id = table.Column<int>(type: "integer", nullable: true),
                    apply_date = table.Column<DateOnly>(type: "date", nullable: true),
                    requirement_link = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    applicant_name = table.Column<string>(type: "text", nullable: true),
                    applicant_email = table.Column<string>(type: "text", nullable: true),
                    applicant_telp = table.Column<string>(type: "text", nullable: true),
                    interview_date = table.Column<DateOnly>(type: "date", nullable: true),
                    interview_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    interview_link = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_application", x => x.application_id);
                });

            migrationBuilder.CreateTable(
                name: "job_opening",
                columns: table => new
                {
                    job_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<int>(type: "integer", nullable: true),
                    job_name = table.Column<string>(type: "text", nullable: true),
                    start_recruitment_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_recruitment_date = table.Column<DateOnly>(type: "date", nullable: true),
                    job_type = table.Column<string>(type: "text", nullable: true),
                    salary = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_opening", x => x.job_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    profile_picture = table.Column<string>(type: "text", nullable: true),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    headline = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job_application");

            migrationBuilder.DropTable(
                name: "job_opening");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
