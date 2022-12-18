using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new []
            {
                1000000,
                1100000,
                1200000,
                1300000,
                1400000,
                1500000,
                1600000,
                1700000,
                1800000,
                1900000,
                2000000
            };

            var outputBy2 = input
                .Select(i => TestMergeSort(i, true))
                .ToList();

            var outputBy3 = input
                .Select(i => TestMergeSort(i, false))
                .ToList();

            Console.WriteLine(string.Join(',', input));
            Console.WriteLine(string.Join(',', outputBy2));
            Console.WriteLine(string.Join(',', outputBy3));

            Console.Read();
        }

        private static long TestMergeSort(int arrayLength, bool isDouble)
        {
            Random randNum = new Random();
            var input = Enumerable
                .Repeat(0, arrayLength)
                .Select(i => randNum.Next(Int32.MinValue, Int32.MaxValue))
                .ToList();

            var watch = new Stopwatch();
            watch.Start();
            var sorted = isDouble
                ? SortBy2Ascending(input)
                : SortBy3Ascending(input);

            watch.Stop();

            //Console.WriteLine($"Input:  {string.Join(' ', input)}");
            //Console.WriteLine($"Sorted: {string.Join(' ', sorted)}");
            //Console.WriteLine($"Length: {arrayLength}. Execution time: {watch.ElapsedMilliseconds} milliseconds");

            return watch.ElapsedMilliseconds;
        }

        private static List<int> SortBy3Ascending(List<int> input)
        {
            switch(input.Count)
            {
                case 0:
                case 1:
                    return input;
                case 2:
                    return input[0] > input[1]
                        ? new List<int> { input[1], input[0] }
                        : new List<int> { input[0], input[1] };
                default:
                    var thirdLength = (int)Math.Floor((double)input.Count / 3);
                    var leftPart = SortBy3Ascending(input.Take(thirdLength).ToList());
                    var middlePart = SortBy3Ascending(input.Skip(thirdLength).Take(thirdLength).ToList());
                    var rightPart = SortBy3Ascending(input.TakeLast(input.Count - 2*thirdLength).ToList());

                    var result = MergeThirds(leftPart, middlePart, rightPart);

                    return result;
            }
        }

        private static List<int> MergeThirds(List<int> leftPart, List<int> middlePart, List<int> rightPart)
        {
            var leftIndex = 0;
            var middleIndex = 0;
            var rightIndex = 0;

            var totalLength = leftPart.Count + middlePart.Count + rightPart.Count;
            var result = new List<int>();

            for(int i = 0; i < totalLength; i++)
            {
                if (leftIndex < leftPart.Count &&
                    leftPart[leftIndex] < (middleIndex >= middlePart.Count ? Int32.MaxValue : middlePart[middleIndex]) &&
                    leftPart[leftIndex] < (rightIndex >= rightPart.Count ? Int32.MaxValue : rightPart[rightIndex]))
                {
                    result.Add(leftPart[leftIndex]);
                    leftIndex++;
                }
                else if (middleIndex < middlePart.Count &&
                         middlePart[middleIndex] < (rightIndex >= rightPart.Count ? Int32.MaxValue : rightPart[rightIndex]))
                {
                    result.Add(middlePart[middleIndex]);
                    middleIndex++;
                }
                else
                {
                    result.Add(rightPart[rightIndex]);
                    rightIndex++;
                }
            }

            return result;
        }

        private static List<int> SortBy2Ascending(List<int> input)
        {
            switch(input.Count)
            {
                case 0:
                case 1:
                    return input;
                case 2:
                    return input[0] > input[1]
                        ? new List<int> { input[1], input[0] }
                        : new List<int> { input[0], input[1] };
                default:
                    var halfLength = input.Count / 2;
                    var leftPart = SortBy2Ascending(input.Take(halfLength).ToList());
                    var rightPart = SortBy2Ascending(input.Skip(halfLength).ToList());

                    var result = MergeHalfs(leftPart, rightPart);

                    return result;
            }
        }

        private static List<int> MergeHalfs(List<int> left, List<int> right)
        {
            var leftIndex = 0;
            var rightIndex = 0;

            var totalLength = left.Count + right.Count;
            var result = new List<int>();

            for(var i = 0; i < totalLength; i++)
            {
                if(leftIndex == left.Count)
                {
                    result.AddRange(right.Skip(rightIndex));
                    break;
                }
                if(rightIndex == right.Count)
                {
                    result.AddRange(left.Skip(leftIndex));
                    break;
                }

                if (left[leftIndex] > right[rightIndex])
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
                else
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
            }

            return result;
        }
    }
}