using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherModel  property for VM Create.
    /// </summary>
    public class TeacherModelCreateVM : ITVM
    {
        ///
       // public String? NameId { get; set; }
        public NameModelCreateVM? Name { get; set; }
        ///
        public String? RowId { get; set; }
        //public RowModelCreateVM? Row { get; set; }
        ////
        //public List<SchoolModelCreateVM>? Schools { get; set; }
        ////
        //public List<ModuleModelCreateVM>? Moduls { get; set; }
        ////
        //public List<StudentModelCreateVM>? Students { get; set; }
    }
}