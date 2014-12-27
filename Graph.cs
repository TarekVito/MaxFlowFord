using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Task2
{
    class Graph
    {
        // Class Attributes
        public int numOfVertices;
        public int numOfEdges;
        public List<Vertex> graph;

        // Class Constructor
        public Graph()
        {
            numOfVertices = 0;
            numOfEdges = 0;
            graph = new List<Vertex>();
        }
        public Graph(int vertices, int edges)
        {
            numOfVertices = vertices;
            numOfEdges = edges;
            graph = new List<Vertex>(vertices);
            for (int i = 0; i < vertices; ++i)
                graph.Add(new Vertex(i));
        }

        public class Vertex
        {
            public int vertexID;
            public List<Tuple<int, double>> neighbors;

            public Vertex(int ID)
            {
                vertexID = ID;
                neighbors = new List<Tuple<int, double>>();
            }
        }
    }
}
