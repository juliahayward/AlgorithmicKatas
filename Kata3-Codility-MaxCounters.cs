using System;

namespace UnitTestProject1
{
    // https://codility.com/demo/take-sample-test/max_counters/
    class Kata3_Codility_MaxCounters
    {
        public int[] solution(int N, int[] A)
        {
            var counters = new int[N];
            // Keep a running track of the highest counter so far
            var maxSoFar = 0;
            // Keep a running track of where the "max all" would take you to, if applied.
            var baseValue = 0;

            foreach (int operation in A)
            {
                if (operation < N + 1)
                {
                    // Danger - counters seem to be 1-based

                    // If we're below baseValue, this is because we have postponed updating it from [1]
                    if (counters[operation - 1] < baseValue)
                        counters[operation - 1] = baseValue;

                    counters[operation - 1]++;
                    maxSoFar = Math.Max(maxSoFar, counters[operation - 1]);
                }
                else
                {
                    // [1] Every counter should be incremented to baseValue now, but we defer actually doing
                    // the increment to avoid a potentially O(M*N) result. Counters are left with lower values 
                    // in them, but this represents baseValue.
                    baseValue = maxSoFar;
                }
            }

            // Finally, fix up all the deferred increments. 
            for (int i = 0; i < N; i++)
                if (counters[i] < baseValue)
                    counters[i] = baseValue;

            return counters;
        }
    }
}

