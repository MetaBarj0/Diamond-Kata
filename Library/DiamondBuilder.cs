using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class DiamondBuilder
    {
        public static string MakeDiamondWith(string input)
        {
            FailForInvalidInput(input);

            if (input.Length > 1)
            {
                uint.TryParse(input, out uint n);

                var bottomHalfDiamond = BuildBottomHalfDiamondWith(n);
                var topHalfDiamond = MirrorBottomHalfDiamond(bottomHalfDiamond);

                return string.Join(Environment.NewLine, topHalfDiamond.Concat(bottomHalfDiamond));
            }
            else
            {
                char c = input[0];

                var bottomHalfDiamond = BuildBottomHalfDiamondWith(c);
                var topHalfDiamond = MirrorBottomHalfDiamond(bottomHalfDiamond);

                var diamond = string.Join(Environment.NewLine, topHalfDiamond.Concat(bottomHalfDiamond));

                return char.IsLower(c) ? diamond : diamond.ToUpper();
            }
        }

        private static IEnumerable<string> MirrorBottomHalfDiamond(IEnumerable<string> bottomHalfDiamond) => bottomHalfDiamond.Skip(1).Reverse();

        private static IEnumerable<string> BuildBottomHalfDiamondWith(char c)
        {
            int lRank = ComputeCharacterRank(c);
            var lineLength = 2 * lRank + 1;

            for (; lRank >= 0; --lRank)
            {
                var line = BuildDiamondMiddleLineWith(c--);
                var padding = (lineLength - line.Length) / 2;

                yield return $"{RepeatChar(' ', padding)}{line}";
            }
        }

        private static IEnumerable<string> BuildBottomHalfDiamondWith(uint n)
        {
            var lineLength = 2 * n + 1;

            for (uint i = n + 1; i > 0; --i)
            {
                var line = BuildDiamondMiddleLineWith(n--);
                var padding = (lineLength - line.Length) / 2;

                yield return $"{RepeatChar(' ', (int)padding)}{line}";
            }
        }

        private static string BuildDiamondMiddleLineWith(char letter)
        {
            var lRank = ComputeCharacterRank(letter);
            var holeLength = lRank == 0 ? 0 : 2 * (lRank - 1) + 1;

            if (holeLength == 0)
                return $"{letter}";

            return $"{letter}{RepeatChar(' ', holeLength)}{letter}";
        }

        private static string BuildDiamondMiddleLineWith(uint n)
        {
            var holeLength = n == 0 ? 0 : 2 * (n - 1) + 1 - (2 * $"{n}".Length - 2);

            if (holeLength == 0)
                return $"{n}";

            return $"{n}{RepeatChar(' ', (int)holeLength)}{n}";
        }

        private static int ComputeCharacterRank(char c)
        {
            if (char.IsDigit(c))
                return c - '0';

            return char.ToLower(c) - 'a';
        }

        private static void FailForInvalidInput(string input)
        {
            if (input == string.Empty)
                FailForEmptyInput();

            if (IsNotLetterNorPositiveInteger(input))
                FailForNonLetterNorPositiveIntegerInput();
        }

        private static void FailForNonLetterNorPositiveIntegerInput()
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendLine("You can create a diamond only with a single letter or a positive integer!");
            messageBuilder.Append(USAGE);

            throw new ForbiddenNonLetterNorPositiveIntegerInputException(messageBuilder.ToString());
        }

        private static bool IsNotLetterNorPositiveInteger(string input)
        {
            if (input.Length > 1 && !uint.TryParse(input, out uint _))
                return true;

            return !Char.IsLetterOrDigit(input[0]);
        }

        private static string RepeatChar(char c, int n) => string.Join(null, Enumerable.Repeat(c, n));


        private static void FailForEmptyInput()
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendLine("Making a diamond with an empty input does not make sense!");
            messageBuilder.Append(USAGE);

            throw new ForbiddenEmptyInputException(messageBuilder.ToString());
        }

        private const string USAGE = @"
Usage: diamond (letter|positive integer)
where letter is a valid uppercase or lowercase letter and digit, a positive integer.";
    }

    public class ForbiddenEmptyInputException : Exception
    {
        public ForbiddenEmptyInputException(string message) : base(message) { }
    }

    public class ForbiddenNonLetterNorPositiveIntegerInputException : Exception
    {
        public ForbiddenNonLetterNorPositiveIntegerInputException(string message) : base(message) { }
    }
}
