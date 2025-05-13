using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.ResponseFilters
{
    public class TeacherStudentResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// TeacherId property for DTO.
        /// </summary>
        public String? TeacherId { get; set; }
        public TeacherModelResponseFilterBuildDto? Teacher { get; set; }
        /// <summary>
        /// StudentId property for DTO.
        /// </summary>
        public String? StudentId { get; set; }
        public StudentModelResponseFilterBuildDto? Student { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}