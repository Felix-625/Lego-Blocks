using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'legoBlocks' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER m
     */

    public static int legoBlocks(int n, int m)
    {
        long MOD = 1000000007;
    
    // Step 1: Calculate ways to build a single row of width m
    long[] singleRow = new long[Math.Max(m + 1, 5)];
    singleRow[0] = 1;
    
    for (int i = 1; i <= m; i++)
    {
        singleRow[i] = singleRow[i - 1];
        if (i >= 2) singleRow[i] = (singleRow[i] + singleRow[i - 2]) % MOD;
        if (i >= 3) singleRow[i] = (singleRow[i] + singleRow[i - 3]) % MOD;
        if (i >= 4) singleRow[i] = (singleRow[i] + singleRow[i - 4]) % MOD;
    }
    
    // Step 2: Calculate all possible walls (including bad ones with vertical lines)
    long[] allWalls = new long[m + 1];
    for (int i = 1; i <= m; i++)
    {
        allWalls[i] = 1;
        for (int j = 0; j < n; j++)
        {
            allWalls[i] = (allWalls[i] * singleRow[i]) % MOD;
        }
    }
    
    // Step 3: Calculate good walls (no vertical lines through all rows)
    long[] goodWalls = new long[m + 1];
    goodWalls[1] = 1; // For width 1, only one way and it's always good
    
    for (int i = 2; i <= m; i++)
    {
        goodWalls[i] = allWalls[i];
        
        // Subtract bad walls that have at least one vertical line
        for (int j = 1; j < i; j++)
        {
            long bad = (goodWalls[j] * allWalls[i - j]) % MOD;
            goodWalls[i] = (goodWalls[i] - bad + MOD) % MOD;
        }
    }
    
    return (int)(goodWalls[m] % MOD);
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for (int tItr = 0; tItr < t; tItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int m = Convert.ToInt32(firstMultipleInput[1]);

            int result = Result.legoBlocks(n, m);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
