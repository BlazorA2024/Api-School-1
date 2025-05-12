using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class TeacherModelValidator : BaseValidator<TeacherModelResponseFilterDso, TeacherModelValidatorStates>, ITValidator
    {
        public TeacherModelValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(TeacherModelValidatorStates.IsActive, new LambdaCondition<TeacherModelResponseFilterDso>(nameof(TeacherModelValidatorStates.IsActive), context => IsActive(context), "TeacherModel is not active"));
        }

        private bool IsActive(TeacherModelResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  TeacherModelValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}