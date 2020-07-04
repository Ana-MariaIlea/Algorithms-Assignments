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
        pathRecursiveGenerator(pFrom, pTo, 0);
        return shortsetPath;
    }

    void pathRecursiveGenerator(Node pFrom, Node pTo, int deapth)
    {
        Console.WriteLine(shortsetPath.Count);

        if (pFrom != pTo)
        {
           // Console.WriteLine("continue");
            visitedNodes.Add(pFrom);
            for (int i = 0; i < pFrom.connections.Count; i++)
            {
                //bool visited = false;
                //for (int j = 0; j < visitedNodes.Count; j++)
                //{
                //    if (pFrom.connections[i] == visitedNodes[j]) 
                //    {
                //       // Console.WriteLine("visited");
                //        visited = true; 
                //        break; 
                //    }
                //}
                //Console.WriteLine(visited);

                //if (visited == false)
                if(!visitedNodes.Contains(pFrom.connections[i]))
                {
                    pathRecursiveGenerator(pFrom.connections[i], pTo, deapth++);
                    visitedNodes.Remove(pFrom.connections[i]);
                }
            }
        }
        else
        {
            visitedNodes.Add(pTo);
            if (shortsetPath.Count == 0)
            {
                //Console.WriteLine("make path");
                for (int i = 0; i < visitedNodes.Count; i++)
                {
                    shortsetPath.Add(visitedNodes[i]);
                }
            }
            else
            {
                if (visitedNodes.Count < shortsetPath.Count)
                {
                    shortsetPath.Clear();
                    for (int i = 0; i < visitedNodes.Count; i++)
                    {
                        shortsetPath.Add(visitedNodes[i]);
                    }
                }
            }

        }

    }

}