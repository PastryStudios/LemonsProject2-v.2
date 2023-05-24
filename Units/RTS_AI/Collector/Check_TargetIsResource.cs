using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using BehaviourTree;

public class Check_TargetIsResource : BT_Node
{
    public override NodeState Evaluate()
    {
        bool targetIsResource = (bool)Root.GetData("target_is_resource");
        _state = targetIsResource ? NodeState.SUCCESS : NodeState.FAILURE;
        Debug.Log("Target is Resource: " + _state);
        return _state;
    }
}
