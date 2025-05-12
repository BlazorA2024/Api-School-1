using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherModel  property for VM Output.
    /// </summary>
    public class TeacherModelOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? NameId { get; set; }
        public NameModelOutputVM? Name { get; set; }
        ///
        public String? RowId { get; set; }
        public RowModelOutputVM? Row { get; set; }
        //
        public List<SchoolModelOutputVM>? Schools { get; set; }
        //
        public List<ModuleModelOutputVM>? Moduls { get; set; }
        //
        public List<StudentModelOutputVM>? Students { get; set; }
    }
}