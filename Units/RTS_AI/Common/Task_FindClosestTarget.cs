using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BehaviourTree;
public class Task_FindClosestTarget : BT_Node
{
    private Transform transformTarget;
    private Tilemap searchTilemap;
    private bool searchingForResource;

    private int maxGridSize;
    private Vector3 gridTileAnchor;

    public Task_FindClosestTarget(Transform transform, Tilemap searchToTilemap, bool searchForResource)
    {
        transformTarget = transform;
        searchTilemap = searchToTilemap;
        searchingForResource = searchForResource;

        BoundsInt tileMapBounds = searchTilemap.cellBounds;
        maxGridSize = Mathf.Max(tileMapBounds.size.x, tileMapBounds.size.y);
        gridTileAnchor = searchTilemap.tileAnchor;
    }

    public override NodeState Evaluate()
    {
        base.Evaluate();
        Vector3Int position = Vector3Int.FloorToInt(transformTarget.position);
        for(int searchRadius = 0; searchRadius < maxGridSize; searchRadius++)
        {
            for(int x = position.x - searchRadius; x <= position.x + searchRadius; x++)
            {
                for(int y = position.y - searchRadius; y <= position.y; y++)
                {
                    Vector3 worldPos = new Vector3(x, y, 0) + gridTileAnchor;
                    Vector3Int cellPos = searchTilemap.WorldToCell(worldPos);
                    if (searchTilemap.HasTile(cellPos))
                    {
                        Root.SetData("target", worldPos);
                        Root.SetData("target_cell", cellPos);
                        Root.SetData("target_is-resource", searchingForResource);
                        _state = NodeState.SUCCESS;
                        return _state;
                    }
                }
            }
        }
        _state = NodeState.FAILURE;
        Debug.Log("Closest Target: " + _state);
        return _state;
    }
}
