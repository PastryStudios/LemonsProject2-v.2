using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using BehaviourTree;

public class Task_Collect : BT_Node
{
    private const int collection_Amount = 1;
    private ResourceMap resourceMap;
    private int maxStorage;

    public Task_Collect(ResourceMap _resourceMap, int _maxStorage)
    {
        resourceMap = _resourceMap;
        maxStorage = _maxStorage;
    }

    public override NodeState Evaluate()
    {
        int currentAmount = (int)Root.GetData("current_resource_amount");
        int newAmount = currentAmount + collection_Amount;
        if (newAmount > maxStorage)
            newAmount = maxStorage;

        Root.SetData("current_resource_amount", newAmount);
       
        //Update Resource Map
        Vector3Int resourceCellPos = (Vector3Int)Root.GetData("target_cell");
        try
        {
            bool tileIsDead = resourceMap.ConsumeTile(resourceCellPos, collection_Amount);
            if (tileIsDead)
                _ClearTarget();
        }
        catch (KeyNotFoundException)
        {
            _ClearTarget();
        }
        _state = NodeState.RUNNING;
        return _state;
    }
    private void _ClearTarget()
    {
        Root.ClearData("target");
        Root.ClearData("target_cell");
        Root.ClearData("target_is_resource");
    }
}
