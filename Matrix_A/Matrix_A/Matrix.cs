using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_A
{
    public class Matrix : AMatrix
    {
        #region Attributes
        //Set up a 2D array
        private double[,] dArray;
        #endregion

        public Matrix (double [,] dArray)
        {
            //should do a copy
            this.dArray = dArray;
            //set the row and col property
            //note how we obtain dimentions of arrays in C#
            this.Rows = dArray.GetLength(0);
            this.Cols = dArray.GetLength(1);
        }

        public override double GetElement(int iRow, int iCol)
        {
            //offset into the array, as matrices start at 1,1 not 0,0
            return dArray[iRow - 1, iCol - 1];
        }

        public override void SetElement(int iRow, int iCol, double dValue)
        {
            dArray[iRow - 1, iCol - 1] = dValue;
        }

        internal override AMatrix NewMatrix(int iRows, int iCols)
        {
            return new Matrix(new double[iRows, iCols]);
        }

        //Note that not all languages support operator overloading (Java does not)
        //some languages restrict the operators that can be overloaded.
        //Some languages require that certain operators be ovcerloaded in pairs (ex: + & -)
        public static Matrix operator + (Matrix LeftOp, Matrix RightOP)
        {
            return (Matrix) LeftOp.Add(RightOP);
        }

        public static Matrix operator - (Matrix LeftOp, Matrix RightOP)
        {
            return (Matrix)LeftOp.Subtract(RightOP);
        }

        public static Matrix operator * (Matrix LeftOp, Matrix RightOP)
        {
            return (Matrix)LeftOp.Multiply(RightOP);
        }

        public static Matrix operator * (double dScalar, Matrix RightOp)
        {
            return (Matrix)RightOp.ScalarMultiplication(dScalar);
        }

        public static Matrix operator *(Matrix LeftOp, double dScalar)
        {
            return (Matrix)LeftOp.ScalarMultiplication(dScalar);
        }
    }
}
