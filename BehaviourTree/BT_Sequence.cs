using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTree
{
    public class BT_Sequence : BT_Node
    {
        public BT_Sequence(): base() { }
        public BT_Sequence(List<BT_Node> nodeChildren) : base(nodeChildren) { }
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            foreach (BT_Node bT_Node in nodeChildren)
            {
                switch (bT_Node.Evaluate())
                {
                    case NodeState.FAILURE:
                        nodeState = NodeState.FAILURE;
                        return nodeState;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        nodeState = NodeState.SUCCESS;
                        return nodeState;
                }
            }

            nodeState = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return nodeState;

        }
    }

}
