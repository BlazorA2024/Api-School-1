using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// NameModel  property for VM Update.
    /// </summary>
    public class NameModelUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public NameModelCreateVM? Body { get; set; }
    }
}