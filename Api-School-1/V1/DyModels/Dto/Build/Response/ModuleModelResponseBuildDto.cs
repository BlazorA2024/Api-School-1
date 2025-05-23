using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Responses
{
    public class ModuleModelResponseBuildDto : ITBuildDto
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
        /// <summary>
        /// RowId property for DTO.
        /// </summary>
        public String? RowId { get; set; }
        public RowModelResponseBuildDto? Row { get; set; }
        public ICollection<StudentModuleResponseBuildDto>? StudentModules { get; set; }
        public ICollection<TeacherModuleResponseBuildDto>? TeacherModules { get; set; }
    }
}