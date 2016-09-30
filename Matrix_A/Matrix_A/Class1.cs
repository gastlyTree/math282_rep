using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Matrix_A
{
    class Class1
    {
        static void TestCreatMatrix()
        {
            double[,] dArray = { {12,3,52 }, {-10,45,0.98 } };
            Matrix m = new Matrix(dArray);
            Console.WriteLine(m.ToString());
        }

        static void TestAdd()
        {
            double[,] d1 = { { 1, 3 }, { -1, 2 } };
            double[,] d2 = { { -2, 2 }, { 7, 8 } };
            Matrix m1 = new Matrix(d1);
            Matrix m2 = new Matrix(d2);
            Matrix sum = null;
            sum = m1 + m2;
            Console.WriteLine(sum);

        }

        static void TestLeastSquares()
        {
            double[,] d1 = { { -2,4 }, { -1,2 }, { 0,1 }, { 1,2}, {2,4 }, {3,4.5 }, {4,2.5 } };

            Matrix m = new Matrix(d1);
            try
            {
                Matrix mSolution = (Matrix)m.LeastSquares(3);
                Console.WriteLine(mSolution);
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void TestScalarMultiply()
        {
            double[,] dArray = { { 12, 3, 52 }, { -10, 45, 0.98 } };
            Matrix m = new Matrix(dArray);
            Matrix product = m * 3.0;
            Console.WriteLine(product.ToString());
        }

        static void TestMultiply()
        {
            double[,] d1 = { { 1, 0, -1 }, { 2, -1, 2 } };
            double[,] d2 = { { 1, 0 }, { -1, 1 }, { 2, 2 } };
            Matrix m1 = new Matrix(d1);
            Matrix m2 = new Matrix(d2);
            Matrix product = null;
            product = m1 * m2;
            Console.WriteLine(product);
        }

        static void TestGaussJordan()
        {
            double[,] d1 = { { 7, 7, 35, 91, 20 }, { 7, 35, 91, 371, 23.5 }, { 35, 91, 371, 1267, 116.5 }, { 91, 371, 1267, 4955, 281.5 } };
            //double[,] d1 = { { 1, 1, 2, 8 }, { -1, -2, 3, 1 }, {3, -7, 4, 10 } };
            Matrix m1 = new Matrix(d1);
            try
            {
                Console.WriteLine(m1.GaussJordanElimination());
            }catch(ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void TestGaussJordanIllConditioned()
        {
            //change 2.0 in the second row to 2.001
            double[,] d1 = { { 1, 1, 2 }, { 1, 1.001, 2.001} };

            Matrix m1 = new Matrix(d1);
            try
            {
                Console.WriteLine(m1.GaussJordanElimination());
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            TestLeastSquares();
        }
    }
}
