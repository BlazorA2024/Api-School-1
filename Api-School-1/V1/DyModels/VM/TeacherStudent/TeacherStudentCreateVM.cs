using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherStudent  property for VM Create.
    /// </summary>
    public class TeacherStudentCreateVM : ITVM
    {
        ///
        public String? TeacherId { get; set; }
        public TeacherModelCreateVM? Teacher { get; set; }
        ///
        public String? StudentId { get; set; }
        public StudentModelCreateVM? Student { get; set; }
    }
}