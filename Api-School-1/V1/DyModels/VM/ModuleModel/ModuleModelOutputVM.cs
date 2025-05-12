using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// ModuleModel  property for VM Output.
    /// </summary>
    public class ModuleModelOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? Name { get; set; }
        ///
        public String? SchoolId { get; set; }
        ///
        public String? RowId { get; set; }
        public RowModelOutputVM? Row { get; set; }
        //
        public List<StudentModelOutputVM>? Students { get; set; }
        //
        public List<TeacherModelOutputVM>? Teachers { get; set; }
    }
}