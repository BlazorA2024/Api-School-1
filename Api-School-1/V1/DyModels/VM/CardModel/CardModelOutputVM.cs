using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// CardModel  property for VM Output.
    /// </summary>
    public class CardModelOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public DateTime Date { get; set; }
        ///
        public String? SchoolId { get; set; }
        ///
        public String? StudentId { get; set; }
        ///
        public String? RowId { get; set; }
        ///
        public String? Academic { get; set; }
        ///
        public String? Stage { get; set; }
    }
}