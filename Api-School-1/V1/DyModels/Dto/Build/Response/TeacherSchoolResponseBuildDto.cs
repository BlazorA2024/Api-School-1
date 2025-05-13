using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Responses
{
    public class TeacherSchoolResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// TeacherId property for DTO.
        /// </summary>
        public String? TeacherId { get; set; }
        public TeacherModelResponseBuildDto? Teacher { get; set; }
        /// <summary>
        /// SchoolId property for DTO.
        /// </summary>
        public String? SchoolId { get; set; }
        public SchoolModelResponseBuildDto? School { get; set; }
    }
}