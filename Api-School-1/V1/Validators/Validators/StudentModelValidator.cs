using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class StudentModelValidator : BaseValidator<StudentModelResponseFilterDso, StudentModelValidatorStates>, ITValidator
    {
        public StudentModelValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(StudentModelValidatorStates.IsActive, new LambdaCondition<StudentModelResponseFilterDso>(nameof(StudentModelValidatorStates.IsActive), context => IsActive(context), "StudentModel is not active"));
        }

        private bool IsActive(StudentModelResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  StudentModelValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}