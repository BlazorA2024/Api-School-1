using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Responses
{
    public class TeacherModuleResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// TeacherId property for DTO.
        /// </summary>
        public String? TeacherId { get; set; }
        public TeacherModelResponseBuildDto? Teacher { get; set; }
        /// <summary>
        /// ModuleId property for DTO.
        /// </summary>
        public String? ModuleId { get; set; }
        public ModuleModelResponseBuildDto? Module { get; set; }
    }
}