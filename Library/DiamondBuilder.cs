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

            char c = input[0];

            var bottomHalfDiamond = BuildBottomHalfDiamondWith(c);
            var topHalfDiamond = MirrorBottomHalfDiamond(bottomHalfDiamond);

            var diamond = string.Join(Environment.NewLine, topHalfDiamond.Concat(bottomHalfDiamond));

            return char.IsLower(c) ? diamond : diamond.ToUpper();
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

        private static string BuildDiamondMiddleLineWith(char letter)
        {
            var lRank = ComputeCharacterRank(letter);
            var holeLength = lRank == 0 ? 0 : 2 * (lRank - 1) + 1;

            if (holeLength == 0)
                return $"{letter}";

            return $"{letter}{RepeatChar(' ', holeLength)}{letter}";
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

            if (IsNotLetterNorDigit(input))
                FailForNonLetterNorDigitInput();
        }

        private static void FailForNonLetterNorDigitInput()
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendLine("You can create a diamond only with a single letter or a single digit!");
            messageBuilder.Append(USAGE);

            throw new ForbiddenNonLetterNorDigitInputException(messageBuilder.ToString());
        }

        private static bool IsNotLetterNorDigit(string input)
        {
            if (input.Length > 1)
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
Usage: diamond (letter|digit)
where letter is a valid uppercase or lowercase letter and digit, a valid digit between 0 and 9.";
    }

    public class ForbiddenEmptyInputException : Exception
    {
        public ForbiddenEmptyInputException(string message) : base(message) { }
    }

    public class ForbiddenNonLetterNorDigitInputException : Exception
    {
        public ForbiddenNonLetterNorDigitInputException(string message) : base(message) { }
    }
}
