using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Responses
{
    public class StudentModuleResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// StudentId property for DTO.
        /// </summary>
        public String? StudentId { get; set; }
        public StudentModelResponseBuildDto? Student { get; set; }
        /// <summary>
        /// ModuleId property for DTO.
        /// </summary>
        public String? ModuleId { get; set; }
        public ModuleModelResponseBuildDto? Module { get; set; }
    }
}