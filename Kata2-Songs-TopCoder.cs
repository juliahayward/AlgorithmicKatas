using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    /// <summary>
    /// https://community.topcoder.com/stat?c=problem_statement&pm=14123
    /// </summary>
    [TestClass]
    public class ListeningSongs
    {
        [TestMethod]
        public void TestMethod1()
        {
            var output = MaxNumberOfSongs(new List<int>() { 300,200,100 }, new List<int>() { 400,500,600 }, 17, 1);
            Assert.AreEqual(output, 4);
            output = MaxNumberOfSongs(new List<int>() { 300, 200, 100 }, new List<int>() { 400, 500, 600 }, 10, 1);
            Assert.AreEqual(output, 2);
            output = MaxNumberOfSongs(new List<int>() { 60, 60, 60 }, new List<int>() { 60, 60, 60 }, 5, 2);
            Assert.AreEqual(output, 5);
            output = MaxNumberOfSongs(new List<int>() { 120, 120, 120, 120, 120, 120}, new List<int>() { 60, 60, 60, 60, 60, 60 }, 10, 3);
            Assert.AreEqual(output, 7);
            output = MaxNumberOfSongs(new List<int>() { 196, 147, 201, 106, 239, 332, 165, 130, 205, 221, 248, 108, 60 }, new List<int>() { 280, 164, 206, 95, 81, 383, 96, 255, 260, 244, 60, 313, 101 }, 60, 3);
            Assert.AreEqual(output, 22);
            output = MaxNumberOfSongs(new List<int>() { 100, 200, 300 }, new List<int>() { 100, 200, 300 }, 2, 1);
            Assert.AreEqual(output, -1);
            output = MaxNumberOfSongs(new List<int>() { 100,200,300,400,500,600 }, new List<int>() { 100,200,300 }, 2, 1);
            Assert.AreEqual(output, -1);
        }

        public int MaxNumberOfSongs(List<int> durations1, List<int> durations2, int minutes, int T)
        {
            // Not enough to pick T out of either list;
            if (durations1.Count() < T || durations2.Count() < T) return -1;
            var secondsAvailable = minutes*60;

            // There is no benefit at all in picking a longer song over a shorter one - we're not trying to maximise
            // time fill or enjoyment, just number of songs. So we pick the shortest T from each list (to meet that constraint)
            // then pick the shortest  from the combined list until time is full.
            durations1.Sort();
            durations2.Sort();
            // Add end-markers which can never be picked unless we have effectively infinite time
            durations1.Add(int.MaxValue);
            durations2.Add(int.MaxValue);
            int timeUsed = 0, takenFrom1 = 0, takenFrom2 = 0;
            while (true)
            {
                /* We need at least one more from 1st album */
                if (takenFrom1 < T) 
                {
                    timeUsed += durations1.First();
                    if (timeUsed > secondsAvailable) return -1; // couldn't fit minimum number in
                    takenFrom1++;
                    durations1.RemoveAt(0);
                }
                /* We need at least one more from 2nd album */
                else if (takenFrom2 < T)
                {
                    timeUsed += durations2.First();
                    if (timeUsed > secondsAvailable) return -1; // couldn't fit minimum number in
                    takenFrom2++;
                    durations2.RemoveAt(0);
                }
                /* next 1st-album song is the shortest remaining and we have enough time left */
                else if (durations1.Any() && durations1.First() <= durations2.First() && durations1.First() + timeUsed <= secondsAvailable)
                {
                    timeUsed += durations1.First();
                    takenFrom1++;
                    durations1.RemoveAt(0);
                }
                /* Next 2nd-album song is the shortest remaining and we have enough time left */
                else if (durations2.Any() && durations2.First() <= durations1.First() && durations2.First() + timeUsed <= secondsAvailable)
                {
                    timeUsed += durations2.First();
                    takenFrom2++;
                    durations2.RemoveAt(0);
                }
                else
                {
                    /* Have met our quotas, and can't fit another one in */
                    return takenFrom1 + takenFrom2;
                }
            }
        }

    }
}
