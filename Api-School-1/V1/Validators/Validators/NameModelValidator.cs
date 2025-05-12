using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class NameModelValidator : BaseValidator<NameModelResponseFilterDso, NameModelValidatorStates>, ITValidator
    {
        public NameModelValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(NameModelValidatorStates.IsActive, new LambdaCondition<NameModelResponseFilterDso>(nameof(NameModelValidatorStates.IsActive), context => IsActive(context), "NameModel is not active"));
        }

        private bool IsActive(NameModelResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  NameModelValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}