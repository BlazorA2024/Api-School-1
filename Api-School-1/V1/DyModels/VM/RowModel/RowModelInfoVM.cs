using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// RowModel  property for VM Info.
    /// </summary>
    public class RowModelInfoVM : ITVM
    {
        ///
        public string? Id { get; set; }
    }
    public class RowModelInfoVMV : ITVM
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? SchoolNames { get; set; }
        public List<string>? StudentsNames { get; set; }
        public List<string>? ModuleNames { get; set; }
        public List<string>? TeacherNames { get; set; }
    }
}