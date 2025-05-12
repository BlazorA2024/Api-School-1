using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// StudentModel  property for VM Output.
    /// </summary>
    public class StudentModelOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? NameId { get; set; }
        public NameModelOutputVM? Name { get; set; }
        ///
        public String? RowId { get; set; }
        public RowModelOutputVM? Row { get; set; }
        ///
        public String? SchoolId { get; set; }
        public SchoolModelOutputVM? School { get; set; }
        ///
        public String? CardId { get; set; }
        public CardModelOutputVM? Card { get; set; }
        ///
        public Nullable<SexType> SexType { get; set; }
        ///
        public Int32 Age { get; set; }
        //
        public List<ModuleModelOutputVM>? Moduls { get; set; }
        //
        public List<TeacherModelOutputVM>? Teachers { get; set; }
    }
}