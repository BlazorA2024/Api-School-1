using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.ResponseFilters
{
    public class StudentModelResponseFilterBuildDto : ITBuildDto
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
        /// <summary>
        /// SchoolId property for DTO.
        /// </summary>
        public String? SchoolId { get; set; }
        public SchoolModelResponseFilterBuildDto? School { get; set; }
        /// <summary>
        /// CardId property for DTO.
        /// </summary>
        public String? CardId { get; set; }
        public CardModelResponseFilterBuildDto? Card { get; set; }
        /// <summary>
        /// SexType property for DTO.
        /// </summary>
        public Nullable<SexType> SexType { get; set; }
        /// <summary>
        /// Age property for DTO.
        /// </summary>
        public Int32 Age { get; set; }
        public ICollection<ModuleModelResponseFilterBuildDto>? Moduls { get; set; }
        public ICollection<TeacherModelResponseFilterBuildDto>? Teachers { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}