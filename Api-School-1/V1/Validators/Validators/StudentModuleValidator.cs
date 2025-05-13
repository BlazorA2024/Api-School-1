using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class StudentModuleValidator : BaseValidator<StudentModuleResponseFilterDso, StudentModuleValidatorStates>, ITValidator
    {
        public StudentModuleValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(StudentModuleValidatorStates.IsActive, new LambdaCondition<StudentModuleResponseFilterDso>(nameof(StudentModuleValidatorStates.IsActive), context => IsActive(context), "StudentModule is not active"));
        }

        private bool IsActive(StudentModuleResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  StudentModuleValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}