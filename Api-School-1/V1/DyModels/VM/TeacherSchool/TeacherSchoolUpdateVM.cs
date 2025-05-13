using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherSchool  property for VM Update.
    /// </summary>
    public class TeacherSchoolUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public TeacherSchoolCreateVM? Body { get; set; }
    }
}