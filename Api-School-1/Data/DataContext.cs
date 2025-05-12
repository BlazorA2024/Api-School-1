
using ApiSchool.Models;
using AutoGenerator;
using AutoGenerator.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiSchool.Data
{
    public class DataContext : AutoIdentityDataContext<ApplicationUser, IdentityRole, string>, ITAutoDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<NameModel> Names { get; set; }
        public DbSet<CardModel> Cards { get; set; }
        public DbSet<SchoolModel> Schools { get; set; }
        public DbSet<RowModel> Rows { get; set; }
        public DbSet<ModuleModel> Moduls { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ----- School ↔ Rows (One-to-Many)
            modelBuilder.Entity<SchoolModel>()
                .HasMany(s => s.Rows)
                .WithOne(r => r.School)
                .HasForeignKey(r => r.SchoolId)
                .OnDelete(DeleteBehavior.SetNull);

            // ----- School ↔ Students (One-to-Many)
            modelBuilder.Entity<SchoolModel>()
                .HasMany(s => s.Students)
                .WithOne(st => st.School)
                .HasForeignKey(st => st.SchoolId)
                .OnDelete(DeleteBehavior.SetNull);

            // ❌ [تم الحذف] School ↔ Teachers (One-to-Many) — تم استبدالها بـ Many-to-Many

            // ----- School ↔ Modules (One-to-Many)
            modelBuilder.Entity<SchoolModel>()
                .HasMany(s => s.Moduls)
                .WithOne()
                .HasForeignKey(m => m.SchoolId)
                .OnDelete(DeleteBehavior.SetNull);

            // ----- Row ↔ Students (One-to-Many)
            modelBuilder.Entity<RowModel>()
                .HasMany(r => r.Students)
                .WithOne(s => s.Row)
                .HasForeignKey(s => s.RowId)
                .OnDelete(DeleteBehavior.SetNull);

            // ----- Row ↔ Teachers (One-to-Many)
            modelBuilder.Entity<RowModel>()
                .HasMany(r => r.Teachers)
                .WithOne(t => t.Row)
                .HasForeignKey(t => t.RowId)
                .OnDelete(DeleteBehavior.SetNull);

            // ----- Row ↔ Modules (One-to-Many)
            modelBuilder.Entity<RowModel>()
                .HasMany(r => r.Moduls)
                .WithOne(m => m.Row)
                .HasForeignKey(m => m.RowId)
                .OnDelete(DeleteBehavior.SetNull);

            // ----- Student ↔ Name (One-to-One)
            modelBuilder.Entity<StudentModel>()
                .HasOne(s => s.Name)
                .WithMany()
                .HasForeignKey(s => s.NameId)
                .OnDelete(DeleteBehavior.SetNull);

            // ----- Teacher ↔ Name (One-to-One)
            modelBuilder.Entity<TeacherModel>()
                .HasOne(t => t.Name)
                .WithMany()
                .HasForeignKey(t => t.NameId)
                .OnDelete(DeleteBehavior.SetNull);

            // ----- Student ↔ Card (One-to-One)
            modelBuilder.Entity<StudentModel>()
                .HasOne(s => s.Card)
                .WithOne()
                .HasForeignKey<StudentModel>(s => s.CardId)
                .OnDelete(DeleteBehavior.SetNull);

            // ----- Card ↔ School/Row/Student (Optional Links)
            modelBuilder.Entity<CardModel>()
                .HasOne<SchoolModel>()
                .WithMany()
                .HasForeignKey(c => c.SchoolId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CardModel>()
                .HasOne<RowModel>()
                .WithMany()
                .HasForeignKey(c => c.RowId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CardModel>()
                .HasOne<StudentModel>()
                .WithMany()
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.SetNull);

            // ----- Student ↔ Module (Many-to-Many)
            modelBuilder.Entity<StudentModel>()
                .HasMany(s => s.Moduls)
                .WithMany(m => m.Students);

            // ----- Student ↔ Teacher (Many-to-Many)
            modelBuilder.Entity<StudentModel>()
                .HasMany(s => s.Teachers)
                .WithMany(t => t.Students);

            // ----- Teacher ↔ Module (Many-to-Many)
            modelBuilder.Entity<TeacherModel>()
                .HasMany(t => t.Moduls)
                .WithMany(m => m.Teachers);

            // ✅ Teacher ↔ School (Many-to-Many)
            modelBuilder.Entity<TeacherModel>()
                .HasMany(t => t.Schools)
                .WithMany(s => s.Teachers);
        }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // --- Student ↔ Modul (Many-to-Many)
        //    modelBuilder.Entity<StudentModel>()
        //        .HasMany(s => s.Moduls)
        //        .WithMany(m => m.Students);

        //    // --- Student ↔ Teacher (Many-to-Many)
        //    modelBuilder.Entity<StudentModel>()
        //        .HasMany(s => s.Teachers)
        //        .WithMany(t => t.Students);

        //    // --- Teacher ↔ Modul (Many-to-Many)
        //    modelBuilder.Entity<TeacherModel>()
        //        .HasMany(t => t.Moduls)
        //        .WithMany(m => m.Teachers);

        //    // --- Teacher ↔ School (Many-to-Many)
        //    modelBuilder.Entity<TeacherModel>()
        //        .HasMany(t => t.Schools)
        //        .WithMany(s => s.Teachers);

        //    // --- Row ↔ Modul (One-to-Many)
        //    modelBuilder.Entity<RowModel>()
        //        .HasMany(r => r.Moduls)
        //        .WithOne(m => m.Row)
        //        .HasForeignKey(m => m.RowId)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    // --- Row ↔ Student (One-to-Many)
        //    modelBuilder.Entity<RowModel>()
        //        .HasMany(r => r.Students)
        //        .WithOne(s => s.Row)
        //        .HasForeignKey(s => s.RowId)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    // --- Row ↔ Teacher (One-to-Many)
        //    modelBuilder.Entity<RowModel>()
        //        .HasMany(r => r.Teachers)
        //        .WithOne(t => t.Row)
        //        .HasForeignKey(t => t.RowId)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    // --- School ↔ Row (One-to-Many)
        //    modelBuilder.Entity<SchoolModel>()
        //        .HasMany(s => s.Rows)
        //        .WithOne(r => r.School)
        //        .HasForeignKey(r => r.SchoolId)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    // --- School ↔ Student (One-to-Many)
        //    modelBuilder.Entity<SchoolModel>()
        //        .HasMany(s => s.Students)
        //        .WithOne(st => st.School)
        //        .HasForeignKey(st => st.SchoolId)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    // --- School ↔ Modul (One-to-Many)
        //    modelBuilder.Entity<SchoolModel>()
        //        .HasMany(s => s.Moduls)
        //        .WithOne()
        //        .HasForeignKey(m => m.RowId) // indirectly via Row.School
        //        .OnDelete(DeleteBehavior.SetNull);

        //    // --- Student ↔ Name (One-to-One)
        //    modelBuilder.Entity<StudentModel>()
        //        .HasOne(s => s.Name)
        //        .WithMany()
        //        .HasForeignKey(s => s.NameId)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    // --- Student ↔ Card (One-to-One)
        //    modelBuilder.Entity<StudentModel>()
        //        .HasOne(s => s.Card)
        //        .WithMany()
        //        .HasForeignKey(s => s.CardId)
        //        .OnDelete(DeleteBehavior.SetNull);
        //}
    }
}