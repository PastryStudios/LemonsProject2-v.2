using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BehaviourTree;
using BehaviorTree;

public class CollectorAI : BT_Tree
{
    public enum Resource {
        Wood,
        Stone,
    }
    private static float collection_Rate = 1;
    [SerializeField] private Resource resource;
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private int maxStorage = 10;
    [SerializeField] private Transform resourceFillBar;

    [SerializeField] private Tilemap groundTileMap;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _horizontalSprite;
    [SerializeField] private Sprite _verticalSprite;

    protected override void Start()
    {
       // base.Start();
        //UpdateResourceFillBar();
    }

    protected override BT_Node SetupTree()
    {
        if(groundTileMap == null)
        {
            Debug.LogWarning($"No ground tilemap for '{name}'");
            return null;
        }
        AStar.Pathfinder2D pathfinder = new(groundTileMap);

        Tilemap resourceTilemap = null, storageTilemap = null;
        ResourceMap resourceMap = null;
        Tilemap[] tileMapArray = FindObjectsOfType<Tilemap>();
        foreach(Tilemap tilemap in tileMapArray)
        {
            //Debug.Log(tilemap.name);
            if(tilemap.name == $"Resource:{resource}")
            {
                resourceTilemap = tilemap;
                Debug.Log(resourceTilemap);
                resourceMap = tilemap.GetComponent<ResourceMap>();
            }
            else if(tilemap.name == $"Building:{resource}")
            {
                storageTilemap = tilemap;
            }
        }
        if(resourceTilemap == null)
        {
            Debug.LogWarning($"Cannot find resource tilemap '{resource}'");
            return null;
        }
        if(storageTilemap == null)
        {
            Debug.LogWarning($"Cannot find building tilemap '{resource}'");
            return null;
        }

        BT_Node root = new Selector();
        root.SetChildren(new List<BT_Node>()
        {
           new Sequence(new List<BT_Node>()
           {
                new Check_HasTarget(),
                new Selector(new List<BT_Node>()
                {
                    new Sequence(new List<BT_Node>()
                    {
                        new Check_InTargetRange(transform),
                        new Selector(new List<BT_Node>()
                        {
                            new Sequence(new List<BT_Node>()
                            {
                                new Check_TargetIsResource(),
                                new Timer(collection_Rate, new List<BT_Node>()
                                {
                                    new Task_Collect(resourceMap, maxStorage),
                                })//, UpdateResourceFillBar)
                            }),
                        })
                    }),
                    new Task_Walk(transform, pathfinder, moveSpeed, (Vector2 dir) =>
                    {
                        if (Mathf.Approximately(dir.x, 1f))
                        {
                            // going right
                            _spriteRenderer.sprite = _horizontalSprite;
                            _spriteRenderer.flipX = false;
                            _spriteRenderer.flipY = false;
                        }
                        else if (Mathf.Approximately(dir.x, -1f))
                        {
                            // going left
                            _spriteRenderer.sprite = _horizontalSprite;
                            _spriteRenderer.flipX = true;
                            _spriteRenderer.flipY = false;
                        }
                        else if (Mathf.Approximately(dir.y, 1f))
                        {
                            // going up
                            _spriteRenderer.sprite = _verticalSprite;
                            _spriteRenderer.flipX = false;
                            _spriteRenderer.flipY = true;
                        }
                        else if (Mathf.Approximately(dir.y, -1f))
                        {
                            // going down
                            _spriteRenderer.sprite = _verticalSprite;
                            _spriteRenderer.flipX = false;
                            _spriteRenderer.flipY = false;
                        }
                    }),
                }),
           }),
           new Task_FindClosestTarget(transform, resourceTilemap, true)
        }, forceRoot: true);
        root.SetData("current_resource_amount", 0);
        return root;

        throw new System.NotImplementedException();
    }
    private void UpdateResourceFillBar()
    {
        int curAmount = (int)_root.GetData("current_resource_amount");
        float resourceRatio = curAmount / (float)maxStorage;
        resourceFillBar.localScale = new Vector3(resourceRatio, 1, 1);
        resourceFillBar.localPosition = new Vector3(-0.5f + resourceRatio / 2f, 0, 0);
    }
}
