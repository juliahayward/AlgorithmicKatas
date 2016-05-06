using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    /// <summary>
    /// https://community.topcoder.com/stat?c=problem_statement&pm=11967
    /// </summary>
    [TestClass]
    public class KingdomAndTrees
    {
        [TestMethod]
        public void TestMethod1()
        {
            var output = MinLevel(new [] { 9,5,11 });
            Assert.AreEqual(output, 3);
            output = MinLevel(new [] { 5,8 });
            Assert.AreEqual(output, 0);
            output = MinLevel(new [] { 1,1,1,1,1 });
            Assert.AreEqual(output, 4);
            output = MinLevel(new [] { 548,47,58,250,2012 });
            Assert.AreEqual(output, 251);

        }

        public int MinLevel(int[] treeHeights)
        {
            // The up-or-down-by-N spell is essentially equivalent to an up-by-2N-then-shrink-all-trees-by-same-amount
            // spell. The latter case is rather simpler to solve as we just up each tree to be higher than its predecessor;
            // so we get the answer to the former by upping everything, then shrinking by as much as we can.
            int biggestUppedness = 0;
            for (int i = 1; i < treeHeights.Length; i++)
            {
                var uppedness = treeHeights[i - 1] - treeHeights[i] + 1;
                if (uppedness > biggestUppedness)
                    biggestUppedness = uppedness;
                treeHeights[i] += uppedness;
            }
            // We shrink all trees by (biggestUppedness/2 rounded down) to minimise the spell - except when the height
            // of the first tree is a constraint (as we can't shrink to 0)
            int maxPossibleShrink = (int)Math.Min(biggestUppedness/2, treeHeights[0] - 1);
            return biggestUppedness - maxPossibleShrink;
        }

    }
}
