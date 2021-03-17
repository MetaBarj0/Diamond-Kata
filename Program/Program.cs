using System;
using Library;

namespace Program
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var input = args.Length == 0 ? string.Empty : args[0];

            var diamond = DiamondBuilder.MakeDiamondWith(input);

            Console.Write(diamond);
        }
    }
}
