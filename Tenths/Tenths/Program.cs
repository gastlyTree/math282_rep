using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenths
{
    class Program
    {
        static void Main(string[] args)
        {

            float fTenth = 1 / 10F;
            float fOne = 1.0F;
            float fResult = 0;

            for (int i = 0; i < 10; i++)
            {
                fResult += fTenth;
            }

            if (fTenth == fOne)
            {
                Console.WriteLine("Values Match");
            }
            else
            {
                Console.WriteLine("Values do not Match");
            }

            Console.WriteLine("Value of fResult is: " + fResult.ToString("E20"));
            Console.WriteLine("vALUE OF 1/10 is: " + fTenth.ToString("E20"));
        }
    }
}
