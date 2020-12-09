using System;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length == 0 ? null : args[0];

            var diamond = DiamondBuilder.MakeDiamondWith(input);

            Console.Write(diamond);
        }
    }
}
