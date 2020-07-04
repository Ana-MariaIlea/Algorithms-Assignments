using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
class PathFindingAgent : OffGraphWayPointAgent //NodeGraphAgent
{
    //Current target to move towards
    //private Node _target = null;
    //private List<Node> nodeQueue;
   // private Node currentNode;
   // private int numberOfNods = 1;
    private PathFinder _path;

    public PathFindingAgent(NodeGraph pNodeGraph, PathFinder path) : base(pNodeGraph)
    {
        _path = path;
        
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

    override protected void onNodeClickHandler(Node pNode)
    {
        if (_target == null)
        {
            nodeQueue = _path.Generate(currentNode, pNode);
            _target = nodeQueue[0];
        }
    }

    //protected override void Update()
    //{
    //    //no target? Don't walk
    //    if (_target == null) return;
    //    //Move towards the target node, if we reached it, clear the target
    //    if (moveTowardsNode(_target))
    //    {
    //        if (nodeQueue.Count() > numberOfNods)
    //        {
    //            _target = nodeQueue[numberOfNods];
    //            numberOfNods++;
    //        }
    //        else
    //        {
    //            currentNode = _target;
    //            _target = null;
    //            nodeQueue.Clear();
    //            numberOfNods = 1;
    //        }
    //    }
    //}
}
