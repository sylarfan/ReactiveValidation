﻿using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

using ReactiveValidation.Tests.TestModels;
using ReactiveValidation.Validators;
using ReactiveValidation.Tests.Helpers;

namespace ReactiveValidation.Tests.Validators
{
    public class EqualValidatorTests
    {
        [Theory]
        [InlineData(0, 10)]
        [InlineData("abacaba", "ABACABA")]
        public void EqualValidator_NotValidTheory<TProp>(TProp value, TProp valueToCompare)
        {
            var validationMessage = Equal(value, valueToCompare);

            AssertValidationMessage.NotEmptyMessage(validationMessage);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData("abacaba", "abacaba")]
        public void EqualValidatorProperty_ValidTheory<TProp>(TProp value, TProp valueToCompare)
        {
            var validationMessage = Equal(value, valueToCompare);

            AssertValidationMessage.EmptyMessage(validationMessage);
        }

        [Theory]
        [InlineData("abacaba", "ABACABA")]
        public void EqualValidatorWithComparer_ValidTheory(string value, string valueToCompare)
        {
            var validationMessage = Equal(value, valueToCompare, StringComparer.OrdinalIgnoreCase);

            AssertValidationMessage.EmptyMessage(validationMessage);
        }


        private ValidationMessage Equal<TProp>(
            TProp value,
            TProp valueToCompare,
            IEqualityComparer<TProp> comparer = null,
            ValidationMessageType validationMessageType = ValidationMessageType.Error)
        {
            var equalValidator = new EqualValidator<TestValidatableObject, TProp>(_ => valueToCompare, comparer, validationMessageType);
            var context = new ValidationContext<TestValidatableObject, TProp>(null, nameof(TestValidatableObject.Number), null, value);
            var validationMessage = equalValidator.ValidateProperty(context).FirstOrDefault();

            return validationMessage;
        }
    }
}
