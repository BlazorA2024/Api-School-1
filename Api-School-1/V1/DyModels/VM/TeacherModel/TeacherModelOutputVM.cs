using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// TeacherModel  property for VM Output.
    /// </summary>
    public class TeacherModelOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? NameId { get; set; }
        public NameModelOutputVM? Name { get; set; }
        ///
        public String? RowId { get; set; }
        public RowModelOutputVM? Row { get; set; }
        //
        public List<TeacherSchoolOutputVM>? TeacherSchools { get; set; }
        //
        public List<TeacherModuleOutputVM>? TeacherModules { get; set; }
        //
        public List<TeacherStudentOutputVM>? TeacherStudents { get; set; }
    }
}