using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// RowModel  property for VM Create.
    /// </summary>
    public class RowModelCreateVM : ITVM
    {
        ///
        public String? Name { get; set; }
        ///
        public String? SchoolId { get; set; }
        //public SchoolModelCreateVM? School { get; set; }
        ////
        //public List<StudentModelCreateVM>? Students { get; set; }
        ////
        //public List<TeacherModelCreateVM>? Teachers { get; set; }
        ////
        //public List<ModuleModelCreateVM>? Modules { get; set; }
    }
}