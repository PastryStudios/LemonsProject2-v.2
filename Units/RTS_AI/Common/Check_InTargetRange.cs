using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using BehaviourTree;

public class Check_InTargetRange : BT_Node
{
    private const float reach_Threshold = 0.1f;

    private Transform transform;

    public Check_InTargetRange(Transform _transform)
    {
        transform = _transform;
    }
    public override NodeState Evaluate()
    {
        Vector3 target = (Vector3)Root.GetData("target");
        //Debug.Log(target);
        _state = Vector2.Distance(transform.position, target) < reach_Threshold
            ? NodeState.SUCCESS : NodeState.FAILURE;
        //Debug.Log("Target In Range: " + _state);
        return _state;
    }
}
