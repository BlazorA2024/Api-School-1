using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// CardModel  property for VM Create.
    /// </summary>
    public class CardModelCreateVM : ITVM
    {
        ///
        public DateTime Date { get; set; }
        ///
        public String? StudentId { get; set; }
        public StudentModelCreateVM? Student { get; set; }
        ///
        public String? SchoolId { get; set; }
        ///
        public String? RowId { get; set; }
        ///
        public String? Academic { get; set; }
        ///
        public String? Stage { get; set; }
    }
}