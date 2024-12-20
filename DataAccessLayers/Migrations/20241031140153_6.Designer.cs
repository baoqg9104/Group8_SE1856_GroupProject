﻿// <auto-generated />
using System;
using DataAccessLayers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayers.Migrations
{
    [DbContext(typeof(JobFinderContext))]
    [Migration("20241031140153_6")]
    partial class _6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObjects.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AdminId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BusinessObjects.InterviewSchedule", b =>
                {
                    b.Property<int>("InterviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterviewId"));

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InterviewDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InterviewId");

                    b.HasIndex("ApplicationId")
                        .IsUnique();

                    b.ToTable("InterviewSchedules");
                });

            modelBuilder.Entity("BusinessObjects.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<DateTime>("ApplicationDeadline")
                        .HasColumnType("datetime2");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requirements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("JobId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("BusinessObjects.JobApplication", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationId"));

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicationId");

                    b.HasIndex("JobId");

                    b.HasIndex("MemberId");

                    b.ToTable("JobApplications");
                });

            modelBuilder.Entity("BusinessObjects.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Education")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExperienceYears")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skill")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MemberId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Members");
                });

            modelBuilder.Entity("BusinessObjects.RecruitmentCompany", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Industry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CompanyId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RecruitmentCompanies");
                });

            modelBuilder.Entity("BusinessObjects.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BusinessObjects.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusinessObjects.Admin", b =>
                {
                    b.HasOne("BusinessObjects.User", "User")
                        .WithOne("Admin")
                        .HasForeignKey("BusinessObjects.Admin", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObjects.InterviewSchedule", b =>
                {
                    b.HasOne("BusinessObjects.JobApplication", "JobApplication")
                        .WithOne("InterviewSchedule")
                        .HasForeignKey("BusinessObjects.InterviewSchedule", "ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobApplication");
                });

            modelBuilder.Entity("BusinessObjects.Job", b =>
                {
                    b.HasOne("BusinessObjects.RecruitmentCompany", "Company")
                        .WithMany("Jobs")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("BusinessObjects.JobApplication", b =>
                {
                    b.HasOne("BusinessObjects.Job", "Job")
                        .WithMany("JobApplications")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Member", "Member")
                        .WithMany("JobApplications")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("BusinessObjects.Member", b =>
                {
                    b.HasOne("BusinessObjects.User", "User")
                        .WithOne("Member")
                        .HasForeignKey("BusinessObjects.Member", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObjects.RecruitmentCompany", b =>
                {
                    b.HasOne("BusinessObjects.User", "User")
                        .WithOne("RecruitmentCompany")
                        .HasForeignKey("BusinessObjects.RecruitmentCompany", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObjects.User", b =>
                {
                    b.HasOne("BusinessObjects.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BusinessObjects.Job", b =>
                {
                    b.Navigation("JobApplications");
                });

            modelBuilder.Entity("BusinessObjects.JobApplication", b =>
                {
                    b.Navigation("InterviewSchedule")
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessObjects.Member", b =>
                {
                    b.Navigation("JobApplications");
                });

            modelBuilder.Entity("BusinessObjects.RecruitmentCompany", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("BusinessObjects.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BusinessObjects.User", b =>
                {
                    b.Navigation("Admin")
                        .IsRequired();

                    b.Navigation("Member")
                        .IsRequired();

                    b.Navigation("RecruitmentCompany")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
