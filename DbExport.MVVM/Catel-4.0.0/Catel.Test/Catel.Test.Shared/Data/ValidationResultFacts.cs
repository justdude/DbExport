﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResultTest.cs" company="Catel development team">
//   Copyright (c) 2011 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Catel.Test.Data
{
    using System;

    using Catel.Data;

    using NUnit.Framework;

    public class FieldValidationResultFacts
    {
        [TestFixture]
        public class TheConstructor
        {
            [TestCase]
            public void ThrowsNullReferenceExceptionForNullProperty()
            {
                ExceptionTester.CallMethodAndExpectException<NullReferenceException>(() => new FieldValidationResult((PropertyData)null, ValidationResultType.Error, "message"));
            }

            [TestCase]
            public void ThrowsArgumentExceptionForNullPropertyName()
            {
                ExceptionTester.CallMethodAndExpectException<ArgumentException>(() => new FieldValidationResult((string)null, ValidationResultType.Error, "message"));
            }

            [TestCase]
            public void ThrowsArgumentExceptionForEmptyPropertyName()
            {
                ExceptionTester.CallMethodAndExpectException<ArgumentException>(() => new FieldValidationResult(string.Empty, ValidationResultType.Error, "message"));
            }

            [TestCase]
            public void ThrowsArgumentNullExceptionForNullMessage()
            {
                ExceptionTester.CallMethodAndExpectException<ArgumentNullException>(() => new FieldValidationResult("myProperty", ValidationResultType.Error, null));
            }

            [TestCase]
            public void SetsValuesCorrectlyUsingEmptyMessage()
            {
                var validationResult = new FieldValidationResult("myProperty", ValidationResultType.Error, string.Empty);

                Assert.AreEqual("myProperty", validationResult.PropertyName);
                Assert.AreEqual(ValidationResultType.Error, validationResult.ValidationResultType);
                Assert.AreEqual(string.Empty, validationResult.Message);
            }

            [TestCase]
            public void SetsValuesCorrectlyUsingNormalMessage()
            {
                var validationResult = new FieldValidationResult("myProperty", ValidationResultType.Error, "my message");

                Assert.AreEqual("myProperty", validationResult.PropertyName);
                Assert.AreEqual(ValidationResultType.Error, validationResult.ValidationResultType);
                Assert.AreEqual("my message", validationResult.Message);
            }

            [TestCase]
            public void SetsValuesCorrectlyUsingFormattedMessage()
            {
                var validationResult = new FieldValidationResult("myProperty", ValidationResultType.Error, "my message with {0}", "format");

                Assert.AreEqual("myProperty", validationResult.PropertyName);
                Assert.AreEqual(ValidationResultType.Error, validationResult.ValidationResultType);
                Assert.AreEqual("my message with format", validationResult.Message);
            }
        }

        [TestFixture]
        public class TheCreateWarningMethod
        {
            [TestCase]
            public void SetsValuesCorrectlyUsingNormalMessage()
            {
                var validationResult = FieldValidationResult.CreateWarning("myProperty", "my message");

                Assert.AreEqual("myProperty", validationResult.PropertyName);
                Assert.AreEqual(ValidationResultType.Warning, validationResult.ValidationResultType);
                Assert.AreEqual("my message", validationResult.Message);
            }

            [TestCase]
            public void SetsValuesCorrectlyUsingFormattedMessage()
            {
                var validationResult = FieldValidationResult.CreateWarning("myProperty", "my message with {0}", "format");

                Assert.AreEqual("myProperty", validationResult.PropertyName);
                Assert.AreEqual(ValidationResultType.Warning, validationResult.ValidationResultType);
                Assert.AreEqual("my message with format", validationResult.Message);
            }
        }

        [TestFixture]
        public class TheCreateErrorMethod
        {
            [TestCase]
            public void SetsValuesCorrectlyUsingNormalMessage()
            {
                var validationResult = FieldValidationResult.CreateError("myProperty", "my message");

                Assert.AreEqual("myProperty", validationResult.PropertyName);
                Assert.AreEqual(ValidationResultType.Error, validationResult.ValidationResultType);
                Assert.AreEqual("my message", validationResult.Message);
            }

            [TestCase]
            public void SetsValuesCorrectlyUsingFormattedMessage()
            {
                var validationResult = FieldValidationResult.CreateError("myProperty", "my message with {0}", "format");

                Assert.AreEqual("myProperty", validationResult.PropertyName);
                Assert.AreEqual(ValidationResultType.Error, validationResult.ValidationResultType);
                Assert.AreEqual("my message with format", validationResult.Message);
            }
        }
    }

    public class BusinessRuleValidationResultFacts
    {
        [TestFixture]
        public class TheConstructor
        {
            [TestCase]
            public void ThrowsArgumentNullExceptionForNullMessage()
            {
                ExceptionTester.CallMethodAndExpectException<ArgumentException>(() => new BusinessRuleValidationResult(ValidationResultType.Error, null));
            }

            [TestCase]
            public void SetsValuesCorrectlyUsingEmptyMessage()
            {
                var validationResult = new BusinessRuleValidationResult(ValidationResultType.Error, string.Empty);

                Assert.AreEqual(ValidationResultType.Error, validationResult.ValidationResultType);
                Assert.AreEqual(string.Empty, validationResult.Message);
            }

            [TestCase]
            public void SetsValuesCorrectlyUsingNormalMessage()
            {
                var validationResult = new BusinessRuleValidationResult(ValidationResultType.Error, "my message");

                Assert.AreEqual(ValidationResultType.Error, validationResult.ValidationResultType);
                Assert.AreEqual("my message", validationResult.Message);
            }

            [TestCase]
            public void SetsValuesCorrectlyUsingFormattedMessage()
            {
                var validationResult = new BusinessRuleValidationResult(ValidationResultType.Error, "my message with {0}", "format");

                Assert.AreEqual(ValidationResultType.Error, validationResult.ValidationResultType);
                Assert.AreEqual("my message with format", validationResult.Message);
            }
        }

        [TestFixture]
        public class TheCreateWarningMethod
        {
            [TestCase]
            public void SetsValuesCorrectlyUsingNormalMessage()
            {
                var validationResult = BusinessRuleValidationResult.CreateWarning("my message");

                Assert.AreEqual(ValidationResultType.Warning, validationResult.ValidationResultType);
                Assert.AreEqual("my message", validationResult.Message);
            }

            [TestCase]
            public void SetsValuesCorrectlyUsingFormattedMessage()
            {
                var validationResult = BusinessRuleValidationResult.CreateWarning("my message with {0}", "format");

                Assert.AreEqual(ValidationResultType.Warning, validationResult.ValidationResultType);
                Assert.AreEqual("my message with format", validationResult.Message);
            }
        }

        [TestFixture]
        public class TheCreateErrorMethod
        {
            [TestCase]
            public void SetsValuesCorrectlyUsingNormalMessage()
            {
                var validationResult = BusinessRuleValidationResult.CreateError("my message");

                Assert.AreEqual(ValidationResultType.Error, validationResult.ValidationResultType);
                Assert.AreEqual("my message", validationResult.Message);
            }

            [TestCase]
            public void SetsValuesCorrectlyUsingFormattedMessage()
            {
                var validationResult = BusinessRuleValidationResult.CreateError("my message with {0}", "format");

                Assert.AreEqual(ValidationResultType.Error, validationResult.ValidationResultType);
                Assert.AreEqual("my message with format", validationResult.Message);
            }
        }
    }
}