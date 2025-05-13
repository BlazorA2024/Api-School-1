using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.ResponseFilters
{
    public class SchoolModelResponseFilterBuildDto : ITBuildDto
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
        /// Title property for DTO.
        /// </summary>
        public String? Title { get; set; }
        public ICollection<RowModelResponseFilterBuildDto>? Rows { get; set; }
        public ICollection<StudentModelResponseFilterBuildDto>? Students { get; set; }
        public ICollection<ModuleModelResponseFilterBuildDto>? Modules { get; set; }
        public ICollection<TeacherSchoolResponseFilterBuildDto>? TeacherSchools { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}