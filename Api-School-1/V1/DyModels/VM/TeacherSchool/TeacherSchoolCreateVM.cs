using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherSchool  property for VM Create.
    /// </summary>
    public class TeacherSchoolCreateVM : ITVM
    {
        ///
        public String? TeacherId { get; set; }
     //   public TeacherModelCreateVM? Teacher { get; set; }
        ///
        public String? SchoolId { get; set; }
      //  public SchoolModelCreateVM? School { get; set; }
    }
}