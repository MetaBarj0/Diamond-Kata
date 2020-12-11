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
        [InlineData("I'm not one letter")]
        [InlineData("Aa\r")]
        [InlineData("\0")]
        [InlineData("\t")]
        [InlineData("\v")]
        [InlineData("A\n")]
        [InlineData("-0")]
        [InlineData("-1")]
        [InlineData("-10")]
        public void ANonLetterInputShouldAdviseUserWithUsageInformationAndAClearErrorMessage(string input)
        {
            Action action = () => DiamondBuilder.MakeDiamondWith(input);

            action.Should().ThrowExactly<ForbiddenNonLetterNorPositiveIntegerInputException>()
                           .WithMessage(expectedWildcardPattern: "*only with a single letter*positive integer*Usage:*",
                                        because: "Anything else that letter and digit is not supported");
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

        [Theory]
        [InlineData("0", "0")]
        [InlineData("1", @" 0
1 1
 0")]
        [InlineData("9", @"         0
        1 1
       2   2
      3     3
     4       4
    5         5
   6           6
  7             7
 8               8
9                 9
 8               8
  7             7
   6           6
    5         5
     4       4
      3     3
       2   2
        1 1
         0")]
        public void ACorrectDigitShouldDrawADigitDiamond(string digit, string expected)
        {
            Func<string> action = () => DiamondBuilder.MakeDiamondWith(digit);

            action.Should().NotThrow();
            action().Should().Be(expected);
        }

        [Theory]
        [InlineData("11", @"           0
          1 1
         2   2
        3     3
       4       4
      5         5
     6           6
    7             7
   8               8
  9                 9
 10                 10
11                   11
 10                 10
  9                 9
   8               8
    7             7
     6           6
      5         5
       4       4
        3     3
         2   2
          1 1
           0")]
        [InlineData("80", @"                                                                                0
                                                                               1 1
                                                                              2   2
                                                                             3     3
                                                                            4       4
                                                                           5         5
                                                                          6           6
                                                                         7             7
                                                                        8               8
                                                                       9                 9
                                                                      10                 10
                                                                     11                   11
                                                                    12                     12
                                                                   13                       13
                                                                  14                         14
                                                                 15                           15
                                                                16                             16
                                                               17                               17
                                                              18                                 18
                                                             19                                   19
                                                            20                                     20
                                                           21                                       21
                                                          22                                         22
                                                         23                                           23
                                                        24                                             24
                                                       25                                               25
                                                      26                                                 26
                                                     27                                                   27
                                                    28                                                     28
                                                   29                                                       29
                                                  30                                                         30
                                                 31                                                           31
                                                32                                                             32
                                               33                                                               33
                                              34                                                                 34
                                             35                                                                   35
                                            36                                                                     36
                                           37                                                                       37
                                          38                                                                         38
                                         39                                                                           39
                                        40                                                                             40
                                       41                                                                               41
                                      42                                                                                 42
                                     43                                                                                   43
                                    44                                                                                     44
                                   45                                                                                       45
                                  46                                                                                         46
                                 47                                                                                           47
                                48                                                                                             48
                               49                                                                                               49
                              50                                                                                                 50
                             51                                                                                                   51
                            52                                                                                                     52
                           53                                                                                                       53
                          54                                                                                                         54
                         55                                                                                                           55
                        56                                                                                                             56
                       57                                                                                                               57
                      58                                                                                                                 58
                     59                                                                                                                   59
                    60                                                                                                                     60
                   61                                                                                                                       61
                  62                                                                                                                         62
                 63                                                                                                                           63
                64                                                                                                                             64
               65                                                                                                                               65
              66                                                                                                                                 66
             67                                                                                                                                   67
            68                                                                                                                                     68
           69                                                                                                                                       69
          70                                                                                                                                         70
         71                                                                                                                                           71
        72                                                                                                                                             72
       73                                                                                                                                               73
      74                                                                                                                                                 74
     75                                                                                                                                                   75
    76                                                                                                                                                     76
   77                                                                                                                                                       77
  78                                                                                                                                                         78
 79                                                                                                                                                           79
80                                                                                                                                                             80
 79                                                                                                                                                           79
  78                                                                                                                                                         78
   77                                                                                                                                                       77
    76                                                                                                                                                     76
     75                                                                                                                                                   75
      74                                                                                                                                                 74
       73                                                                                                                                               73
        72                                                                                                                                             72
         71                                                                                                                                           71
          70                                                                                                                                         70
           69                                                                                                                                       69
            68                                                                                                                                     68
             67                                                                                                                                   67
              66                                                                                                                                 66
               65                                                                                                                               65
                64                                                                                                                             64
                 63                                                                                                                           63
                  62                                                                                                                         62
                   61                                                                                                                       61
                    60                                                                                                                     60
                     59                                                                                                                   59
                      58                                                                                                                 58
                       57                                                                                                               57
                        56                                                                                                             56
                         55                                                                                                           55
                          54                                                                                                         54
                           53                                                                                                       53
                            52                                                                                                     52
                             51                                                                                                   51
                              50                                                                                                 50
                               49                                                                                               49
                                48                                                                                             48
                                 47                                                                                           47
                                  46                                                                                         46
                                   45                                                                                       45
                                    44                                                                                     44
                                     43                                                                                   43
                                      42                                                                                 42
                                       41                                                                               41
                                        40                                                                             40
                                         39                                                                           39
                                          38                                                                         38
                                           37                                                                       37
                                            36                                                                     36
                                             35                                                                   35
                                              34                                                                 34
                                               33                                                               33
                                                32                                                             32
                                                 31                                                           31
                                                  30                                                         30
                                                   29                                                       29
                                                    28                                                     28
                                                     27                                                   27
                                                      26                                                 26
                                                       25                                               25
                                                        24                                             24
                                                         23                                           23
                                                          22                                         22
                                                           21                                       21
                                                            20                                     20
                                                             19                                   19
                                                              18                                 18
                                                               17                               17
                                                                16                             16
                                                                 15                           15
                                                                  14                         14
                                                                   13                       13
                                                                    12                     12
                                                                     11                   11
                                                                      10                 10
                                                                       9                 9
                                                                        8               8
                                                                         7             7
                                                                          6           6
                                                                           5         5
                                                                            4       4
                                                                             3     3
                                                                              2   2
                                                                               1 1
                                                                                0")]
        public void ValidPositiveIntegerDrawADiamond(string number, string expected)
        {
            Func<string> action = () => DiamondBuilder.MakeDiamondWith(number);

            action.Should().NotThrow();
            action().Should().Be(expected);
        }
    }
}
