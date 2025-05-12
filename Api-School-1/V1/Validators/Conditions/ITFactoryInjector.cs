using AutoGenerator;
using AutoGenerator.Conditions;
using AutoMapper;
using ApiSchool.Data;
using System;

namespace V1.Validators.Conditions
{
    public interface ITFactoryInjector : ITBaseFactoryInjector
    {
        public DataContext Context { get; }
    }
}