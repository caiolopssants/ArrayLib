using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

using ArrayLib_V1_0;
namespace ArrayOperationsConsoleTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {            
            #region Teste 4 - Matriz identidade/ Matriz inversa/ Determinante
            //if (true)
            //{
            //    object valueT = ArrayLib.GetJaggedArrayFromFile(usedType, $@"G:\Machine Learning\C#\array operations\ArrayOperationsConsoleTest\TabDouble.csv", ';', Encoding.UTF8);
            //    object valueT = ArrayLib.ConvertJaggedArrayToNewJaggedArrayType(ArrayLib.ValuesType.Byte, ArrayLib.ValuesType.Double, ArrayLib.NewRandomJaggedArray(ArrayLib.ValuesType.Byte, 4, 4));
            //    string infs = string.Empty;

            //    if (true)
            //    {
            //        infs = string.Empty;
            //        valueT = ArrayLib.ConvertJaggedArrayToNewJaggedArrayType(ArrayLib.ValuesType.Byte, ArrayLib.ValuesType.Double, ArrayAritmeticOperationsLib.IdentityArray(15).Item2);
            //        foreach (double[] doubles in (double[][])(valueT))
            //        {
            //            foreach (double @double in doubles)
            //            {
            //                switch ($"{@double}".Length)
            //                {
            //                    case 1:
            //                        infs += $"{@double}      ";
            //                        break;
            //                    case 2:
            //                        infs += $"{@double}    ";
            //                        break;
            //                    case 3:
            //                        infs += $"{@double}  ";
            //                        break;
            //                }


            //            }
            //            infs += "\n";
            //        }
            //        MessageBox.Show(infs);
            //    }
            //    if (true)
            //    {
            //        infs = string.Empty;
            //        valueT = ArrayAritmeticOperationsLib.InverseJaggedArray(ArrayAritmeticOperationsLib.ValuesType.Double, valueT);
            //        foreach (double[] doubles in (double[][])(valueT))
            //        {
            //            foreach (double @double in doubles)
            //            {
            //                MessageBox.Show($"{@double}");
            //            }
            //        }

            //    }
            //    if (true)
            //    {

            //    }
            //}
            #endregion

            #region Teste 3 - testando inversão de matriz no site da microsoft
            //if (true)
            //{

            //    object valueT = ArrayLib.GetJaggedArrayFromFile(usedType, $@"G:\Machine Learning\C#\array operations\ArrayOperationsConsoleTest\TabDouble.csv", ';', Encoding.UTF8);
            //    object valueT = ArrayLib.ConvertJaggedArrayToNewJaggedArrayType(ArrayLib.ValuesType.Byte, ArrayLib.ValuesType.Double, ArrayLib.NewRandomJaggedArray(ArrayLib.ValuesType.Byte, 4, 4));
            //    string infs = string.Empty;
            //    object dbl = MicrosoftClass.MatrixDecompose((double[][])valueT, out int[] perm, out int toggle);
            //    object inv = MicrosoftClass.MatrixInverse((double[][])valueT);



            //    infs = string.Empty;
            //    foreach (double[] doubles in (double[][])valueT)
            //    {
            //        foreach (double @double in doubles)
            //        {
            //            switch ($"{@double}".Length)
            //            {
            //                case 1:
            //                    infs += $"{@double}      ";
            //                    break;
            //                case 2:
            //                    infs += $"{@double}    ";
            //                    break;
            //                case 3:
            //                    infs += $"{@double}  ";
            //                    break;
            //            }

            //        }
            //        infs += "\n";
            //    }
            //    MessageBox.Show(infs);
            //    infs = string.Empty;
            //    foreach (double[] doubles in dbl)
            //    {
            //        foreach (double @double in doubles)
            //        {
            //            switch ($"{@double}".Length)
            //            {
            //                case 1:
            //                    infs += $"{@double}      ";
            //                    break;
            //                case 2:
            //                    infs += $"{@double}    ";
            //                    break;
            //                case 3:
            //                    infs += $"{@double}  ";
            //                    break;
            //            }
            //        }
            //        infs += "\n";
            //    }
            //    MessageBox.Show(infs);
            //    infs = string.Empty;

            //    foreach (double @double in perm)
            //    {
            //        switch ($"{@double}".Length)
            //        {
            //            case 1:
            //                infs += $"{@double}      ";
            //                break;
            //            case 2:
            //                infs += $"{@double}    ";
            //                break;
            //            case 3:
            //                infs += $"{@double}  ";
            //                break;
            //        }
            //    }
            //    infs += "\n";
            //    MessageBox.Show(infs);


            //    infs = string.Empty;
            //    foreach (double[] doubles in inv)
            //    {
            //        foreach (double @double in doubles)
            //        {
            //            MessageBox.Show($"{@double}");
            //        }

            //    }


            //    foreach (double[] doubles in (double[][])ArrayAritmeticOperationsLib.MultiplicationJaggedArray(ArrayAritmeticOperationsLib.ValuesType.Double, valueT, ArrayAritmeticOperationsLib.ValuesType.Double, inv).Item2)
            //    {
            //        foreach (double @double in doubles)
            //        {
            //            MessageBox.Show($"{@double}");
            //        }
            //    }



            //}
            #endregion

            #region Teste 2 - soma/subtração/multiplicação
            //if (true)
            //{
            //    ArrayLib.ValuesType usedType = ArrayLib.ValuesType.Double;
            //    object valueT = ArrayLib.GetJaggedArrayFromFile(usedType, $@"G:\Machine Learning\C#\array operations\ArrayOperationsConsoleTest\TabDouble.csv", ';', Encoding.UTF8);
            //    string infs = string.Empty;

            //    if (true)
            //    {
            //        infs = string.Empty;
            //        valueT = ArrayAritmeticOperationsLib.SumJaggedArray(ArrayAritmeticOperationsLib.ValuesType.Double, valueT, ArrayAritmeticOperationsLib.ValuesType.Double, valueT);
            //        foreach (double[] doubles in (double[][])((Tuple<ArrayAritmeticOperationsLib.ValuesType, object>)valueT).Item2)
            //        {
            //            foreach (double @double in doubles)
            //            {
            //                switch ($"{@double}".Length)
            //                {
            //                    case 1:
            //                        infs += $"{@double}      ";
            //                        break;
            //                    case 2:
            //                        infs += $"{@double}    ";
            //                        break;
            //                    case 3:
            //                        infs += $"{@double}  ";
            //                        break;
            //                }


            //            }
            //            infs += "\n";
            //        }
            //        MessageBox.Show(infs);
            //    }
            //    if (true)
            //    {
            //        infs = string.Empty;
            //        valueT = ArrayAritmeticOperationsLib.SubtractionJaggedArray(ArrayAritmeticOperationsLib.ValuesType.Double, valueT, ArrayAritmeticOperationsLib.ValuesType.Double, valueT);
            //        foreach (double[] doubles in (double[][])((Tuple<ArrayAritmeticOperationsLib.ValuesType, object>)valueT).Item2)
            //        {
            //            foreach (double @double in doubles)
            //            {
            //                switch ($"{@double}".Length)
            //                {
            //                    case 1:
            //                        infs += $"{@double}      ";
            //                        break;
            //                    case 2:
            //                        infs += $"{@double}    ";
            //                        break;
            //                    case 3:
            //                        infs += $"{@double}  ";
            //                        break;
            //                }


            //            }
            //            infs += "\n";
            //        }
            //        MessageBox.Show(infs);
            //    }
            //    if (true)
            //    {
            //        infs = string.Empty;
            //        valueT = ArrayAritmeticOperationsLib.MultiplicationJaggedArray(ArrayAritmeticOperationsLib.ValuesType.Double, valueT, ArrayAritmeticOperationsLib.ValuesType.Double, valueT);
            //        foreach (double[] doubles in (double[][])((Tuple<ArrayAritmeticOperationsLib.ValuesType, object>)valueT).Item2)
            //        {
            //            foreach (double @double in doubles)
            //            {
            //                switch ($"{@double}".Length)
            //                {
            //                    case 1:
            //                        infs += $"{@double}      ";
            //                        break;
            //                    case 2:
            //                        infs += $"{@double}    ";
            //                        break;
            //                    case 3:
            //                        infs += $"{@double}  ";
            //                        break;
            //                }


            //            }
            //            infs += "\n";
            //        }
            //        MessageBox.Show(infs);
            //    }
            //}
            #endregion

            #region Teste 1 - visuTest

            //MessageBox.Show($"{ArrayLib.GetValue(ArrayLib.ValuesType.Double,ArrayLib.GetJaggedArrayRowSize(ArrayLib.ValuesType.Double, valueT)-1, ArrayLib.GetJaggedArrayColumnSize(ArrayLib.ValuesType.Double, valueT)-1,valueT)}");

            //MessageBox.Show($"{ArrayLib.GetJaggedArrayRowSize(ArrayLib.ValuesType.Double, valueT)}");
            //MessageBox.Show($"{ArrayLib.GetJaggedArrayColumnSize(ArrayLib.ValuesType.Double, valueT)}");

            //string infs = string.Empty;
            //foreach (double[] doubles in (double[][])valueT)
            //{
            //    foreach (double @double in doubles)
            //    {
            //        switch ($"{@double}".Length)
            //        {
            //            case 1:
            //                infs += $"{@double}      ";
            //                break;
            //            case 2:
            //                infs += $"{@double}    ";
            //                break;
            //            case 3:
            //                infs += $"{@double}  ";
            //                break;
            //        }


            //    }
            //    infs += "\n";
            //}
            //MessageBox.Show(infs);


            //string infs = string.Empty;
            //foreach (double[] doubles in (double[][])valueT)
            //{
            //    foreach (double @double in doubles)
            //    {
            //        MessageBox.Show($"{@double}");
            //    }                
            //}
            #endregion
        }        
    }




    static class MicrosoftClass
    {
        static double[][] MatrixCreate(int rows, int cols)
        {   /*// creates a matrix initialized to all 0.0s   // do error checking here?*/
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
            {
                result[i] = new double[cols];
            } /*// auto init to 0.0*/
            return result;
        }
        static double[][] MatrixDuplicate(double[][] matrix)
        {   // assumes matrix is not null.
            double[][] result = MatrixCreate(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; ++i)
            {
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    result[i][j] = matrix[i][j];
                }
            }
            return result;
        }
        public static double[][] MatrixDecompose(double[][] matrix, out int[] perm, out int toggle)
        {   // Doolittle LUP decomposition.
            // assumes matrix is square.
            int n = matrix.Length; /*// convenience */
            double[][] result = MatrixDuplicate(matrix);
            perm = new int[n];
            for (int i = 0; i < n; ++i)
            {
                perm[i] = i;
            }
            toggle = 1;
            for (int j = 0; j < n - 1; ++j) /*// each column */
            {
                double colMax = Math.Abs(result[j][j]); /*// largest val in col j*/
                int pRow = j;
                for (int i = j + 1; i < n; ++i)
                {
                    if (result[i][j] > colMax)
                    {
                        colMax = result[i][j];
                        pRow = i;
                    }
                }
                if (pRow != j) /*// swap rows*/
                {
                    double[] rowPtr = result[pRow];
                    result[pRow] = result[j];
                    result[j] = rowPtr;
                    int tmp = perm[pRow]; /*// and swap perm info*/
                    perm[pRow] = perm[j];
                    perm[j] = tmp;
                    toggle = -toggle; /*// row-swap toggle*/
                }
                if (Math.Abs(result[j][j]) < 1.0E-20)
                {
                    return null; /*// consider a throw*/
                }
                for (int i = j + 1; i < n; ++i)
                {
                    result[i][j] /= result[j][j];
                    for (int k = j + 1; k < n; ++k)
                    {
                        result[i][k] -= result[i][j] * result[j][k];
                    }
                }
            } /*// main j column loop*/
            return result;
        }        
        static double[] HelperSolve(double[][] luMatrix, double[] b)
        {   /* // solve luMatrix * x = b */
            int n = luMatrix.Length;
            double[] x = new double[n];
            b.CopyTo(x, 0);
            for (int i = 1; i < n; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                {
                    sum -= luMatrix[i][j] * x[j];
                }
                x[i] = sum;
            }
            x[n - 1] /= luMatrix[n - 1][n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j)
                {
                    sum -= luMatrix[i][j] * x[j];
                }
                x[i] = sum / luMatrix[i][i];
            }
            return x;
        }
        public static double[][] MatrixInverse(double[][] matrix)
        {
            int n = matrix.Length;
            double[][] result = MatrixDuplicate(matrix);

            double[][] lum = MatrixDecompose(matrix, out int[] perm, out int toggle);
            if (lum == null) throw new Exception("Unable to compute inverse");
            double[] b = new double[n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == perm[j])
                    {
                        b[j] = 1.0;
                    }
                    else
                    {
                        b[j] = 0.0;
                    }
                }                
                double[] x = HelperSolve(lum, b);
                for (int j = 0; j < n; ++j)
                {
                    result[j][i] = x[j];
                }
            }

            return result;
        }
        static double MatrixDeterminant(double[][] matrix)
        {
            double[][] lum = MatrixDecompose(matrix, out int[] perm, out int toggle);
            if (lum == null)
            {
                throw new Exception("Unable to compute MatrixDeterminant");
            }
            double result = toggle;
            for (int i = 0; i < lum.Length; ++i)
            {
                result *= lum[i][i];
            }
            return result;
        }
    }

}
