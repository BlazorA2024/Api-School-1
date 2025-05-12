using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.Requests
{
    public class StudentModelRequestBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// NameId property for DTO.
        /// </summary>
        public String? NameId { get; set; }
        public NameModelRequestBuildDto? Name { get; set; }
        /// <summary>
        /// RowId property for DTO.
        /// </summary>
        public String? RowId { get; set; }
        public RowModelRequestBuildDto? Row { get; set; }
        /// <summary>
        /// SchoolId property for DTO.
        /// </summary>
        public String? SchoolId { get; set; }
        public SchoolModelRequestBuildDto? School { get; set; }
        /// <summary>
        /// CardId property for DTO.
        /// </summary>
        public String? CardId { get; set; }
        public CardModelRequestBuildDto? Card { get; set; }
        /// <summary>
        /// SexType property for DTO.
        /// </summary>
        public Nullable<SexType> SexType { get; set; }
        /// <summary>
        /// Age property for DTO.
        /// </summary>
        public Int32 Age { get; set; }
        public ICollection<ModuleModelRequestBuildDto>? Moduls { get; set; }
        public ICollection<TeacherModelRequestBuildDto>? Teachers { get; set; }
    }
}