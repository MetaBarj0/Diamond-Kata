using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    public class DiamondBuilder
    {
        public static string MakeDiamondWith(string input)
        {
            if (input == null)
                FailForNullInput();

            FailForEmptyInput();

            throw new NotImplementedException();
        }

        private static void FailForNullInput()
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendLine("Making a diamond with a null input does not make sense!");
            messageBuilder.Append(@"
Usage: diamond letter
where letter is a valid uppercase or lowercase letter.");

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
}
