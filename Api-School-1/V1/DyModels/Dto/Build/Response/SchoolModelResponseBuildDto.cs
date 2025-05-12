using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Responses
{
    public class SchoolModelResponseBuildDto : ITBuildDto
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
        /// Title property for DTO.
        /// </summary>
        public String? Title { get; set; }
        public ICollection<RowModelResponseBuildDto>? Rows { get; set; }
        public ICollection<StudentModelResponseBuildDto>? Students { get; set; }
        public ICollection<TeacherModelResponseBuildDto>? Teachers { get; set; }
        public ICollection<ModuleModelResponseBuildDto>? Moduls { get; set; }
    }
}