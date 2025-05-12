using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class ApplicationUserValidator : BaseValidator<ApplicationUserResponseFilterDso, ApplicationUserValidatorStates>, ITValidator
    {
        public ApplicationUserValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(ApplicationUserValidatorStates.IsActive, new LambdaCondition<ApplicationUserResponseFilterDso>(nameof(ApplicationUserValidatorStates.IsActive), context => IsActive(context), "ApplicationUser is not active"));
        }

        private bool IsActive(ApplicationUserResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  ApplicationUserValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}