using System;
using System.Collections.Generic;
using System.Linq;


class Solution {
    public int solution(int X, int[] A) {
        var Leaves = new bool[X];       // Will track where leaves are
        var howFar = 0;                 // Will track how far the frog can get.
        for (int i=0; i<A.Length; i++)
        {
            // If a leaf has already fallen here, do nothing.
            if (!Leaves[A[i] - 1])
            {
                Console.WriteLine("Leaf has fallen at " + A[i]);
                Leaves[A[i] - 1] = true;
                // Has leaf fallen immediately in front of frog? If so, advance.
                if (A[i] == howFar + 1) howFar++;
                // Are there further leaves in front of us? If so, advance
                while (howFar < X && Leaves[howFar]) howFar++;
                Console.WriteLine("Frog can advance to " + howFar);
                // Have we reached the far bank?
                if (howFar == X) return i;
            }
        }
        return -1;
    }
}
