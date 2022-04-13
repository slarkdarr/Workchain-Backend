﻿// <auto-generated />
using System;
using IF3250_2022_24_APPTS_Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IF3250_2022_24_APPTS_Backend.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220413112954_001_init_tables")]
    partial class _001_init_tables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IF3250_2022_24_APPTS_Backend.Entities.JobApplication", b =>
                {
                    b.Property<int>("application_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("application_id"));

                    b.Property<string>("applicant_email")
                        .HasColumnType("text");

                    b.Property<int?>("applicant_id")
                        .HasColumnType("integer");

                    b.Property<string>("applicant_name")
                        .HasColumnType("text");

                    b.Property<string>("applicant_telp")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("apply_date")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("interview_date")
                        .HasColumnType("date");

                    b.Property<string>("interview_link")
                        .HasColumnType("text");

                    b.Property<TimeOnly?>("interview_time")
                        .HasColumnType("time without time zone");

                    b.Property<int?>("job_id")
                        .HasColumnType("integer");

                    b.Property<string>("requirement_link")
                        .HasColumnType("text");

                    b.Property<string>("status")
                        .HasColumnType("text");

                    b.HasKey("application_id");

                    b.ToTable("job_application");
                });

            modelBuilder.Entity("IF3250_2022_24_APPTS_Backend.Entities.JobOpening", b =>
                {
                    b.Property<int>("job_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("job_id"));

                    b.Property<int?>("company_id")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("end_recruitment_date")
                        .HasColumnType("date");

                    b.Property<string>("job_name")
                        .HasColumnType("text");

                    b.Property<string>("job_type")
                        .HasColumnType("text");

                    b.Property<int?>("salary")
                        .HasColumnType("integer");

                    b.Property<DateOnly?>("start_recruitment_date")
                        .HasColumnType("date");

                    b.HasKey("job_id");

                    b.ToTable("job_opening");
                });

            modelBuilder.Entity("IF3250_2022_24_APPTS_Backend.Entities.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("user_id"));

                    b.Property<DateOnly?>("birthdate")
                        .HasColumnType("date");

                    b.Property<string>("city")
                        .HasColumnType("text");

                    b.Property<string>("country")
                        .HasColumnType("text");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("gender")
                        .HasColumnType("text");

                    b.Property<string>("headline")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("phone_number")
                        .HasColumnType("text");

                    b.Property<string>("profile_picture")
                        .HasColumnType("text");

                    b.Property<string>("status")
                        .HasColumnType("text");

                    b.Property<string>("type")
                        .HasColumnType("text");

                    b.HasKey("user_id");

                    b.ToTable("user");
                });
#pragma warning restore 612, 618
        }
    }
}