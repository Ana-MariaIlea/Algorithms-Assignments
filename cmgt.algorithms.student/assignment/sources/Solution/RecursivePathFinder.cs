using GXPEngine;
using System;
using System.Collections.Generic;

/**
 * An example of a PathFinder implementation which completes the process by rolling a die 
 * and just returning the straight-as-the-crow-flies path if you roll a 6 ;). 
 */
class RecursivePathFinder : PathFinder
{
    List<Node> visitedNodes;
    List<Node> shortsetPath;
    public RecursivePathFinder(NodeGraph pGraph) : base(pGraph)
    {
        visitedNodes = new List<Node>();
        shortsetPath = new List<Node>();
    }

    protected override List<Node> generate(Node pFrom, Node pTo)
    {
        shortsetPath.Clear();
        visitedNodes.Clear();
        pathRecursiveGenerator(pFrom, pTo);
        return shortsetPath;
    }

    void pathRecursiveGenerator(Node pFrom, Node pTo)
    {
        if (pFrom != pTo)
        {
            visitedNodes.Add(pFrom);
            for (int i = 0; i < pFrom.connections.Count; i++)
            {
                if(!visitedNodes.Contains(pFrom.connections[i]))
                {
                    pathRecursiveGenerator(pFrom.connections[i], pTo);
                    visitedNodes.RemoveAt(visitedNodes.Count - 1);
                }
            }
        }
        else
        { // Found a path
            visitedNodes.Add(pTo);
            if (shortsetPath.Count == 0)
            { // There isn't a path yet
                shortsetPath = new List<Node>(visitedNodes);
                //for (int i = 0; i < visitedNodes.Count; i++)
                //{
                //    shortsetPath.Add(visitedNodes[i]);
                //}
            }
            else
            { // There is a path
                if (visitedNodes.Count < shortsetPath.Count)
                { // If the new path is shorter
                    shortsetPath.Clear();
                    shortsetPath = new List<Node>(visitedNodes);
                    //for (int i = 0; i < visitedNodes.Count; i++)
                    //{
                    //    shortsetPath.Add(visitedNodes[i]);
                    //}
                }
            }

        }

    }

}