using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    /// <summary>
    /// This class represents a single node in the graph.
    /// Similar to the idea of a node in a BST
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Vertex<T> where T: IComparable<T>
    {
        #region Attributes
        private T data; //the data the vertex stores
        private int index; //The index of the vertex in the vertex array
        #endregion

        #region Constructors
        public Vertex(int index, T data)
        {
            this.index = index;
            this.data = data;
        }

        #endregion

        #region Properties
        public T Data
        {
            get
            {
                return data;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }
        }
        #endregion

        public int CompareTo(Vertex<T> other)
        {
            return Index.CompareTo(other.index);
        }

        public override string ToString()
        {
            return "[" + data + "(" + index + ")]";
        }

    }
}
