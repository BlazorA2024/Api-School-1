using ApiSchool.Models;
using AutoGenerator.Data;
using AutoGenerator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiSchool.Data
{
    public class DataContext : AutoIdentityDataContext<ApplicationUser, IdentityRole, string>, ITAutoDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<NameModel> NameModels { get; set; }
        public DbSet<CardModel> Cards { get; set; }
        public DbSet<SchoolModel> Schools { get; set; }
        public DbSet<RowModel> Rows { get; set; }
        public DbSet<ModuleModel> Modules { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }

        public DbSet<StudentModule> StudentModules { get; set; }
        public DbSet<TeacherModule> TeacherModules { get; set; }
        public DbSet<TeacherStudent> TeacherStudents { get; set; }
        public DbSet<TeacherSchool> TeacherSchools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Student ↔ Module
            modelBuilder.Entity<StudentModule>()
                .HasKey(sm => new { sm.StudentId, sm.ModuleId });

            modelBuilder.Entity<StudentModule>()
                .HasOne(sm => sm.Student)
                .WithMany(s => s.StudentModules)
                .HasForeignKey(sm => sm.StudentId);

            modelBuilder.Entity<StudentModule>()
                .HasOne(sm => sm.Module)
                .WithMany(m => m.StudentModules)
                .HasForeignKey(sm => sm.ModuleId);

            // Teacher ↔ Module
            modelBuilder.Entity<TeacherModule>()
                .HasKey(tm => new { tm.TeacherId, tm.ModuleId });

            modelBuilder.Entity<TeacherModule>()
                .HasOne(tm => tm.Teacher)
                .WithMany(t => t.TeacherModules)
                .HasForeignKey(tm => tm.TeacherId);

            modelBuilder.Entity<TeacherModule>()
                .HasOne(tm => tm.Module)
                .WithMany(m => m.TeacherModules)
                .HasForeignKey(tm => tm.ModuleId);

            // Teacher ↔ Student
            modelBuilder.Entity<TeacherStudent>()
                .HasKey(ts => new { ts.TeacherId, ts.StudentId });

            modelBuilder.Entity<TeacherStudent>()
                .HasOne(ts => ts.Teacher)
                .WithMany(t => t.TeacherStudents)
                .HasForeignKey(ts => ts.TeacherId);

            modelBuilder.Entity<TeacherStudent>()
                .HasOne(ts => ts.Student)
                .WithMany(s => s.TeacherStudents)
                .HasForeignKey(ts => ts.StudentId);

            // Teacher ↔ School
            modelBuilder.Entity<TeacherSchool>()
                .HasKey(tsc => new { tsc.TeacherId, tsc.SchoolId });

            modelBuilder.Entity<TeacherSchool>()
                .HasOne(tsc => tsc.Teacher)
                .WithMany(t => t.TeacherSchools)
                .HasForeignKey(tsc => tsc.TeacherId);

            modelBuilder.Entity<TeacherSchool>()
                .HasOne(tsc => tsc.School)
                .WithMany(s => s.TeacherSchools)
                .HasForeignKey(tsc => tsc.SchoolId);

            // Student ↔ Card
            modelBuilder.Entity<CardModel>()
                .HasOne(c => c.Student)
                .WithMany(s => s.Cards)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.SetNull);

            // Module ↔ Row & School
            modelBuilder.Entity<ModuleModel>()
                .HasOne(m => m.School)
                .WithMany(s => s.Modules)
                .HasForeignKey(m => m.SchoolId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ModuleModel>()
                .HasOne(m => m.Row)
                .WithMany(r => r.Modules)
                .HasForeignKey(m => m.RowId)
                .OnDelete(DeleteBehavior.SetNull);

            // Row ↔ School
            modelBuilder.Entity<RowModel>()
                .HasOne(r => r.School)
                .WithMany(s => s.Rows)
                .HasForeignKey(r => r.SchoolId)
                .OnDelete(DeleteBehavior.SetNull);

            // Student ↔ Row & School
            modelBuilder.Entity<StudentModel>()
                .HasOne(s => s.School)
                .WithMany(sch => sch.Students)
                .HasForeignKey(s => s.SchoolId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StudentModel>()
                .HasOne(s => s.Row)
                .WithMany(r => r.Students)
                .HasForeignKey(s => s.RowId)
                .OnDelete(DeleteBehavior.SetNull);

            // Teacher ↔ Row
            modelBuilder.Entity<TeacherModel>()
                .HasOne(t => t.Row)
                .WithMany(r => r.Teachers)
                .HasForeignKey(t => t.RowId)
                .OnDelete(DeleteBehavior.SetNull);

           
        }
    }
}

