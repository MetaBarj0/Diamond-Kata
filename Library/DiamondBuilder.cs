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

            return input switch
            {
                _ when IsInputSingleLetter(input) => BuildLetterDiamond(input[0]),
                _ when uint.TryParse(input, out uint x) => BuildRawDiamond((int)x),
                _ => throw new NotSupportedException()
            };
        }

        private static string BuildLetterDiamond(char c)
        {
            var raw = BuildRawDiamond(c);

            return char.IsLower(c) ? raw : raw.ToUpper();
        }

        private static string BuildRawDiamond<T>(T input) where T : notnull
        {
            var bottomHalfDiamond = BuildRawBottomHalfDiamond(input);
            var topHalfDiamond = MirrorBottomHalfDiamond(bottomHalfDiamond);

            return string.Join(Environment.NewLine, topHalfDiamond.Concat(bottomHalfDiamond));
        }

        private static IEnumerable<string> BuildRawBottomHalfDiamond<T>(T input) where T : notnull
        {
            int rank = ComputeInputRank(input);
            var targetLength = 2 * rank + 1;

            for (int i = 0; i <= rank; ++i)
                yield return input switch
                {
                    char c => BuildPaddedLine(targetLength, BuildDiamondMiddleLineWith((char)(c - i))),
                    int n => BuildPaddedLine(targetLength, BuildDiamondMiddleLineWith(n - i)),
                    _ => throw new NotSupportedException()
                };
        }

        private static int ComputeInputRank<T>(T input) where T : notnull => input switch
        {
            char c => char.ToLower(c) - 'a',
            int n => n,
            _ => throw new NotSupportedException()
        };

        private static bool IsInputSingleLetter(string input) => input.Length == 1 && char.IsLetter(input[0]);

        private static IEnumerable<string> MirrorBottomHalfDiamond(IEnumerable<string> bottomHalfDiamond) => bottomHalfDiamond.Skip(1).Reverse();

        private static string BuildPaddedLine(int targetLength, string line)
        {
            var padding = (targetLength - line.Length) / 2;

            return $"{RepeatChar(' ', padding)}{line}";
        }

        private static int ComputeHoleLengthForLetterLine(char c)
        {
            var lRank = ComputeCharacterRank(c);
            var holeLength = lRank == 0 ? 0 : 2 * (lRank - 1) + 1;

            return holeLength;
        }

        private static int ComputeHoleLengthForNumberLine(int n)
        {
            if (n == 0)
                return 0;

            return 2 * (n - 1) + 1 - (2 * $"{n}".Length - 2);
        }

        private static string BuildDiamondMiddleLineWith(char c) => BuildDiamondMiddleLine(c, ComputeHoleLengthForLetterLine(c));

        private static string BuildDiamondMiddleLineWith(int n) => BuildDiamondMiddleLine(n, ComputeHoleLengthForNumberLine(n));

        private static string BuildDiamondMiddleLine<T>(T x, int holeLength)
        {
            if (holeLength == 0)
                return $"{x}";

            return $"{x}{RepeatChar(' ', (int)holeLength)}{x}";
        }

        private static int ComputeCharacterRank(char c) => char.ToLower(c) - 'a';

        private static void FailForInvalidInput(string input)
        {
            if (input == string.Empty)
                FailForEmptyInput();

            if (IsNotLetterNorPositiveInteger(input))
                FailForNonLetterNorPositiveIntegerInput();
        }

        private static void FailForNonLetterNorPositiveIntegerInput()
        {
            StringBuilder messageBuilder = new StringBuilder()
            .AppendLine("You can create a diamond only with a single letter or a positive integer!")
            .Append(USAGE);

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
            StringBuilder messageBuilder = new StringBuilder()
            .AppendLine("Making a diamond with an empty input does not make sense!")
            .Append(USAGE);

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
