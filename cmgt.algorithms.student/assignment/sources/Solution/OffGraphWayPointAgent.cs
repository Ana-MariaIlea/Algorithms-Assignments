using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using GXPEngine;


class OffGraphWayPointAgent : NodeGraphAgent
{
    //Current target to move towards
    private Node _target = null;
    private List<Node> nodeQueue;
    private Node currentNode;
    private int numberOfNods=1;

    public OffGraphWayPointAgent(NodeGraph pNodeGraph) : base(pNodeGraph)
    {
        SetOrigin(width / 2, height / 2);
        nodeQueue = new List<Node>();
        //position ourselves on a random node
        if (pNodeGraph.nodes.Count > 0)
        {
            currentNode = pNodeGraph.nodes[Utils.Random(0, pNodeGraph.nodes.Count)];
            jumpToNode(currentNode);
        }
        //listen to nodeclicks
        pNodeGraph.OnNodeLeftClicked += onNodeClickHandler;
    }

    protected virtual void onNodeClickHandler(Node pNode)
    {
        Console.WriteLine(pNode.connections.Count());
        if (nodeQueue.Count() == 0)
        {
            if (currentNode.connections.Contains(pNode))
            {
                nodeQueue.Add(pNode);
                _target = pNode;
            }
        }
        else
        {
            if (nodeQueue[nodeQueue.Count()-1].connections.Contains(pNode))
            {
                nodeQueue.Add(pNode);
            }
        }
    }

    protected override void Update()
    {
        //no target? Don't walk
        if (_target == null) return;
       // Console.WriteLine(nodeQueue.Count());

        //Move towards the target node, if we reached it, clear the target
        if (moveTowardsNode(_target))
        {
            if (nodeQueue.Count() > numberOfNods)
            {
                _target = nodeQueue[numberOfNods];
                numberOfNods++;
            }
            else
            {
                currentNode = _target;
                _target = null;
                nodeQueue.Clear();
                numberOfNods = 1;
            }
        }
    }
}

