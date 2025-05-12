using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Requests
{
    public class SchoolModelRequestBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Name property for DTO.
        /// </summary>
        public String? Name { get; set; }
        /// <summary>
        /// Title property for DTO.
        /// </summary>
        public String? Title { get; set; }
        public ICollection<RowModelRequestBuildDto>? Rows { get; set; }
        public ICollection<StudentModelRequestBuildDto>? Students { get; set; }
        public ICollection<TeacherModelRequestBuildDto>? Teachers { get; set; }
        public ICollection<ModuleModelRequestBuildDto>? Moduls { get; set; }
    }
}