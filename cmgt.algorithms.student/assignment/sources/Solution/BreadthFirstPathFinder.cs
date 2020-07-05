using GXPEngine;
using System;
using System.Collections.Generic;

/**
 * An example of a PathFinder implementation which completes the process by rolling a die 
 * and just returning the straight-as-the-crow-flies path if you roll a 6 ;). 
 */
class BreadthFirstPathFinder : PathFinder
{
    List<Node> nodesToCheck;
    List<Node> nodesAreChecked;
    List<Node> finalPath;
    Dictionary<Node, Node> childParent;
    Node currentNode;
    public BreadthFirstPathFinder(NodeGraph pGraph) : base(pGraph)
    {
        nodesToCheck = new List<Node>();
        nodesAreChecked = new List<Node>();
        finalPath = new List<Node>();
        childParent = new Dictionary<Node, Node>();
    }
    protected override List<Node> generate(Node pFrom, Node pTo)
    {
        nodesToCheck.Clear();
        nodesAreChecked.Clear();
        childParent.Clear();
        finalPath.Clear();
        nodesToCheck.Add(pFrom);
        return BFSPathFinding(pFrom, pTo);
    }

    List<Node> BFSPathFinding(Node startNode, Node endNode)
    {
        while (nodesToCheck.Count != 0)
        {
            currentNode = nodesToCheck[0];
            nodesToCheck.Remove(currentNode);
            nodesAreChecked.Add(currentNode);
            if (currentNode != endNode)
            {
                foreach (Node connection in currentNode.connections)
                {
                    if (!nodesToCheck.Contains(connection) && !nodesAreChecked.Contains(connection))
                    {
                        nodesToCheck.Add(connection);
                        childParent.Add(connection, currentNode);
                    }
                }
            }
            else
            {
                finalPath.Add(currentNode);
                while (currentNode != startNode)
                {

                    finalPath.Add(childParent[currentNode]);
                    currentNode = childParent[currentNode];
                }
                finalPath.Reverse();
                return finalPath;
            }
        }

        return null;
    }

}
