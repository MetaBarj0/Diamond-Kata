using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Program
{
    public class DiamondBuilder
    {
        public static string MakeDiamondWith(string input)
        {
            FailForInvalidInput(input);

            char letter = input[0];

            if (IsA(letter))
                return input;

            string diamond = string.Empty;

            if (IsB(letter))
                diamond = @" a 
b b
 a ";

            if (IsC(letter))
                diamond = @"  a  
 b b 
c   c
 b b 
  a  ";

            return char.IsLower(letter) ? diamond : diamond.ToUpper();
        }

        public static string BuildDiamondMiddleLineWith(char letter)
        {
            var l = char.ToLower(letter);
            var lRank = l - 'a';
            var holeLength = lRank == 0 ? 0 : 2 * (lRank - 1) + 1;

            if (holeLength == 0)
                return $"{letter}";

            return $"{letter}{string.Join(null, Enumerable.Repeat(' ', holeLength))}{letter}";
        }

        public static IEnumerable<string> BuildBottomHalfDiamondWith(char letter)
        {
            var l = char.ToLower(letter);
            var lRank = l - 'a';
            var lineLength = 2 * lRank + 1;

            for (; lRank >= 0; --lRank)
            {
                var line = BuildDiamondMiddleLineWith(letter--);
                var padding = (lineLength - line.Length) / 2;
                yield return $"{string.Join(null, Enumerable.Repeat(' ', padding))}{line}{string.Join(null, Enumerable.Repeat(' ', padding))}";
            }
        }

        private static bool IsA(char letter) => letter == 'a' || letter == 'A';
        private static bool IsB(char letter) => letter == 'b' || letter == 'B';
        private static bool IsC(char letter) => letter == 'c' || letter == 'C';

        private static void FailForInvalidInput(string input)
        {
            if (input == null)
                FailForNullInput();

            if (input == string.Empty)
                FailForEmptyInput();

            if (IsNotLetter(input))
                FailForNonLetterInput();
        }

        private static void FailForNonLetterInput()
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendLine("You can create a diamond only with a single letter!");
            messageBuilder.Append(USAGE);

            throw new ForbiddenNonLetterInputException(messageBuilder.ToString());
        }

        private static bool IsNotLetter(string input)
        {
            if (input.Length > 1)
                return true;

            return !Char.IsLetter(input[0]);
        }

        private static void FailForNullInput()
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendLine("Making a diamond with a null input does not make sense!");
            messageBuilder.Append(USAGE);

            throw new ForbiddenNullInputException(messageBuilder.ToString());
        }

        private static void FailForEmptyInput()
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendLine("Making a diamond with an empty input does not make sense!");
            messageBuilder.Append(USAGE);

            throw new ForbiddenEmptyInputException(messageBuilder.ToString());
        }

        private const string USAGE = @"
Usage: diamond letter
where letter is a valid uppercase or lowercase letter.";
    }

    public class ForbiddenNullInputException : Exception
    {
        public ForbiddenNullInputException(string message) : base(message) { }
    }

    public class ForbiddenEmptyInputException : Exception
    {
        public ForbiddenEmptyInputException(string message) : base(message) { }
    }

    public class ForbiddenNonLetterInputException : Exception
    {
        public ForbiddenNonLetterInputException(string message) : base(message) { }
    }
}
