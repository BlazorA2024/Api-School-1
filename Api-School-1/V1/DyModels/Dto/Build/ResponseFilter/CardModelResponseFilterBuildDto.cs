using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using AutoGenerator.Config;
using System;

namespace V1.DyModels.Dto.Build.ResponseFilters
{
    public class CardModelResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// Date property for DTO.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// StudentId property for DTO.
        /// </summary>
        public String? StudentId { get; set; }
        public StudentModelResponseFilterBuildDto? Student { get; set; }
        /// <summary>
        /// SchoolId property for DTO.
        /// </summary>
        public String? SchoolId { get; set; }
        /// <summary>
        /// RowId property for DTO.
        /// </summary>
        public String? RowId { get; set; }
        /// <summary>
        /// Academic property for DTO.
        /// </summary>
        public String? Academic { get; set; }
        /// <summary>
        /// Stage property for DTO.
        /// </summary>
        public String? Stage { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}