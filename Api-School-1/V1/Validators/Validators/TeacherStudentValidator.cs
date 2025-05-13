using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class TeacherStudentValidator : BaseValidator<TeacherStudentResponseFilterDso, TeacherStudentValidatorStates>, ITValidator
    {
        public TeacherStudentValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(TeacherStudentValidatorStates.IsActive, new LambdaCondition<TeacherStudentResponseFilterDso>(nameof(TeacherStudentValidatorStates.IsActive), context => IsActive(context), "TeacherStudent is not active"));
        }

        private bool IsActive(TeacherStudentResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  TeacherStudentValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}