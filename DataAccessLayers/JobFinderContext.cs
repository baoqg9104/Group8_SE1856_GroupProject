using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class JobFinderContext : DbContext
    {
        public JobFinderContext()
        {

        }

        public JobFinderContext(DbContextOptions<JobFinderContext> options) : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<InterviewSchedule> InterviewSchedules { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobSkill> JobSkills { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<RecruitmentCompany> RecruitmentCompanies { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<WorkExperience> WorkExperiences { get; set; }
        public virtual DbSet<MemberSkill> MemberSkills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var strConn = config["ConnectionStrings:DefaultConnection"];

            if (string.IsNullOrEmpty(strConn))
            {
                throw new InvalidOperationException("The ConnectionString property has not been initialized.");
            }

            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasOne(m => m.User)
                .WithOne(u => u.Member)
                .HasForeignKey<Member>(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecruitmentCompany>()
                .HasMany(rc => rc.User)
                .WithOne(u => u.RecruitmentCompany)
                .HasForeignKey(u => u.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne(u => u.Admin)
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobApplication>()
                .HasOne(ja => ja.Member)
                .WithMany(m => m.JobApplications)
                .HasForeignKey(ja => ja.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobApplication>()
                .HasOne(ja => ja.Job)
                .WithMany(j => j.JobApplications)
                .HasForeignKey(ja => ja.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InterviewSchedule>()
                .HasOne(i => i.JobApplication)
                .WithOne(ja => ja.InterviewSchedule)
                .HasForeignKey<InterviewSchedule>(i => i.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Job>()
                .HasOne(j => j.Company)
                .WithMany(rc => rc.Jobs)
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Job>()
                .HasMany(j => j.JobSkills)
                .WithOne(jk => jk.Job)
                .HasForeignKey(j => j.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Skill>()
                .HasMany(s => s.JobSkills)
                .WithOne(jk => jk.Skill)
                .HasForeignKey(s => s.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Skill>()
                .HasMany(s => s.MemberSkills)
                .WithOne(ms => ms.Skill)
                .HasForeignKey (s => s.SkillId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Member)
                .WithOne(m => m.User)
                .HasForeignKey<Member>(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.RecruitmentCompany)
                .WithMany(rc => rc.User)
                .HasForeignKey(u => u.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<JobSkill>()
                .HasOne(jk => jk.Job)
                .WithMany(j => j.JobSkills)
                .HasForeignKey(jk => jk.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobSkill>()
                .HasOne(jk => jk.Skill)
                .WithMany(j => j.JobSkills)
                .HasForeignKey(jk => jk.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MemberSkill>()
                .HasOne(ms => ms.Skill)
                .WithMany(s => s.MemberSkills)
                .HasForeignKey(ms => ms.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MemberSkill>()
               .HasOne(ms => ms.Member)
               .WithMany(s => s.MemberSkills)
               .HasForeignKey(ms => ms.MemberId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Education>()
                .HasOne(e => e.Member)
                .WithMany(m => m.Educations)
                .HasForeignKey(e => e.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkExperience>()
                .HasOne(w => w.Member)
                .WithMany(m => m.WorkExperiences)
                .HasForeignKey(w => w.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            

        }
    }
}
