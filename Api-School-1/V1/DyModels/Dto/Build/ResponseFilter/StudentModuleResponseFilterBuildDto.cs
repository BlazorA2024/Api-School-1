using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.ResponseFilters
{
    public class StudentModuleResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// StudentId property for DTO.
        /// </summary>
        public String? StudentId { get; set; }
        public StudentModelResponseFilterBuildDto? Student { get; set; }
        /// <summary>
        /// ModuleId property for DTO.
        /// </summary>
        public String? ModuleId { get; set; }
        public ModuleModelResponseFilterBuildDto? Module { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}