using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherSchool  property for VM Output.
    /// </summary>
    public class TeacherSchoolOutputVM : ITVM
    {
        ///
        public String? TeacherId { get; set; }
        public TeacherModelOutputVM? Teacher { get; set; }
        ///
        public String? SchoolId { get; set; }
        public SchoolModelOutputVM? School { get; set; }
    }
}