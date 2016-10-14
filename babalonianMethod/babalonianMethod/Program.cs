using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace babalonianMethod
{
    class Program
    {
        /// <summary>
        /// This function will compute a square root for the value fVal
        /// to a certain level of precision (fPrec). this method will use
        /// the babylonian algorithm to aclomplish this.
        /// </summary>
        /// <param name="fVal"></param>
        /// <param name="fPrec"></param>
        /// <returns></returns>
        static public float babylonian(float fVal, float fPrec)
        {
            float fX = 1.0f;

            while(true)
            {
                float fXPrime = (float)0.5 * (fX + fVal / fX);
                Console.WriteLine("fXPrime {0:F} and difference {1:F}", fXPrime, (fXPrime * fXPrime) - fVal);
                if((fXPrime * fXPrime) - fVal <= fPrec)
                {
                    return fXPrime;
                }
                else
                {
                    fX = fXPrime;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Using Babylonian square, root of 16 is: " + babylonian(16.0f, 0.001f));
        }
    }
}
