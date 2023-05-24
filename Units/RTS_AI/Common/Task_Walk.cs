using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using BehaviorTree;
using AStar;
public class Task_Walk : BT_Node
{
    private const float distanceThreshold = 0.1f;

    private Transform unitTransform;
    private Pathfinder2D unitPathfinder;
    private float unitSpeed;
    private System.Action<Vector2> reachedTile;

    private List<Vector3> pathToTarget;

    public Task_Walk(Transform transform, Pathfinder2D pathfinder, float speed, System.Action<Vector2> onReachTile) 
    {
        unitTransform = transform;
        unitPathfinder = pathfinder;
        unitSpeed = speed;
        reachedTile = onReachTile;
    }

    public override NodeState Evaluate()
    {
        _state = NodeState.RUNNING;
        //If no path calculated, calculate new path
        if(pathToTarget == null)
        {
            Vector3 targetWorldPos = (Vector3)GetData("target");
            Debug.Log("Target Position: " + targetWorldPos);
            if(Vector3.Distance(unitTransform.position, targetWorldPos) < distanceThreshold)
            {
                _state = NodeState.SUCCESS;
            }
            else
            {
                pathToTarget = unitPathfinder.FindPath(unitTransform.position, targetWorldPos);
                Debug.Log("Target Path: " + pathToTarget);
                if (pathToTarget == null)
                    _state = NodeState.FAILURE;
                //else
                //{
                    //Debug.Log(pathToTarget[0]);
                    //reachedTile(GetNextDir());
                //}

            }
            //return _state;
        }
        //If a path has already been calculated
        else if(pathToTarget.Count > 0)
        {
            Debug.Log(pathToTarget);
            Vector3 target = pathToTarget[0];
            Debug.Log(target);
            if (Vector3.Distance(unitTransform.position, target) < distanceThreshold)
            {
                unitTransform.position = target;
                pathToTarget.RemoveAt(0);
                //Special case, reached the end
                if(pathToTarget.Count == 0)
                {
                    _state = NodeState.SUCCESS;
                    pathToTarget = null;
                }
                //Else move to tile
                //else
                //{
                    //reachedTile(GetNextDir());
                //}
            }
            else
            {
                unitTransform.position = Vector3.MoveTowards(unitTransform.position, target, unitSpeed * Time.deltaTime);
                Debug.Log(unitTransform.position);
            }
        }
        return _state;
    }
    private Vector2 GetNextDir()
    {
        //Debug.Log(pathToTarget[0]);
        Vector3 nextPoint = pathToTarget[0];
        return (nextPoint - unitTransform.position).normalized;
    }
}
