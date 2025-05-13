using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// StudentModule  property for VM Update.
    /// </summary>
    public class StudentModuleUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public StudentModuleCreateVM? Body { get; set; }
    }
}