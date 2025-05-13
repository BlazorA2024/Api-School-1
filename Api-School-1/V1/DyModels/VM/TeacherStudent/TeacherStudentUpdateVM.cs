using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherStudent  property for VM Update.
    /// </summary>
    public class TeacherStudentUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public TeacherStudentCreateVM? Body { get; set; }
    }
}