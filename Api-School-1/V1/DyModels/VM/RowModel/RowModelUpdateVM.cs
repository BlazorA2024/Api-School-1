using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// RowModel  property for VM Update.
    /// </summary>
    public class RowModelUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public RowModelCreateVM? Body { get; set; }
    }
}