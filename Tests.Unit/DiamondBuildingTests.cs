using System;
using FluentAssertions;
using Library;
using Xunit;

namespace Tests.Unit
{
    public class DiamondBuildingTests
    {
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
            action().Should().Be(a, because: $"'{a}' is the first letter of the alphabet and should output itself");
        }

        [Theory]
        [InlineData("b", @" a
b b
 a")]
        [InlineData("B", @" A
B B
 A")]
        public void TheLetterBShouldOutputADiamondContainingFromALetterToBLetter(string letter, string expected)
        {
            Func<string> action = () => DiamondBuilder.MakeDiamondWith(letter);

            action.Should().NotThrow(because: $"{letter} is a valid input");

            action().Should().Be(expected);
        }

        [Theory]
        [InlineData("c", @"  a
 b b
c   c
 b b
  a")]
        [InlineData("C", @"  A
 B B
C   C
 B B
  A")]
        [InlineData("x", @"                       a
                      b b
                     c   c
                    d     d
                   e       e
                  f         f
                 g           g
                h             h
               i               i
              j                 j
             k                   k
            l                     l
           m                       m
          n                         n
         o                           o
        p                             p
       q                               q
      r                                 r
     s                                   s
    t                                     t
   u                                       u
  v                                         v
 w                                           w
x                                             x
 w                                           w
  v                                         v
   u                                       u
    t                                     t
     s                                   s
      r                                 r
       q                               q
        p                             p
         o                           o
          n                         n
           m                       m
            l                     l
             k                   k
              j                 j
               i               i
                h             h
                 g           g
                  f         f
                   e       e
                    d     d
                     c   c
                      b b
                       a")]
        public void AnyLetterShouldOutputADiamondContainingFromALetterToSpecifiedLetter(string letter, string expected)
        {
            Func<string> action = () => DiamondBuilder.MakeDiamondWith(letter);

            action.Should().NotThrow(because: $"{letter} is a valid input");

            action().Should().Be(expected);
        }
    }
}
