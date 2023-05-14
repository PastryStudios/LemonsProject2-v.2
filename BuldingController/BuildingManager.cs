using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxConstructionRadius = 100f;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    [SerializeField] private BuildingController hqBuilding;

    public event EventHandler<OnActiveBuildingTypeChangeEventArgs> OnActiveBuildingTypeChange;

    public class OnActiveBuildingTypeChangeEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    private void Start()
    {
        mainCamera = Camera.main;

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(activeBuildingType != null)
            {
                if (CanSpawnBuilding(activeBuildingType, InputHandler.GetMouseWorldPosition(), out string errorMessage))
                {

                    if (ResourceManager.Instance.CanAfford(activeBuildingType.buildingCostArray))
                    {
                        ResourceManager.Instance.SpendResources(activeBuildingType.buildingCostArray);
                        Instantiate(activeBuildingType.prefab,
                            InputHandler.GetMouseWorldPosition(), Quaternion.identity);
                    }
                    else
                    {
                        ToolTipUI.Instance.Show("Cannot afford " + activeBuildingType.GetConstructionCostString(),
                            new ToolTipUI.TooltipTimer { timer = 2f });
                    }
                }
                else
                {
                    ToolTipUI.Instance.Show(errorMessage, new ToolTipUI.TooltipTimer { timer = 2f });
                }
            
            }
            //Debug.Log("CanSpawnBuilding " + CanSpawnBuilding((buildingTypeList.list[0]), 
                //InputHandler.GetMouseWorldPosition()));
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChange?.Invoke(this,
            new OnActiveBuildingTypeChangeEventArgs { activeBuildingType = activeBuildingType });
    }
    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "Area not clear";
            return false;
        }

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach(Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                if(buildingTypeHolder.buildingType == buildingType)
                {
                    //Building of same type exists in radius
                    Debug.Log("Building of same type is too close");
                    errorMessage = "Building of same type is too close";

                    return false;
                }
            }
        }

        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach(Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                errorMessage = "";

                return true;
            }
            //else if (buildingTypeHolder == null)
            //{
            //    Debug.Log("Too far!");
            //}
        }
        errorMessage = "Building is too far from base.";

        return false;
    }

    public BuildingController GetHQBuilding()
    {
        return hqBuilding;
    }
}
