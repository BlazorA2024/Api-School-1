using AutoGenerator;
using AutoGenerator.Helper.Translation;
using ApiSchool.Models;
using System;

namespace V1.DyModels.VMs
{
    /// <summary>
    /// CardModel  property for VM Update.
    /// </summary>
    public class CardModelUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public CardModelCreateVM? Body { get; set; }
    }
}