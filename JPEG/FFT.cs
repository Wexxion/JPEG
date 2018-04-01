using System;
using System.Numerics;

namespace JPEG
{
    public class FFT
    {
        public static double[,] FFT2D(double[,] input, bool forward = true)
        {
            var width = input.GetLength(0);
            var height = input.GetLength(1);
            var coeffs = new Complex[width, height];
            var res = new double[width, height];
            var real = new double[width];
            var imag = new double[width];

            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                    real[i] = input[i, j];

                FFT1D(forward, real, imag);

                for (var i = 0; i < width; i++)
                    coeffs[i, j] = new Complex(real[i], imag[i]);
            }
     
            real = new double[height];
            imag = new double[height];

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    real[j] = coeffs[i, j].Real;
                    imag[j] = coeffs[i, j].Imaginary;
                }

                FFT1D(forward, real, imag);
                
                for (var j = 0; j < height; j++)
                    res[i, j] = real[j];
            }
            return res;
        }

        public static double[,] IFFT2D(double[,] coeffs) => FFT2D(coeffs, false);

        private static void FFT1D(bool forward, double[] x, double[] y)
        {
            long nn, i, i1, j, k, i2, l, l1, l2;
            double c1, c2, tx, ty, t1, t2, u1, u2, z;
            var m = 3;//(int) Math.Log(x.Length, 2);
            /* Calculate the number of points */
            nn = 1;
            for (i = 0; i < m; i++)
                nn *= 2;
            /* Do the bit reversal */
            i2 = nn >> 1;
            j = 0;
            for (i = 0; i < nn - 1; i++)
            {
                if (i < j)
                {
                    tx = x[i];
                    ty = y[i];
                    x[i] = x[j];
                    y[i] = y[j];
                    x[j] = tx;
                    y[j] = ty;
                }
                k = i2;
                while (k <= j)
                {
                    j -= k;
                    k >>= 1;
                }
                j += k;
            }
            /* Compute the FFT */
            c1 = -1.0;
            c2 = 0.0;
            l2 = 1;
            for (l = 0; l < m; l++)
            {
                l1 = l2;
                l2 <<= 1;
                u1 = 1.0;
                u2 = 0.0;
                for (j = 0; j < l1; j++)
                {
                    for (i = j; i < nn; i += l2)
                    {
                        i1 = i + l1;
                        t1 = u1 * x[i1] - u2 * y[i1];
                        t2 = u1 * y[i1] + u2 * x[i1];
                        x[i1] = x[i] - t1;
                        y[i1] = y[i] - t2;
                        x[i] += t1;
                        y[i] += t2;
                    }
                    z = u1 * c1 - u2 * c2;
                    u2 = u1 * c2 + u2 * c1;
                    u1 = z;
                }
                c2 = Math.Sqrt((1.0 - c1) / 2.0);
                if (forward)
                    c2 = -c2;
                c1 = Math.Sqrt((1.0 + c1) / 2.0);
            }
            /* Scaling for forward transform */
            if (forward)
            {
                for (i = 0; i < nn; i++)
                {
                    x[i] /= nn;
                    y[i] /= nn;

                }
            }



            //  return(true) ;
            return;
        }
    }
}