using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using V1.DyModels.Dso.ResponseFilters;
using System;

namespace V1.Validators
{
    public class CardModelValidator : BaseValidator<CardModelResponseFilterDso, CardModelValidatorStates>, ITValidator
    {
        public CardModelValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(CardModelValidatorStates.IsActive, new LambdaCondition<CardModelResponseFilterDso>(nameof(CardModelValidatorStates.IsActive), context => IsActive(context), "CardModel is not active"));
        }

        private bool IsActive(CardModelResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  CardModelValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}