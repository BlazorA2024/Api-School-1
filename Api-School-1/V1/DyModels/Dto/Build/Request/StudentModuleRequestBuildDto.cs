using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Requests
{
    public class StudentModuleRequestBuildDto : ITBuildDto
    {
        /// <summary>
        /// StudentId property for DTO.
        /// </summary>
        public String? StudentId { get; set; }
        public StudentModelRequestBuildDto? Student { get; set; }
        /// <summary>
        /// ModuleId property for DTO.
        /// </summary>
        public String? ModuleId { get; set; }
        public ModuleModelRequestBuildDto? Module { get; set; }
    }
}