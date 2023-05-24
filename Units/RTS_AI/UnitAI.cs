using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using BehaviourTree;

public class UnitAI : UnitManager
{
    // Controls the unit
    //private void
    //Debug.Log("Unit was selected");

    protected override BT_Node SetupTree()
    {

        //Need pathfinding information:
        // Look at NavMesh

        BT_Node root = new Selector();

        root.SetChildren(new List<BT_Node>()
            {new Sequence(new List<BT_Node>(){ 
                   new Check_HasTarget(),
                //    new Task_Walk(transform, pathfinder, moveSpeed),
                    })
                //new Task_FindClosestTarget(transform, resourceTilemap, true)
              
            }
        ); //forceRoot: true);
        throw new System.NotImplementedException();
    }

}
