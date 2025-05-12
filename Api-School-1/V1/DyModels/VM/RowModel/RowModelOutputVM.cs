using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// RowModel  property for VM Output.
    /// </summary>
    public class RowModelOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? Name { get; set; }
        ///
        public String? SchoolId { get; set; }
        public SchoolModelOutputVM? School { get; set; }
        //
        public List<StudentModelOutputVM>? Students { get; set; }
        //
        public List<TeacherModelOutputVM>? Teachers { get; set; }
        //
        public List<ModuleModelOutputVM>? Moduls { get; set; }
    }
}