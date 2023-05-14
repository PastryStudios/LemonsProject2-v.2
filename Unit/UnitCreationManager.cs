using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitCreationManager : MonoBehaviour
{
    public static UnitCreationManager Instance { get; private set; }
    [SerializeField] private Camera mainCamera;
    private UnitTypeListSO unitTypeList;
    private UnitTypeSO activeUnitType;

    public event EventHandler<OnActiveUnitTypeChangeEventArgs> OnActiveUnitTypeChange;

    public class OnActiveUnitTypeChangeEventArgs : EventArgs
    {
        public UnitTypeSO activeUnitType;
    }

    private void Awake()
    {
        Instance = this;
        unitTypeList = Resources.Load<UnitTypeListSO>(typeof(UnitTypeListSO).Name);
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeUnitType != null )
            {
                if (ResourceManager.Instance.CanAffordUnit(activeUnitType.unitCostArray))
                {
                    ResourceManager.Instance.SpendResourcesUnit(activeUnitType.unitCostArray);
                    Instantiate(activeUnitType.prefab,
                            InputHandler.GetMouseWorldPosition(), Quaternion.identity);
                }
            }
            else
            {
                ToolTipUI.Instance.Show("Cannot afford " + activeUnitType.GetConstructionCostString(),
                    new ToolTipUI.TooltipTimer { timer = 2f });
            }
            
            //Debug.Log("CanSpawnBuilding " + CanSpawnBuilding((buildingTypeList.list[0]), 
            //InputHandler.GetMouseWorldPosition()));
        }       
    }
    public void SetActiveUnitType(UnitTypeSO unitType)
    {
        activeUnitType = unitType;
        OnActiveUnitTypeChange?.Invoke(this,
            new OnActiveUnitTypeChangeEventArgs { activeUnitType = activeUnitType });
    }
    public UnitTypeSO GetActiveUnitType()
    {
        return activeUnitType;
    }

}
