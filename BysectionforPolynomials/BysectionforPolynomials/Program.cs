using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysectionforPolynomials
{
    public delegate double funcDecl(double dVal);
    

    class Program
    {
        /// <summary>
        /// This will do the polynomial 3x^3-2x+2
        /// </summary>
        /// <param name="dVal"></param>
        /// <returns></returns>
        public static double equation1(double dVal)
        {
            return (double)(3 * Math.Pow(dVal, 3.0) - 2 * dVal + 2);
        }

        public static double equation2(double dVal)
        {
            return (double)(3 * Math.Pow(dVal, 4.0) - 2 * Math.Pow(dVal, 2.0) - 7 * dVal + 3);
        }

        public static double biSection(double dVal1, double dVal2, double dPrec, funcDecl myFunc)
        {
            double fLower = Math.Min(dVal1, dVal2);
            double fUpper = Math.Max(dVal1, dVal2);
            int nIteration = 0;

            while (true)
            {
                double dMidPoint = (fLower + fUpper) / 2;
                double dFofX = myFunc(dMidPoint);

                Console.WriteLine("Lower{0:F6}\tUpper {1:F6}\tMidPoint {2:F6}\tF(x) {3:F6}",fLower,fUpper,dMidPoint,dFofX);

                if(dFofX == 0 || Math.Abs(dFofX) < dPrec || nIteration++ > 50)
                {
                    return dMidPoint;
                }

                if(dFofX < 0)
                {
                    fLower = dMidPoint;
                }
                else
                {
                    fUpper = dMidPoint;

                }
            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("The answer to the equation1 is " + biSection(-1.0, -2, 0.0001, equation1));

            Console.WriteLine("The answer to the equation2 is " + biSection(0.0, 1.0, 0.0001, equation2));
        }
    }
}
