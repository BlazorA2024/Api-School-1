using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class TeacherModuleValidator : BaseValidator<TeacherModuleResponseFilterDso, TeacherModuleValidatorStates>, ITValidator
    {
        public TeacherModuleValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(TeacherModuleValidatorStates.IsActive, new LambdaCondition<TeacherModuleResponseFilterDso>(nameof(TeacherModuleValidatorStates.IsActive), context => IsActive(context), "TeacherModule is not active"));
        }

        private bool IsActive(TeacherModuleResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  TeacherModuleValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}