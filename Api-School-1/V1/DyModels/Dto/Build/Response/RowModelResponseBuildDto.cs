using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Responses
{
    public class RowModelResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// Name property for DTO.
        /// </summary>
        public String? Name { get; set; }
        /// <summary>
        /// SchoolId property for DTO.
        /// </summary>
        public String? SchoolId { get; set; }
        public SchoolModelResponseBuildDto? School { get; set; }
        public ICollection<StudentModelResponseBuildDto>? Students { get; set; }
        public ICollection<TeacherModelResponseBuildDto>? Teachers { get; set; }
        public ICollection<ModuleModelResponseBuildDto>? Moduls { get; set; }
    }
}