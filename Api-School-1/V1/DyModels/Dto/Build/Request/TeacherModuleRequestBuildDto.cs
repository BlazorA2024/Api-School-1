using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Requests
{
    public class TeacherModuleRequestBuildDto : ITBuildDto
    {
        /// <summary>
        /// TeacherId property for DTO.
        /// </summary>
        public String? TeacherId { get; set; }
        public TeacherModelRequestBuildDto? Teacher { get; set; }
        /// <summary>
        /// ModuleId property for DTO.
        /// </summary>
        public String? ModuleId { get; set; }
        public ModuleModelRequestBuildDto? Module { get; set; }
    }
}