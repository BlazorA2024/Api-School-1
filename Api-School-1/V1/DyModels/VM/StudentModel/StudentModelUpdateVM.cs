using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// StudentModel  property for VM Update.
    /// </summary>
    public class StudentModelUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public StudentModelCreateVM? Body { get; set; }
    }
}