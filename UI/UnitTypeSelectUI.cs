using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTypeSelectUI : MonoBehaviour
{
    [SerializeField] private float offsetAmount = 100f;
    private Dictionary<UnitTypeSO, Transform> buttonTransformDictionary;
    private UnitTypeListSO unitTypeList;
    private UnitTypeSO unitType;
    [SerializeField] private Sprite arrowSprite;
    private Transform arrowButton;



    private void Awake()
    {
        Transform buttonTemplate = transform.Find("ButtonTemplate");
        buttonTemplate.gameObject.SetActive(false);

        unitTypeList = Resources.Load<UnitTypeListSO>(typeof(UnitTypeListSO).Name);
        buttonTransformDictionary = new Dictionary<UnitTypeSO, Transform>();

        int index = 0;
        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0f);
        arrowButton.Find("UnitImage").GetComponent<Image>().sprite = arrowSprite;
        MouseEnterExitEvents mouseEnterExitEvents = arrowButton.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sent, EventArgs e) =>
        {
            ToolTipUI.Instance.Show("Arrow");
        };
        mouseEnterExitEvents.OnMouseExit += (object sent, EventArgs e) =>
        {
            ToolTipUI.Instance.Hide();
        };

        arrowButton.GetComponent<Button>().onClick.AddListener(() => {
            UnitCreationManager.Instance.SetActiveUnitType(null);
        });

        index++;

        foreach (UnitTypeSO unitType in unitTypeList.list)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            buttonTransform.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(offsetAmount * index, 0f);
            buttonTransform.Find("UnitImage").GetComponent<Image>().sprite =
                unitType.sprite;
            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                UnitCreationManager.Instance.SetActiveUnitType(unitType);
            });
            mouseEnterExitEvents = buttonTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sent, EventArgs e) =>
            {
                ToolTipUI.Instance.Show(unitType.nameString + "\n" + unitType.GetConstructionCostString());
            };
            mouseEnterExitEvents.OnMouseExit += (object sent, EventArgs e) =>
            {
                ToolTipUI.Instance.Hide();
            };
            buttonTransformDictionary[unitType] = buttonTransform;
            index++;
        }
    }
    private void Start()
    {
        UnitCreationManager.Instance.OnActiveUnitTypeChange += UnitCreationManager_OnActiveUnitTypeChange;
        UpdateActiveUnitTypeButton();
    }

    private void UnitCreationManager_OnActiveUnitTypeChange(object sender, UnitCreationManager.OnActiveUnitTypeChangeEventArgs e)
    {
        UpdateActiveUnitTypeButton();
    }

    private void UpdateActiveUnitTypeButton()
    {
        foreach(UnitTypeSO unitType in buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = buttonTransformDictionary[unitType];
            buttonTransform.Find("selected").gameObject.SetActive(false);
        }
        UnitTypeSO activeUnitType = UnitCreationManager.Instance.GetActiveUnitType();
        buttonTransformDictionary[activeUnitType].Find("selected").gameObject.SetActive(true);
    }

}
