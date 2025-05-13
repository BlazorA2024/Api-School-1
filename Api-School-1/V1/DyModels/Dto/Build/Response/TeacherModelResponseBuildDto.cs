using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Responses
{
    public class TeacherModelResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// NameId property for DTO.
        /// </summary>
        public String? NameId { get; set; }
        public NameModelResponseBuildDto? Name { get; set; }
        /// <summary>
        /// RowId property for DTO.
        /// </summary>
        public String? RowId { get; set; }
        public RowModelResponseBuildDto? Row { get; set; }
        public ICollection<TeacherSchoolResponseBuildDto>? TeacherSchools { get; set; }
        public ICollection<TeacherModuleResponseBuildDto>? TeacherModules { get; set; }
        public ICollection<TeacherStudentResponseBuildDto>? TeacherStudents { get; set; }
    }
}