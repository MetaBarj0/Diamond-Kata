using System;
using System.Runtime.Serialization;
using FluentAssertions;
using Program;
using Xunit;

namespace Tests.Unit
{
    public class DiamondBuildingTests
    {
        [Fact]
        public void ANullInputShouldAdviseUserWithUsageInformationAndAClearErrorMessage()
        {
            Action action = () => DiamondBuilder.MakeDiamondWith(null);

            action.Should().ThrowExactly<ForbiddenNullInputException>()
                           .WithMessage(expectedWildcardPattern: "*null input*Usage:*",
                                        because: "A null input does not make sense");
        }

        [Fact]
        public void AnEmptyInputShouldAdviseUserWithUsageInformationAndAClearErrorMessage()
        {
            Action action = () => DiamondBuilder.MakeDiamondWith(string.Empty);

            action.Should().ThrowExactly<ForbiddenEmptyInputException>()
                           .WithMessage(expectedWildcardPattern: "*empty input*Usage:*",
                                        because: "An empty input does not make sense");
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("_")]
        [InlineData("1")]
        [InlineData("I'm not one letter")]
        [InlineData("Aa\r")]
        [InlineData("\0")]
        [InlineData("\t")]
        [InlineData("\v")]
        [InlineData("A\n")]
        public void ANonLetterInputShouldAdviseUserWithUsageInformationAndAClearErrorMessage(string input)
        {
            Action action = () => DiamondBuilder.MakeDiamondWith(input);

            action.Should().ThrowExactly<ForbiddenNonLetterInputException>()
                           .WithMessage(expectedWildcardPattern: "*only with a single letter*Usage:*",
                                        because: "Anything else that letter is not supported");
        }

        [Fact]
        public void TheUppercaseLetterAShouldOutputItself()
        {
            Func<string> action = () => DiamondBuilder.MakeDiamondWith("A");

            action.Should().NotThrow(because: "'A' is a valid input");
            action.Invoke().Should().Be("A", because: "'A' is the first letter of the alphabet and should output itself");
        }

        [Fact]
        public void TheLowercaseLetterAShouldOutputItself()
        {
            Func<string> action = () => DiamondBuilder.MakeDiamondWith("a");

            action.Should().NotThrow(because: "'a' is a valid input");
            action.Invoke().Should().Be("a", because: "'a' is the first letter of the alphabet and should output itself");
        }
    }
}
