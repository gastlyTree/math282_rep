using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_A
{
    public abstract class AMatrix : IMatrix, ICloneable
    {
        #region Attributes

        private int iRows;
        private int iCols;

        public int Rows
        {
            get
            {
                return iRows;
            }

            set
            {
                if(value < 1)
                {
                    throw new ApplicationException("Rows must be 1 or greater");
                }
                iRows = value;
            }
        }

        public int Cols
        {
            get
            {
                return iCols;
            }

            set
            {
                if (value < 1)
                {
                    throw new ApplicationException("Columns must be 1 or greater");
                }
                iCols = value;
            }
        }

        #endregion


        #region Abstract Methods
        public abstract double GetElement(int iRow, int iCol);

        public abstract void SetElement(int iRow, int iCol, double dValue);

        internal abstract AMatrix NewMatrix(int iRows, int iCols);

        #endregion

        #region LeastSquares

        /// <summary>
        /// Calculates the least squares trendline in the calling matrix.
        /// The calling matrix contains the original set of n data points.
        /// The calling matrix would have rows > m and exactly 2 columns
        /// </summary>
        /// <param name="m">Degree or order requested by the user</param>
        /// <returns>Sloved augmented matrix</returns>
        public AMatrix LeastSquares(int m)
        {
            //create the augmented matrix using C# Reflection instead of NewMatrix
            //create all required parameters
            double[,] dArray = new double[m + 1, m + 2];
            //Wrap all parameters into an object array
            object[] parameters = { dArray };
            //C# Magic
            AMatrix mAugmented = (AMatrix)Activator.CreateInstance(this.GetType(), parameters);

            //create temp storage for the x sums
            AMatrix xSums = NewMatrix(1, 2 * m + 1);

            //check if calling matrix is the correct size
            if(this.Rows > m && this.Cols == 2)
            {
                //Calculate the xSums
                for (int power = 0; power < 2*m+1; power++)
                {
                    //create a temp value
                    double dSum = 0;
                    //for each data point
                    for (int iDataPoint = 1; iDataPoint <= this.Rows; iDataPoint++)
                    {
                        dSum += Math.Pow(this.GetElement(iDataPoint, 1), power);
                    }
                    //store sums in xSums
                    xSums.SetElement(1, power + 1, dSum);
                }

                //calculate teh y-sums
                for (int power = 0; power < m + 1; power++)
                {
                    double dSum = 0;
                    for (int iDataPoint = 1; iDataPoint <= this.Rows; iDataPoint++)
                    {
                        dSum += this.GetElement(iDataPoint, 2) *
                            Math.Pow(this.GetElement(iDataPoint, 1), power);
                    }
                    mAugmented.SetElement(power + 1, mAugmented.Cols, dSum);
                }

                //Put all x-sums into the augmented matrix
                for (int r = 1; r <= m + 1; r++)
                {
                    for (int c = 1; c <= m + 1; c++)
                    {
                        mAugmented.SetElement(r, c, xSums.GetElement(1, r + c - 1 ));
                    }
                }

            }
            else
            {
                throw new ApplicationException("Calling Matrix Incorrect Size");
            }

            Console.WriteLine(mAugmented.ToString());

            //return the solved augmented matrix
            return mAugmented.GaussJordanElimination();
        }

        #endregion


        public AMatrix GaussJordanElimination()
        {
            //A referance to a copy of the calling matrix
            AMatrix mSolution = null;
            //Row multiplying factor
            double dFactor = 0;
            //the current pivot element
            double dPivot = 0;

            //If this is not an augumented matrix, trow an exception
            if(this.Cols != this.Rows + 1)
            {
                throw new ApplicationException("Incorrect dimentions for an augumented matrix");
            }

            //create aa copy of the calling matrix
            mSolution = (AMatrix)this.Clone();

            /*
            For each pivot in the solution matrix (i)
                store a copy of the current pivot element
                for each element in the current pivot row, divide by the pivot value (j)
                for each row in the solution (k)
                    if the currrent row is not the pivot row
                        get the multiplying factor
                        for each element in the current row (j)
                            Current <-- Current element + multipying factor * coresponding value in the pivot row
                            current location in the current row <-- Current
             return the solution matrix
             */

            for (int i = 1; i <= mSolution.Rows; i++)
            {
                mSolution.SystemSolveable(i);
                dPivot = mSolution.GetElement(i, i);

                for (int j = i; j <= mSolution.Cols; j++)
                {
                    mSolution.SetElement(i, j, (mSolution.GetElement(i, j) / dPivot));
                }

                for (int k = 1; k <= mSolution.Rows; k++)
                {
                    if (k != i)
                    {
                        dFactor = mSolution.GetElement(k, i) * (-1);

                        for (int j = i; j <= mSolution.Cols; j++)
                        {
                            mSolution.SetElement(k, j, (mSolution.GetElement(k, j) + dFactor * mSolution.GetElement(i, j)));
                        }

                    }
                }
            }
            return mSolution;
        }

        /// <summary>
        /// Checks the solution matrix to see if the system is solvable.
        /// If, so the method will ensure that the pivot is no-zero, by swappin the rows if necessary.
        /// If not, the system is not solveable, throw's an application exception
        /// </summary>
        /// <param name="i">Current pivot row/colomn</param>
        private void SystemSolveable(int i)
        {
            /*
             CurrentPivot <-- get the current pivot
             NextRow <-- current row to check for non-zero pivot 
              
             While the CurrentPivot equals 0 and there are still rows to check
                CurrentPivot <-- possible pivot from this row
                if CurrentPivot is not 0
                    swap the pivot row (i) with the current row (NextRow)
                else
                    Check the next row
             if a non-zero pivot wasn't found
                indicate an error (throw exception)

             */
            double dCurrentPivot = this.GetElement(i, i);
            int iNextRow = i + 1;

            while(dCurrentPivot == 0 && iNextRow <= this.Rows)
            {
                dCurrentPivot = this.GetElement(iNextRow, i);
                if(dCurrentPivot != 0)
                {
                    for (int j = 1; j <= this.Cols; j++)
                    {
                        double dTemp = this.GetElement(i, j);
                        this.SetElement(i, j, this.GetElement(iNextRow, j));
                        this.SetElement(iNextRow, j, dTemp);
                    }
                }
                else
                {
                    iNextRow++;
                }
            }
            if(dCurrentPivot == 0)
            {
                throw new ApplicationException("No unique solution found");
            }



        }

        public IMatrix Add(IMatrix mRight)
        {
            AMatrix LeftOp = this;
            AMatrix RightOp = (AMatrix)mRight;
            AMatrix Sum = null;

            //if operands are the same size
            if(LeftOp.Cols == RightOp.Cols && LeftOp.Rows == RightOp.Rows)
            {
                Sum = NewMatrix(LeftOp.Rows, LeftOp.Cols);
                for (int r = 1; r <= LeftOp.Rows; r++)
                {
                    for (int c = 1; c <= LeftOp.Cols; c++)
                    {
                        //Calculate the current sum for this position
                        double dVal = LeftOp.GetElement(r, c) + RightOp.GetElement(r, c);
                        Sum.SetElement(r, c, dVal);
                    }
                }
            }
            else
            {
                throw new ApplicationException("Operands must be the same size");
            }
            return Sum;

        }

        public IMatrix Subtract(IMatrix mRight)
        {
            return this.Add(mRight.ScalarMultiplication(-1));
        }

        public IMatrix Multiply(IMatrix mRight)
        {
            AMatrix LeftOp = this;
            AMatrix RightOp = (AMatrix)mRight;
            AMatrix Product = null;
            double dSum = 0;

            //if we can multiply the operands
                //create the product matrix
                //for each row in the product matrix
                    //for each column in the product matrix
                        //Initialize dSum to 0 
                        //for each element in the current row of leftop or current column of right op
                            //add to dsum the current product of coresponding elements  
                        //set the summ to the current location in the product matrix

            //else
                //idicate an error
            //return the product matrix
            if(LeftOp.Cols == RightOp.Rows)
            {
                Product = NewMatrix(LeftOp.Rows, RightOp.Cols);

                for (int r = 1; r <= Product.Rows; r++)
                {
                    for (int c = 1; c <= Product.Cols; c++)
                    {
                        dSum = 0;

                        for(int e = 1; e <= LeftOp.Cols; e++)
                        {
                            dSum += LeftOp.GetElement(r, e) * RightOp.GetElement(e, c);
                        }
                        Product.SetElement(r, c, dSum);

                    }
                }
            }
            else
            {
                throw new ApplicationException("Operands cannot be multiplied");
            }
            return Product;
        }

        public IMatrix ScalarMultiplication(double dScalar)
        {
            AMatrix Product = NewMatrix(this.Rows, this.Cols);
            for (int r = 1; r <= this.Rows; r++)
            {
                for (int c = 1; c <= this.Cols; c++)
                {
                    Product.SetElement(r, c, this.GetElement(r, c) * dScalar);
                }
            }
            return Product;
        }

        public object Clone()
        {
            return ScalarMultiplication(1);
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            char cUL = (char)0x250C;
            char cUR = (char)0x2510;
            char cLL = (char)0x2514;
            char cLR = (char)0x2518;
            char cVLine = (char)0x2502;

            //build the top row
            s.Append(cUL);
            for (int j = 1; j <= this.Cols; j++)
            {
                s.Append("\t\t");
            }
            s.Append(cUR + "\n");

            //build the data rows
            for (int i = 1; i <= this.Rows; i++)
            {
                s.Append(cVLine);
                for (int j = 1; j <= this.Cols; j++)
                {
                    if (this.GetElement(i, j) >= 0)
                    {
                        s.Append(" ");
                    }
                    s.Append(String.Format("{0:0.000 e+00}", this.GetElement(i, j)) + "\t");

                }
                s.Append(cVLine + "\n");
            }
            //Build the bottom row
            s.Append(cLL);
            for (int j = 1; j <= this.Cols; j++)
            {
                s.Append("\t\t");
            }
            s.Append(cLR + "\n");
            return s.ToString();
        }
    }
}
