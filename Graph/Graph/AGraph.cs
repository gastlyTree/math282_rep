using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public abstract class AGraph<T> : IGraph<T> where T : IComparable<T>
    {
        #region Attributes
        //stores the vertices of the graph
        protected List<Vertex<T>> vertices;

        //A dictionary is a hashtable. we will use it to store a data items index
        //into the vertices list.. this will make it much more efficient to lookup
        // a vertex in the vertices list.
        protected Dictionary<T, int> revLookUp;

        //store the number of edges in the graph
        protected int numEdges;

        //is the graph directed or not
        protected bool isDirected;

        //Is the graph weighted
        protected bool isWeighted;

        #endregion

        #region Properties
        public int NumEdges
        {
            get
            {
                return numEdges;
            }
        }

        public int NumVertices
        {
            get
            {
                return vertices.Count();
            }
        }
        #endregion

        #region Abstract Methods

        //a helper method that will allow us to implement the other two add edge methods
        protected abstract void AddEdge(Edge<T> e);

        public abstract IEnumerable<Vertex<T>> EnumerateNeighbors(T data);

        public abstract Edge<T> GetEdge(T from, T to);

        public abstract bool HasEdge(T from, T to);

        public abstract void RemoveEdge(T from, T to);

        //When adding a vertex here, we need to tell the child class to make room
        //for the edges of this vertex.
        public abstract void AddVertexAdjustEdges(Vertex<T> v);

        protected abstract Edge<T>[] getAllEdges();

        #endregion

        public void AddEdge(T from, T to)
        {
            throw new NotImplementedException();
        }

        public void AddEdge(T from, T to, double weight)
        {
            throw new NotImplementedException();
        }

        public void AddVertex(T data)
        {
            throw new NotImplementedException();
        }

        public void BreadthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
        {
            throw new NotImplementedException();
        }

        public void DepthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
        {
            throw new NotImplementedException();
        }  

        public IEnumerable<Vertex<T>> EnumerateVertices()
        {
            throw new NotImplementedException();
        }

        public Vertex<T> GetVertex(T data)
        {
            throw new NotImplementedException();
        }

        public bool HasVertex(T data)
        {
            throw new NotImplementedException();
        }

        public IGraph<T> MinimumSpanningTree()
        {
            throw new NotImplementedException();
        }

        public void RemoveVertex(T data)
        {
            throw new NotImplementedException();
        }

        public IGraph<T> ShortestWeightedPath(T start, T end)
        {
            throw new NotImplementedException();
        }
    }
}
