using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var intput = new List<int> { 5, 4, 1, 8, 7, 2, 6, 3 };

            var sorted = SortAscending(intput);

            Console.WriteLine(string.Join(' ', sorted));
            Console.Read();
        }

        private static List<int> SortAscending(List<int> input)
        {
            var array = input.ToList();
            if (array.Count == 2)
            {
                return array[0] > array[1]
                    ? new List<int> { array[1], array[0] }
                    : new List<int> { array[0], array[1] };
            }

            var halfLength = array.Count / 2;
            var leftPart = SortAscending(input.Take(halfLength).ToList());
            var rightPart = SortAscending(input.Skip(halfLength).ToList());

            var result = MergeHalfs(leftPart, rightPart);

            return result;
        }

        private static List<int> MergeHalfs(List<int> left, List<int> right)
        {
            var leftIndex = 0;
            var rightIndex = 0;

            var totalLength = left.Count * 2;
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