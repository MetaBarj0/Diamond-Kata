using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
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
        [InlineData("\n")]
        [InlineData("\r")]
        [InlineData("\r\n")]
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

        [Theory]
        [InlineData("A")]
        [InlineData("a")]
        public void TheLetterAShouldOutputItself(string a)
        {
            Func<string> action = () => DiamondBuilder.MakeDiamondWith(a);

            action.Should().NotThrow(because: $"{a} is a valid input");
            action.Invoke().Should().Be(a, because: $"'{a}' is the first letter of the alphabet and should output itself");
        }

        [Theory]
        [InlineData("b", @" a 
b b
 a ")]
        [InlineData("B", @" A 
B B
 A ")]
        public void TheLetterBShouldOutputADiamondContainingFromALetterToBLetter(string letter, string expected)
        {
            Func<string> action = () => DiamondBuilder.MakeDiamondWith(letter);

            action.Should().NotThrow(because: $"{letter} is a valid input");

            action.Invoke().Should().Be(expected);
        }

        [Theory]
        [InlineData("c", @"  a  
 b b 
c   c
 b b 
  a  ")]
        [InlineData("C", @"  A  
 B B 
C   C
 B B 
  A  ")]
        public void TheLetterCShouldOutputADiamondContainingFromALetterToCLetter(string letter, string expected)
        {
            Func<string> action = () => DiamondBuilder.MakeDiamondWith(letter);

            action.Should().NotThrow(because: $"{letter} is a valid input");

            action.Invoke().Should().Be(expected);
        }

        // Tests below help us to find out an algorithm to build the diamond, it may be removed in future commits.
        // If such test were not employed, you would have to 'guess' the algorithm to create the diamond in one time.
        // Once you're more experienced with TDD that's not an issue but as a learning exercise this approach is simpler
        [Theory]
        [InlineData('a')]
        [InlineData('A')]
        public void TheLetterAShouldGenerateItself(char a)
        {
            Func<string> action = () => DiamondBuilder.BuildDiamondMiddleLine(a);

            action.Invoke().Should().Be($"{a}");
        }

        [Theory]
        [InlineData('b')]
        [InlineData('B')]
        public void TheLetterBShouldGenerateBLine(char c)
        {
            Func<string> action = () => DiamondBuilder.BuildDiamondMiddleLine(c);

            action.Invoke().Should().Be($"{c} {c}");
        }

        [Theory]
        [InlineData('c')]
        [InlineData('C')]
        public void TheLetterCShouldGenerateCLine(char c)
        {
            Func<string> action = () => DiamondBuilder.BuildDiamondMiddleLine(c);

            action.Invoke().Should().Be($"{c}   {c}");
        }

        [Theory]
        [InlineData('d')]
        [InlineData('D')]
        public void TheLetterDShouldGenerateDLine(char d)
        {
            Func<string> action = () => DiamondBuilder.BuildDiamondMiddleLine(d);

            action.Invoke().Should().Be($"{d}     {d}");
        }

        [Theory]
        [InlineData('x')]
        [InlineData('X')]
        public void TheLetterXShouldGenerateXLine(char x)
        {
            Func<string> action = () => DiamondBuilder.BuildDiamondMiddleLine(x);

            action.Invoke().Should().Be($"{x}                                             {x}");
        }
    }
}
