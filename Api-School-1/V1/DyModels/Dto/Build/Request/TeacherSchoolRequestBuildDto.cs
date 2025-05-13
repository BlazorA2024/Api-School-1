using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Requests
{
    public class TeacherSchoolRequestBuildDto : ITBuildDto
    {
        /// <summary>
        /// TeacherId property for DTO.
        /// </summary>
        public String? TeacherId { get; set; }
        public TeacherModelRequestBuildDto? Teacher { get; set; }
        /// <summary>
        /// SchoolId property for DTO.
        /// </summary>
        public String? SchoolId { get; set; }
        public SchoolModelRequestBuildDto? School { get; set; }
    }
}