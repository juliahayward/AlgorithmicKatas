using NUnit.Framework;

namespace UnitTestProject1
{
    // https://community.topcoder.com/stat?c=problem_statement&pm=1592
    [TestFixture]
    public class ChessMetricTests
    {
        [TestCase(3, new[] { 0, 0 }, new[] { 1, 0 }, 1, 1)]
        [TestCase(3, new[] { 0, 0 }, new[] { 1, 2 }, 1, 1)]
        [TestCase(3, new[] { 0, 0 }, new[] { 2, 2 }, 1, 0)]
        [TestCase(3, new[] { 0, 0 }, new[] { 0, 0 }, 2, 5)]
        [TestCase(100, new[] { 0, 0 }, new[] { 0, 99 }, 50, 243097320072600)]
        [TestCase(8, new[] { 4, 4 }, new[] { 4, 4 }, 6, 246460)]
        [TestCase(8, new[] { 2, 3 }, new[] { 7, 7 }, 9, 69232032)]
        [TestCase(3, new[] { 0, 0 }, new[] { 2, 2 }, 20, 979171322101760)]
        [TestCase(10, new[] { 5, 5 }, new[] { 9, 9 }, 4, 133)]
        [TestCase(13, new[] { 3, 7 }, new[] { 11, 5 }, 4, 4)]
        [TestCase(13, new[] { 3, 7 }, new[] { 11, 5 }, 14, 96417727286208)]
        [TestCase(100, new[] { 0, 0 }, new[] { 50, 50 }, 35, 480451056515520)]
        [TestCase(100, new[] { 0, 0 }, new[] { 50, 50 }, 34, 485001159390)]
        [TestCase(100, new[] { 99, 99 }, new[] { 0, 0 }, 50, 0)]
        [TestCase(100, new[] { 99, 99 }, new[] { 0, 0 }, 50, 0)]
        [TestCase(100, new[] { 99, 99 }, new[] { 0, 0 }, 50, 0)]
        [TestCase(3, new[] { 0, 2 }, new[] { 2, 0 }, 1, 0)]
        [TestCase(3, new[] { 0, 0 }, new[] { 0, 0 }, 1, 0)]
        public void ChessMetricTest(int size, int[] start, int[] end, int numMoves, long expectedCount)
        {
            long actualCount = HowMany(size, start, end, numMoves);

            Assert.AreEqual(expectedCount, actualCount);
        }

        public long HowMany(int boardSize, int[] start, int[] end, int numMoves)
        {
            // All the legal moves from where you are
            var legalMoves = new[]
            {
                // king moves
                new Move(1, 0), new Move(1, 1), new Move(0, 1), new Move(-1, 1), new Move(-1, 0), new Move(-1, -1),
                new Move(0, -1), new Move(1, -1),
                // knight moves
                new Move(1, 2), new Move(1, -2), new Move(-1, 2), new Move(-1, -2), new Move(2, 1), new Move(2, -1),
                new Move(-2, 1), new Move(-2, -1)
            };

            var moveNumber = 0;
            var board = new long[boardSize, boardSize];

            // One path of length 0 - to your starting point.
            board[start[0], start[1]] = 1;
            while (moveNumber < numMoves)
            {
                // The number of paths to a cell after (moveNumber) moves is the sum of the number of
                // paths to its predecessors the move before.
                var newboard = new long[boardSize, boardSize];
                for (int x=0; x < boardSize; x++)
                    for (int y=0; y < boardSize; y++)
                        foreach (var move in legalMoves)
                        {
                            var targetX = x + move.x;
                            var targetY = y + move.y;
                            if (targetX >= 0 && targetY >= 0 && targetX < boardSize && targetY < boardSize)
                                newboard[targetX, targetY] += board[x, y];
                        }

                board = newboard;
                moveNumber++;
            }

            return board[end[0], end[1]];
        }
    }
    
    public class Move
    {
        public int x { get; set; }
        public int y { get; set; }
        public Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
