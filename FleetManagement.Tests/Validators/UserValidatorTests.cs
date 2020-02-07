using FluentValidation.TestHelper;
using NUnit.Framework;
using TheFleet.Services.Domain;
using TheFleet.Services.Validators;

namespace TheFleet.Tests.Validators
{
    internal class UserValidatorTests
    {
        private UserValidator userValidator;

        [SetUp]
        public void Setup()
        {
            this.userValidator = new UserValidator();
        }

        [Test]
        public void UserValidatorShouldNotHaveAnyErrorWhenModelIsValid()
        {
            // Arrange
            var user = new User("valid_test", "valid_test@test.com", "valid_username", "some_valid_password", "some_valid_password");

            // Act
            var result = userValidator.TestValidate(user);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestCase(null, "Name can not be null")]
        [TestCase("", "Name can not be empty")]
        public void UserValidatorShouldThrowErrorWhenNameIsInvalid(string name, string expectedMessage)
        {
            // Arrange
            var user = new User(name, "test@test.com", "test_username", "some_password", "some_password");

            // Act
            var result = userValidator.TestValidate(user);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage(expectedMessage);
        }

        [TestCase("test", "Email is not valid")]
        [TestCase(null, "Email can not be null")]
        [TestCase("", "Email can not be empty")]
        public void UserValidatorShouldThrowErrorWhenEmailIsInvalid(string email, string expectedMessage)
        {
            // Arrange
            var user = new User("Test", email, "test_username", "some_password", "some_password");

            // Act
            var result = userValidator.TestValidate(user);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage(expectedMessage);
        }

        [TestCase(null, "Username can not be null")]
        [TestCase("", "Username can not be empty")]
        public void UserValidatorShouldThrowErrorWhenUsernameIsInvalid(string username, string expectedMessage)
        {
            // Arrange
            var user = new User("Test", "test@test.com", username, "some_password", "some_password");

            // Act
            var result = userValidator.TestValidate(user);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username).WithErrorMessage(expectedMessage);
        }

        [TestCase(null, "Password can not be null")]
        [TestCase("", "Password can not be empty")]
        [TestCase("test", "Minimum length for password is 8 characters")]
        public void UserValidatorShouldThrowErrorWhenPasswordIsInvalid(string password, string expectedMessage)
        {
            // Arrange
            var user = new User("test", "test@test.com", "test_username", password, "some_password");

            // Act
            var result = userValidator.TestValidate(user);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedMessage);
        }

        [TestCase("some_valid_password", "some_invalid_password", "Passwords do not match")]
        [TestCase("some_valid_password", null, "Confirm Password can not be null")]
        [TestCase("some_valid_password", "", "Confirm Password can not be empty")]
        public void UserValidatorShouldThrowErrorWhenConfirmPasswordIsInvalid(string password, string confirmPassword, string expectedMessage)
        {
            // Arrange
            var user = new User("test", "test@test.com", "test_username", password, confirmPassword);

            // Act
            var result = userValidator.TestValidate(user);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword).WithErrorMessage(expectedMessage);
        }
    }
}
