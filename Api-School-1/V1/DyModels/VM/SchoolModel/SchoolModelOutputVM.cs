using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// SchoolModel  property for VM Output.
    /// </summary>
    public class SchoolModelOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? Name { get; set; }
        ///
        public String? Title { get; set; }
        //
        public List<RowModelOutputVM>? Rows { get; set; }
        //
        public List<StudentModelOutputVM>? Students { get; set; }
        //
        public List<ModuleModelOutputVM>? Modules { get; set; }
        //
        public List<TeacherSchoolOutputVM>? TeacherSchools { get; set; }
    }
}