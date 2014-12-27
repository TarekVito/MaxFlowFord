using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Task2
{   
    class MaxFlow
    {
        Graph inputGraph;
        List<bool> visited;
        List<Tuple<int,double> > parent;
        int sourceID, sinkID;
       
        
        public MaxFlow(Graph _graph,int _sourceID,int _sinkID)
        {
            sourceID = _sourceID;
            sinkID = _sinkID;
            inputGraph = _graph;
        }

        bool BFS()
        {
            visited = new List<bool>();
            parent = new List<Tuple<int, double>>();
            for (int i = 0; i < inputGraph.numOfVertices; ++i)
            {
                visited.Add(false);
                parent.Add(new Tuple<int, double>(-1, -1));
            }
            Queue<int> Q = new Queue<int>();
            Q.Enqueue(sourceID);
            while (Q.Count != 0)
            {
                int curVertex = Q.Dequeue();
                visited[curVertex] = true;

                if (curVertex == sinkID)
                    return true;
                for (int i = 0; i < inputGraph.graph[curVertex].neighbors.Count; ++i)
                {
                    Tuple<int, double> childID = inputGraph.graph[curVertex].neighbors[i];
                    if (!visited[childID.Item1] && Math.Abs(childID.Item2-0)>0.00001)
                    {
                        parent[childID.Item1] = new Tuple<int,double>(curVertex,childID.Item2);
                        Q.Enqueue(childID.Item1);
                    }
                }
            }
            return false;
        }

        List<Tuple<int, double>> getPath()
        {
            List<Tuple<int, double>> path = new List<Tuple<int, double>>();
            int curVertex = sinkID;
            while (parent[curVertex].Item1 != -1)
            {
                path.Add(parent[curVertex]);
                curVertex = parent[curVertex].Item1;
            }
            path.Reverse();
            return path;
        }

        double minEdgeCost(List<Tuple<int, double>> path)
        {
            int minIdx = 0;
            for(int i=0;i<path.Count;++i)
                if (path[i].Item2 < path[minIdx].Item2)
                    minIdx = i;

            return path[minIdx].Item2;
        }

        void addToEdge(int id1, int id2, double cost)
        {
            List<Tuple<int, double>> vertexN = inputGraph.graph[id1].neighbors;
            for (int j = 0; j < vertexN.Count; ++j)
                if (vertexN[j].Item1 == id2)
                {
                    vertexN[j] = new Tuple<int, double>(id2, vertexN[j].Item2+cost);
                    return;
                }
        }

        public double computeMaxFlow()
        {
            while (BFS())
            {
                List<Tuple<int, double>> curAugPath = getPath();
                double minPathCost = minEdgeCost(curAugPath);
                for (int i = 0; i < curAugPath.Count; ++i)
                {
                    int nextIdx = i+1 < curAugPath.Count? curAugPath[i + 1].Item1:sinkID;
                    
                    addToEdge(curAugPath[i].Item1,nextIdx, -1 * minPathCost);
                    addToEdge(nextIdx, curAugPath[i].Item1, minPathCost);
                }

            }
            double maxFlow = 0;
            for (int i = 0; i < inputGraph.graph[sinkID].neighbors.Count; ++i)
                maxFlow += inputGraph.graph[sinkID].neighbors[i].Item2;
            
            return maxFlow;
        }




    }
  
}
