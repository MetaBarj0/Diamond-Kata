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
                throw new ForbiddenNullInputException(@"Making a diamond with a null input does not make sense!
Usage: diamond letter
where letter is a valid uppercase or lowercase letter.");

            throw new ForbiddenEmptyInputException(@"Making a diamond with an empty input does not make sense!
Usage: diamond letter
where letter is a valid uppercase or lowercase letter.");
        }
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
