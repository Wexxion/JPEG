using CenterSpace.NMath.Core;

namespace JPEG
{
    public class SFFT
    {
        public static double[,] FFT2D(double[,] input)
        {
            var matrix = new DoubleMatrix(input);
            var fft2d = new DoubleForward2DFFT(8, 8);
            var fftdata = new DoubleMatrix(fft2d.Rows, fft2d.Columns);

            // Compute the 2D FFT.
            fft2d.FFT(matrix, ref fftdata);
            var res = fftdata.ToArray();
            return res;
        }
        public static double[,] IFFT2D(double[,] coeffs)
        {
            var matrixb = new DoubleComplex[8, 8];
            for (int i = 0; i < coeffs.GetLength(0); i++)
            for (int j = 0; j < coeffs.GetLength(1); j++)
                matrixb[i, j] = new DoubleComplex(coeffs[i, j]);
            var fft2db = new DoubleComplexBackward2DFFT(8, 8);
            var fftbdata = new DoubleComplex[8, 8];

            // Compute the 2D FFT.
            fft2db.FFT(matrixb, ref fftbdata);
            var res = new double[8, 8];
            for (int i = 0; i < fftbdata.GetLength(0); i++)
            for (int j = 0; j < fftbdata.GetLength(1); j++)
                res[i, j] = fftbdata[i,j].Real;
            return res;
        }
    }
}