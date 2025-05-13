using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// StudentModule  property for VM Create.
    /// </summary>
    public class StudentModuleCreateVM : ITVM
    {
        ///
        public String? StudentId { get; set; }
      //  public StudentModelCreateVM? Student { get; set; }
        ///
        public String? ModuleId { get; set; }
       // public ModuleModelCreateVM? Module { get; set; }
    }
}