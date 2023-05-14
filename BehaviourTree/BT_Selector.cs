using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTree
{
    public class BT_Selector : BT_Node
    {
        public BT_Selector() : base() { }
        public BT_Selector(List<BT_Node> nodeChildren) : base(nodeChildren) { }
        //public override NodeState Evaluate()
        //{
        //    //bool anyChildIsRunning = false;
        //    //foreach (BT_Node bT_Node in nodeChildren)
        //    //{
        //    //    switch (bT_Node.Evaluate())
        //    //    {
        //    //        case NodeState.FAILURE:                        
        //    //            continue;
        //    //        case NodeState.SUCCESS:
        //    //            nodeState = NodeState.SUCCESS;
        //    //            return nodeState;
        //    //        case NodeState.RUNNING:
        //    //            nodeState = NodeState.RUNNING;
        //    //            return nodeState;
        //    //        default:
        //    //            continue;
        //    //    }
        //    //}

        //    //nodeState = NodeState.FAILURE;
        //    //return nodeState;

        //}
    }

}
