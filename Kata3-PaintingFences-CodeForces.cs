using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace UnitTestProject1
{
    // http://codeforces.com/contest/448/problem/C
    [TestFixture]
    public class Kata3Fence
    {
        [TestCase(new[] { 5,2,2,5,1,1,5,2,2,5 }, 7)]
        [TestCase(new[] {2, 2, 1, 2, 1}, 3)]
        [TestCase(new[] {2, 2}, 2)]
        [TestCase(new[] {5}, 1)]
        public void TestCases(int[] heights, int expected)
        {
            var strokes = MinStrokesForFence(heights.ToList());
            Assert.AreEqual(strokes, expected);
        }

        public int MinStrokesForFence(List<int> heights)
        {
            if (!heights.Any()) return 0;

            // A vertical stroke doesn't help horizontal strokes in any way

            var strokesVerticalOnly = heights.Count(x => x > 0);

            // Any horizontal stroke that's not at the bottom of the remaining area is
            // clearly sub-optimal, so ignore; we'll assume a bottom-most stroke. Also, 
            // if horizontal strokes help then we will do enough to complete at least
            // one post. This divides the fence into separate pieces that are done independently

            var widthOfHorizontalStrokes = heights.Min();
            var strokesWithOneHorizontalNow = widthOfHorizontalStrokes; // the horizontal stroke
            var subList = new List<int>();
            foreach (var height in heights)
            {
                if (height > widthOfHorizontalStrokes)
                    subList.Add(height - widthOfHorizontalStrokes);
                else
                {
                    strokesWithOneHorizontalNow += MinStrokesForFence(subList);
                    subList.Clear();
                }
            }
            strokesWithOneHorizontalNow += MinStrokesForFence(subList);

            return Math.Min(strokesVerticalOnly, strokesWithOneHorizontalNow);
        }



    }
}
