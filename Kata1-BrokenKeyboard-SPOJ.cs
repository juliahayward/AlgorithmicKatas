using System;
using System.Collections.Generic;
using System.Linq;

public class Test
{
    public static void Main()
    {
        var maxDifferentChars = int.Parse(Console.ReadLine());
        if (maxDifferentChars == 0) return;
        var text = Console.ReadLine();
        Console.WriteLine(BROKEN(text, maxDifferentChars));
    }

    public static int BROKEN(string text, int maxDifferentChars)
    {
        var l = text.Length;
        int startIndex = 0, endIndex = 0;
        List<char> differentChars = new List<char>();
        var maxString = 0;
        while (startIndex < l - 1 && endIndex < l)
        {
            var newChar = text[endIndex];
            if (differentChars.Contains(newChar)) // this one's already in our set
            {
                endIndex += 1;
                if (maxString < endIndex - startIndex)
                    maxString = endIndex - startIndex;
            }
            else if (differentChars.Count() < maxDifferentChars) // We can cope with one more
            {
                differentChars.Add(newChar);
                endIndex += 1;
                if (maxString < endIndex - startIndex)
                    maxString = endIndex - startIndex;
            }
            else // OK, can't continue until we've dropped something off the start;
            {
                var droppingOutChar = text[startIndex];
                startIndex += 1;
                // Check to make sure it's not in our range of interest any more
                if (!isCharInRange(startIndex, endIndex, text, droppingOutChar))
                    differentChars.Remove(droppingOutChar);
            }
        }

        return maxString;
    }

    private static bool isCharInRange(int startIndex, int endIndex, string text, char droppingOutChar)
    {
        for (int i = startIndex; i < endIndex; i++)
            if (text[i] == droppingOutChar)
                return true;

        return false;
    }
}
