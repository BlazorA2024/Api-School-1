using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Requests
{
    public class TeacherModelRequestBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// NameId property for DTO.
        /// </summary>
        public String? NameId { get; set; }
        public NameModelRequestBuildDto? Name { get; set; }
        /// <summary>
        /// RowId property for DTO.
        /// </summary>
        public String? RowId { get; set; }
        public RowModelRequestBuildDto? Row { get; set; }
        public ICollection<SchoolModelRequestBuildDto>? Schools { get; set; }
        public ICollection<ModuleModelRequestBuildDto>? Moduls { get; set; }
        public ICollection<StudentModelRequestBuildDto>? Students { get; set; }
    }
}