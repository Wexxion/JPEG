﻿using System;
using JPEG;
using JPEG.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPEG_Tests
{
    [TestClass]
    public class PerformanceTests
    {
        private readonly double[,] input = {
            {44.43612109375, 0.927800781249971, -46.9039765625, -28.2218359375, -24.09798046875, 27.87130078125, 24.1607109375, 9.42807421875 },
            {38.12615625, -41.79703515625, -51.75462109375, -37.86015625, 42.360265625, -12.396390625, -40.5351640625, 6.23374609374997 },
            {-28.6166953125, -62.17589453125, -57.3000390625, 26.86988671875, -19.13802734375, -60.738609375, -60.462921875, -33.53567578125 },
            {-44.38280078125, -58.677984375, -60.09637109375, -42.7701015625, -61.10462890625, -60.5065, -47.1476484375, -31.43725390625 },
            {-22.628640625, -51.50361328125, -70.68674609375, -44.648546875, -29.75209765625, -30.5256015625, -63.3873828125, -58.97521484375 },
            {25.5391875, -53.38886328125, -52.4961484375, -60.331125, -65.7357890625, -62.2662265625, -58.24842578125, -57.4439296875 },
            {45.3162109375, -7.28483593750001, 16.70532421875, -5.47155078125002, -43.06737109375, -62.1974375, -60.656703125, 6.99776171874998 },
            {66.5846875, 44.53846875, 55.7238359375, 25.67248828125, 13.9210859375, -27.93042578125, -0.282664062500018, 61.392703125 }};

        [TestMethod]
        public void SumLinq()
        {
            var height = input.GetLength(0);
            var width = input.GetLength(1);
            var coeffs = new double[width, height];
            for (int i = 0; i < 8192 * 3; i++)
                for (var u = 0; u < width; u++)
                    for (var v = 0; v < height; v++)
                    {
                        coeffs[u, v] = MathEx
                            .SumByTwoVariables(
                                0, width,
                                0, height,
                                (x, y) => DCT.BasisFunction(input[x, y], u, v, x, y, height, width));
                    }
        }
        [TestMethod]
        public void DoSomething_WhenSomething()
        {
            var a = new double[,] {{1, 2}, {3, 4}};
            var res = MathEx.SumByTwoVariables(0, 2, 0, 2, (x, y) => x + y);
        }
    }
}