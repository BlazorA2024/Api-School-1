using AutoGenerator.Conditions;
using V1.Validators.Conditions;
using System;

namespace V1.Validators
{
    public interface IConditionChecker : IBaseConditionChecker
    {
        public ITFactoryInjector Injector { get; }
    }
}