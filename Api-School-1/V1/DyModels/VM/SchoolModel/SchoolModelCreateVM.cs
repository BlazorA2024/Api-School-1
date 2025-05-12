using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// SchoolModel  property for VM Create.
    /// </summary>
    public class SchoolModelCreateVM : ITVM
    {
        ///
        public String? Name { get; set; }
        ///
        public String? Title { get; set; }
        //
        //public List<RowModelCreateVM>? Rows { get; set; }
        ////
        //public List<StudentModelCreateVM>? Students { get; set; }
        ////
        //public List<TeacherModelCreateVM>? Teachers { get; set; }
        ////
        //public List<ModuleModelCreateVM>? Moduls { get; set; }
    }
}