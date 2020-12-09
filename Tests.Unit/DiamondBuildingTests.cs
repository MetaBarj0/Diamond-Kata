using System;
using System.Runtime.Serialization;
using FluentAssertions;
using Xunit;

namespace Tests.Unit
{
    public class DiamondBuildingTests
    {
        [Fact]
        public void ANullInputShouldAdviseUserWithUsageInformation()
        {
            Action action = () => DiamondBuilder.MakeDiamondWith(null);
            action.Should().ThrowExactly<ForbiddenNullInputException>();
        }
    }

    internal class DiamondBuilder
    {
        public static void MakeDiamondWith(object input)
        {
            throw new ForbiddenNullInputException();
        }
    }

    internal class ForbiddenNullInputException : Exception { }
}
