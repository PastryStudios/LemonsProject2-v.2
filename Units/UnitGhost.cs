using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    private void Awake()
    {
        spriteGameObject = transform.Find("GhostSprite").gameObject;

        Hide();
    }

    private void Start()
    {
        UnitCreationManager.Instance.OnActiveUnitTypeChange += UnitCreationManager_OnActiveUnitTypeChange;
    }

    private void UnitCreationManager_OnActiveUnitTypeChange(object sender,
        UnitCreationManager.OnActiveUnitTypeChangeEventArgs e)
    {
        if(e.activeUnitType == null)
        {
            Hide();
        }
        else
        {
            Show(e.activeUnitType.sprite);
        }
    }
    private void Update()
    {
        transform.position = InputHandler.GetMouseWorldPosition();
    }
    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }
    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}
