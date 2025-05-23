using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.ResponseFilters
{
    public class ModuleModelResponseFilterBuildDto : ITBuildDto
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
        public SchoolModelResponseFilterBuildDto? School { get; set; }
        /// <summary>
        /// RowId property for DTO.
        /// </summary>
        public String? RowId { get; set; }
        public RowModelResponseFilterBuildDto? Row { get; set; }
        public ICollection<StudentModuleResponseFilterBuildDto>? StudentModules { get; set; }
        public ICollection<TeacherModuleResponseFilterBuildDto>? TeacherModules { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}