using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherModule  property for VM Update.
    /// </summary>
    public class TeacherModuleUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public TeacherModuleCreateVM? Body { get; set; }
    }
}