using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class RowModelValidator : BaseValidator<RowModelResponseFilterDso, RowModelValidatorStates>, ITValidator
    {
        public RowModelValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(RowModelValidatorStates.IsActive, new LambdaCondition<RowModelResponseFilterDso>(nameof(RowModelValidatorStates.IsActive), context => IsActive(context), "RowModel is not active"));
        }

        private bool IsActive(RowModelResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  RowModelValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}