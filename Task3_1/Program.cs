using System;

namespace Task3_1
{
    public class Program

    {
        private static void Main()

        {
            try
            {
                var rationalNumberA = RationalNumber.Parse("5/1");
                var rationalNumberB = RationalNumber.Parse("-7/2");
                long a = rationalNumberB;
                var rationalNumberC = new RationalNumber(5);
                Console.WriteLine(rationalNumberA.ToString());
                Console.WriteLine((rationalNumberB * rationalNumberA).ToString());
                Console.WriteLine(new RationalNumber(7, 13) == new RationalNumber(4));
                Console.WriteLine(rationalNumberC.Equals(rationalNumberA));
                Console.WriteLine(a);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}