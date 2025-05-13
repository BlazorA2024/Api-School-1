using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// ModuleModel  property for VM Create.
    /// </summary>
    public class ModuleModelCreateVM : ITVM
    {
        ///
        public String? Name { get; set; }
        ///
        public String? SchoolId { get; set; }
     //   public SchoolModelCreateVM? School { get; set; }
        ///
        public String? RowId { get; set; }
       // public RowModelCreateVM? Row { get; set; }
        //
        //public List<StudentModuleCreateVM>? StudentModules { get; set; }
        ////
        //public List<TeacherModuleCreateVM>? TeacherModules { get; set; }
    }
}