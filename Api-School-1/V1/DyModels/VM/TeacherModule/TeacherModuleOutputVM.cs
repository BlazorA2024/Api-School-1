using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherModule  property for VM Output.
    /// </summary>
    public class TeacherModuleOutputVM : ITVM
    {
        ///
        public String? TeacherId { get; set; }
        public TeacherModelOutputVM? Teacher { get; set; }
        ///
        public String? ModuleId { get; set; }
        public ModuleModelOutputVM? Module { get; set; }
    }
}