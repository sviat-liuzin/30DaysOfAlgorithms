using System;

namespace Karatsuba
{
    class Program
    {
        static void Main(string[] args)
        {
            var input1 = 3344;
            var input2 = 5566;

            var result = KaratsubaMultiply(input1, input2);

            Console.WriteLine($"{input1} x {input2} = {result}");
            Console.Read();
        }

        private static int KaratsubaMultiply(int input1, int input2)
        {
            var numOfDigits1 = GetNumberOfDigits(input1);
            var numOfDigits2 = GetNumberOfDigits(input2);
            if (numOfDigits1 == 1 || numOfDigits2 == 1)
            {
                return input1 * input2;
            }

            var (senior1, junior1) = DivideDigits(input1);
            var (senior2, junior2) = DivideDigits(input2);

            var sum1 = senior1 + junior1;
            var sum2 = senior2 + junior2;

            var mult1 = KaratsubaMultiply(senior1, senior2);
            var mult2 = KaratsubaMultiply(junior1, junior2);
            var mult3 = KaratsubaMultiply(sum1, sum2);

            var middle = mult3 - mult2 - mult1;

            var result = Math.Pow(10, numOfDigits1) * mult1 + Math.Pow(10, numOfDigits1 / 2) * middle + mult2;

            return (int)result;
        }

        private static int GetNumberOfDigits(int input)
        {
            return (int)Math.Floor(Math.Log10(input)) + 1;
        }

        private static (int, int) DivideDigits(int input)
        {
            var halfLength = GetNumberOfDigits(input) / 2;

            var junior = (input % Math.Pow(10, halfLength));
            var senior = (input - junior) / Math.Pow(10, halfLength);

            return ((int)senior, (int)junior);
        }
    }
}