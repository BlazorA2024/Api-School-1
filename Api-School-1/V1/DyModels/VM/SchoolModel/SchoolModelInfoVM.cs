using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// SchoolModel  property for VM Info.
    /// </summary>
    public class SchoolModelInfoVM : ITVM
    {
        ///
        public string? Id { get; set; }
    }
    public class SchoolModelOutputVMV : ITVM
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public List<string>? RowNames { get; set; }
        public List<string>? ModuleNames { get; set; }
        public List<string>? TeacherNames { get; set; }
    }
}