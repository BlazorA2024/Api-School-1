using AutoGenerator;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSchool.Models
{
    public enum SexType 
    {
        Male,
        Female
    }


    public class ApplicationUser : IdentityUser<string>,ITModel
    {


    }
        public class NameModel:ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Title { get; set; }

        [NotMapped]
        public string FullName => $"{Title} {Name}";
    }

    public class CardModel:ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime Date { get; set; }

        public string? SchoolId { get; set; }
        public string? StudentId { get; set; }
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
 
        public ICollection<RowModel> Rows { get; set; } = new List<RowModel>();
        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
        public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
        public ICollection<ModuleModel> Moduls { get; set; } = new List<ModuleModel>();
    }

    public class RowModel : ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public string? SchoolId { get; set; }
        public SchoolModel? School { get; set; }

        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
        public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
        public ICollection<ModuleModel> Moduls { get; set; } = new List<ModuleModel>();
    }

    public class ModuleModel : ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? SchoolId { get; set; }

        public string? RowId { get; set; }
        public RowModel? Row { get; set; }

        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
        public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
    }

    public class TeacherModel : ITModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? NameId { get; set; }
        public NameModel? Name { get; set; }
        public string? RowId { get; set; }
        public RowModel? Row { get; set; }

        public ICollection<SchoolModel> Schools { get; set; } = new List<SchoolModel>();
        public ICollection<ModuleModel> Moduls { get; set; } = new List<ModuleModel>();
        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
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

        public string? CardId { get; set; }
        public CardModel? Card { get; set; }

        public SexType? SexType { get; set; }

        public int Age { get; set; }

        public ICollection<ModuleModel> Moduls { get; set; } = new List<ModuleModel>();
        public ICollection<TeacherModel> Teachers { get; set; } = new List<TeacherModel>();
    }
}
