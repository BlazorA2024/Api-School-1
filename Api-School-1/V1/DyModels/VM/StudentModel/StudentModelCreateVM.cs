using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// StudentModel  property for VM Create.
    /// </summary>
    public class StudentModelCreateVM : ITVM
    {
        ///
        public NameModelCreateVM? Name { get; set; }
        ///
        public String? RowId { get; set; }
      //  public RowModelCreateVM? Row { get; set; }
        ///
        public String? SchoolId { get; set; }
      //  public SchoolModelCreateVM? School { get; set; }
        ///
      //  public CardModelCreateVM? Card { get; set; }
        ///
        public Nullable<SexType> SexType { get; set; }
        ///
        public Int32 Age { get; set; }
        //
        //public List<ModuleModelCreateVM>? Moduls { get; set; }
        ////
        //public List<TeacherModelCreateVM>? Teachers { get; set; }
    }
}
