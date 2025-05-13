using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherModule  property for VM Create.
    /// </summary>
    public class TeacherModuleCreateVM : ITVM
    {
        ///
        public String? TeacherId { get; set; }
       // public TeacherModelCreateVM? Teacher { get; set; }
        ///
        public String? ModuleId { get; set; }
       // public ModuleModelCreateVM? Module { get; set; }
    }
}