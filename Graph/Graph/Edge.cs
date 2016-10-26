using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Edge<T> where T: IComparable<T>
    {
        #region Attributes
        private Vertex<T> from;
        private Vertex<T> to;
        private double weight;
        private bool isWeighted;
        #endregion

        #region Properties
        public Vertex<T> From
        {
            get
            {
                return from;
            }
        }

        public Vertex<T> To
        {
            get
            {
                return to;
            }
        }

        public double Weight
        {
            get
            {
                return weight;
            }
        }

        public bool IsWeighted
        {
            get
            {
                return isWeighted;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Non-weighted edge, weight gets set to positive infinity (invalid weight)
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public Edge(Vertex<T> from, Vertex<T> to)
            :this(from, to, double.PositiveInfinity, false)
        {
        }
        public Edge(Vertex<T> from, Vertex<T> to, double weight)
            :this(from, to, weight, true)
        {
        }
        private Edge(Vertex<T> from, Vertex<T> to, double weight, bool isWeighted)
        {
            this.from = from;
            this.to = to;
            this.weight = weight;
            this.isWeighted = isWeighted;
        }
        #endregion

        public int CompareTo(Edge<T> other)
        {
            int result = Weight.CompareTo(other.Weight);
            //What if the edges have the same weight
            if (result == 0)
            {
                result = From.CompareTo(other.From);
                if(result == 0)
                {
                    result = To.CompareTo(other.To);
                }
            }
            return result;
        }

        public override string ToString()
        {
            return from + " To " + to + (isWeighted ? ", W = " + weight : "");
        }

    }
}
