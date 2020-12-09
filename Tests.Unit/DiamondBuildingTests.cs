using System;
using System.Runtime.Serialization;
using FluentAssertions;
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
    }

    internal class DiamondBuilder
    {
        public static void MakeDiamondWith(object input)
        {
            throw new ForbiddenNullInputException(@"Making a diamond with a null input does not make sense!
Usage: diamond letter
where letter is a valid uppercase or lowercase letter.");
        }
    }

    internal class ForbiddenNullInputException : Exception
    {
        public ForbiddenNullInputException(string message) : base(message) { }
    }
}
