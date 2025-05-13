using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherModel  property for VM Info.
    /// </summary>
    public class TeacherModelInfoVM : ITVM
    {
        ///
        public string? Id { get; set; }
    }
    public class TeacherModelInfoVMV : ITVM
    {
        public string Id { get; set; }
        public string? FullName { get; set; }
        public string? RowName { get; set; }
        public List<string>? SchoolNames { get; set; }
        public List<string>? ModulNames { get; set; }
        public List<string>? StudentNames { get; set; }
    }

}