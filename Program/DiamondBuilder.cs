using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Program
{
    public class DiamondBuilder
    {
        public static string MakeDiamondWith(string input)
        {
            FailForInvalidInput(input);

            char letter = input[0];

            if (isA(letter))
                return input;

            string diamond = string.Empty;

            if (isB(letter))
                diamond = @" a 
b b
 a ";

            if (isC(letter))
                diamond = @"  a  
 b b 
c   c
 b b 
  a  ";

            return char.IsLower(letter) ? diamond : diamond.ToUpper();
        }

        public static string BuildDiamondMiddleLine(char letter)
        {
            return letter switch
            {
                'a' => "a",
                'A' => "A",
                'b' => "b b",
                'B' => "B B",
                'c' => "c   c",
                'C' => "C   C",
                _ => string.Empty
            };
        }

        private static bool isA(char letter) => letter == 'a' || letter == 'A';
        private static bool isB(char letter) => letter == 'b' || letter == 'B';
        private static bool isC(char letter) => letter == 'c' || letter == 'C';

        private static void FailForInvalidInput(string input)
        {
            if (input == null)
                FailForNullInput();

            if (input == string.Empty)
                FailForEmptyInput();

            if (isNotLetter(input))
                FailForNonLetterInput();
        }

        private static void FailForNonLetterInput()
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendLine("You can create a diamond only with a single letter!");
            messageBuilder.Append(USAGE);

            throw new ForbiddenNonLetterInputException(messageBuilder.ToString());
        }

        private static bool isNotLetter(string input)
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
