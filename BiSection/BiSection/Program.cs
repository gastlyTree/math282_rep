using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiSection
{
    class Program
    {
        public delegate float funcDel(float fVal);

        public static float mySquare(float fVal)
        {
            return fVal * fVal;
        }

        public static float myCube(float fVal)
        {
            return fVal * fVal * fVal;
        }


        /// <summary>
        /// This function implements the bisection algorithm for finding
        /// roots of real numbers.
        /// </summary>
        /// <param name="fVal"></param>
        /// <param name="fPrecision"></param>
        /// <returns></returns>
        static float biSection(float fVal, float fPrecision, funcDel funcPtr)
        {
            float fUpper = Math.Max(1, fVal);
            float fLower = Math.Min(1, fVal);

            while(true)
            {
                float fGuess = (fUpper + fLower) / 2;
                float fTest = funcPtr(fGuess);

                Console.WriteLine("fUpper is: " + fUpper + ", fLower is: " + fLower + ", fGuess is: " + fGuess);

                if(fVal == fTest || (fUpper - fLower) < fPrecision)
                {
                    return fGuess;
                }
                if(fTest > fVal)
                {
                    fUpper = fGuess;
                }
                else
                {
                    fLower = fGuess;
                }

            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Square root of 4 is " + biSection(4.0f, 0.0001f, mySquare));
            Console.WriteLine("Cube root of 512 is " + biSection(512.0f, 0.0001f, myCube));
        }
    }
}
