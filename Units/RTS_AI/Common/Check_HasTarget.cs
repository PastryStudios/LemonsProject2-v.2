using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Check_HasTarget : BT_Node
{
    public override NodeState Evaluate()
    {
        _state = Root.GetData("target") == null ? NodeState.FAILURE : NodeState.SUCCESS;
        Debug.Log("Check_HasTarget " + _state);
        return _state;
    }
}
