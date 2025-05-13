using AutoGenerator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSchool.Models
{
    public enum SexType
    {
        Male,
        Female
    }

    public class ApplicationUser : IdentityUser<string>, ITModel
    {


    }

    public class NameModel : ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Title { get; set; }

        [NotMapped]
        public string FullName => $"{Title} {Name}";
    }

    public class CardModel : ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Date { get; set; }

        public string? StudentId { get; set; }
        public StudentModel? Student { get; set; }

        public string? SchoolId { get; set; }
        public string? RowId { get; set; }

        public string? Academic { get; set; }
        public string? Stage { get; set; }
    }

    public class SchoolModel : ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? Name { get; set; }
        public string? Title { get; set; }

     //   public string? UserId { get; set; }
      //  public ApplicationUser? User { get; set; }

        public ICollection<RowModel> Rows { get; set; } = new List<RowModel>();
        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
        public ICollection<ModuleModel> Modules { get; set; } = new List<ModuleModel>();
        public ICollection<TeacherSchool> TeacherSchools { get; set; } = new List<TeacherSchool>();
    }

    public class RowModel : ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }

        public string? SchoolId { get; set; }
        public SchoolModel? School { get; set; }

        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
        public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
        public ICollection<ModuleModel> Modules { get; set; } = new List<ModuleModel>();
    }

    public class ModuleModel : ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }

        public string? SchoolId { get; set; }
        public SchoolModel? School { get; set; }

        public string? RowId { get; set; }
        public RowModel? Row { get; set; }

        public ICollection<StudentModule> StudentModules { get; set; } = new List<StudentModule>();
        public ICollection<TeacherModule> TeacherModules { get; set; } = new List<TeacherModule>();
    }

    public class TeacherModel : ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? NameId { get; set; }
        public NameModel? Name { get; set; }

        public string? RowId { get; set; }
        public RowModel? Row { get; set; }

        public ICollection<TeacherSchool> TeacherSchools { get; set; } = new List<TeacherSchool>();
        public ICollection<TeacherModule> TeacherModules { get; set; } = new List<TeacherModule>();
        public ICollection<TeacherStudent> TeacherStudents { get; set; } = new List<TeacherStudent>();
    }

    public class StudentModel : ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? NameId { get; set; }
        public NameModel? Name { get; set; }

        public string? RowId { get; set; }
        public RowModel? Row { get; set; }

        public string? SchoolId { get; set; }
        public SchoolModel? School { get; set; }

        public ICollection<CardModel> Cards { get; set; } = new List<CardModel>();

        public SexType? SexType { get; set; }

        public int Age { get; set; }

        public ICollection<StudentModule> StudentModules { get; set; } = new List<StudentModule>();
        public ICollection<TeacherStudent> TeacherStudents { get; set; } = new List<TeacherStudent>();
    }

    // 👇 جداول الربط Many-to-Many

    public class StudentModule : ITModel
    {
        public string? StudentId { get; set; }
        public StudentModel? Student { get; set; }

        public string? ModuleId { get; set; }
        public ModuleModel? Module { get; set; }
    }

    public class TeacherModule : ITModel
    {
        public string TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }

        public string ModuleId { get; set; }
        public ModuleModel Module { get; set; }
    }

    public class TeacherStudent : ITModel
    {
        public string TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }

        public string StudentId { get; set; }
        public StudentModel Student { get; set; }
    }

    public class TeacherSchool : ITModel
    {
        public string TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }

        public string SchoolId { get; set; }
        public SchoolModel School { get; set; }
    }

}
