using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// StudentModel  property for VM Info.
    /// </summary>
    public class StudentModelInfoVM : ITVM
    {
        ///
        public string? Id { get; set; }
    }
    public class StudentModelInfoVMV : ITVM
    {
        ///
        public string? Id { get; set; }
        public string FullName { get; set; }
        public string RowName { get; set; }
        public string SchoolName { get; set; }
        public List<string> ModulNames { get; set; }
        public List<string> TeacherNames { get; set; }
    }
}