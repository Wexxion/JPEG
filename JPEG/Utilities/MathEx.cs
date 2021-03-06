﻿using System;
using System.Linq;

namespace JPEG.Utilities
{
    public static class MathEx
    {
        public static double Sum(int from, int to, Func<int, double> function)
        {
            double res = 0;
            for (int i = from; i < to; i++)
                res += function(i);
            return res;
        }
		
        public static double SumByTwoVariables(int from1, int to1, int from2, int to2, Func<int, int, double> function)
        {
            return Sum(from1, to1, x => Sum(from2, to2, y => function(x, y)));
        }
    }
}