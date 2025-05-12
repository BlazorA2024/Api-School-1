using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class ModuleModelValidator : BaseValidator<ModuleModelResponseFilterDso, ModuleModelValidatorStates>, ITValidator
    {
        public ModuleModelValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(ModuleModelValidatorStates.IsActive, new LambdaCondition<ModuleModelResponseFilterDso>(nameof(ModuleModelValidatorStates.IsActive), context => IsActive(context), "ModuleModel is not active"));
        }

        private bool IsActive(ModuleModelResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  ModuleModelValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}