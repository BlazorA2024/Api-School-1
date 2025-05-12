using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class SchoolModelValidator : BaseValidator<SchoolModelResponseFilterDso, SchoolModelValidatorStates>, ITValidator
    {
        public SchoolModelValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(SchoolModelValidatorStates.IsActive, new LambdaCondition<SchoolModelResponseFilterDso>(nameof(SchoolModelValidatorStates.IsActive), context => IsActive(context), "SchoolModel is not active"));
        }

        private bool IsActive(SchoolModelResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  SchoolModelValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}