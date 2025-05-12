using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherModel  property for VM Update.
    /// </summary>
    public class TeacherModelUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public TeacherModelCreateVM? Body { get; set; }
    }
}