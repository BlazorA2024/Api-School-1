using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.ResponseFilters
{
    public class TeacherModelResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// NameId property for DTO.
        /// </summary>
        public String? NameId { get; set; }
        public NameModelResponseFilterBuildDto? Name { get; set; }
        /// <summary>
        /// RowId property for DTO.
        /// </summary>
        public String? RowId { get; set; }
        public RowModelResponseFilterBuildDto? Row { get; set; }
        public ICollection<TeacherSchoolResponseFilterBuildDto>? TeacherSchools { get; set; }
        public ICollection<TeacherModuleResponseFilterBuildDto>? TeacherModules { get; set; }
        public ICollection<TeacherStudentResponseFilterBuildDto>? TeacherStudents { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}