using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class TeacherSchoolValidator : BaseValidator<TeacherSchoolResponseFilterDso, TeacherSchoolValidatorStates>, ITValidator
    {
        public TeacherSchoolValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(TeacherSchoolValidatorStates.IsActive, new LambdaCondition<TeacherSchoolResponseFilterDso>(nameof(TeacherSchoolValidatorStates.IsActive), context => IsActive(context), "TeacherSchool is not active"));
        }

        private bool IsActive(TeacherSchoolResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  TeacherSchoolValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}