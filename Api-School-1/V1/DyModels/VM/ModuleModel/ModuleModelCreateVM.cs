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
        ///
        public String? RowId { get; set; }
        //public RowModelCreateVM? Row { get; set; }
        ////
        //public List<StudentModelCreateVM>? Students { get; set; }
        ////
        //public List<TeacherModelCreateVM>? Teachers { get; set; }
    }
}