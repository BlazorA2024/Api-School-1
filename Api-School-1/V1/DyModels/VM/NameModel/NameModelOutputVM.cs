using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// NameModel  property for VM Output.
    /// </summary>
    public class NameModelOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? Name { get; set; }
        ///
        public String? Title { get; set; }
        ///
        public String? FullName { get; set; }
    }
}