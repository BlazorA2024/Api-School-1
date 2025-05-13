using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherStudent  property for VM Output.
    /// </summary>
    public class TeacherStudentOutputVM : ITVM
    {
        ///
        public String? TeacherId { get; set; }
        public TeacherModelOutputVM? Teacher { get; set; }
        ///
        public String? StudentId { get; set; }
        public StudentModelOutputVM? Student { get; set; }
    }
}