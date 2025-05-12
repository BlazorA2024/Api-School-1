using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// SchoolModel  property for VM Update.
    /// </summary>
    public class SchoolModelUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public SchoolModelCreateVM? Body { get; set; }
    }
}