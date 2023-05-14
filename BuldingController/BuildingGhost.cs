using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    private ResourceNearbyOverlay resourceNearbyOverlay;
    private void Awake()
    {
        spriteGameObject = transform.Find("GhostSprite").gameObject;
        resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlayGhost").GetComponent<ResourceNearbyOverlay>();

        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChange += 
            BuildingManager_OnActiveBuildingTypeChange;
    }
    private void BuildingManager_OnActiveBuildingTypeChange(object sender, 
        BuildingManager.OnActiveBuildingTypeChangeEventArgs e)
    {
        if(e.activeBuildingType == null){
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
            if (e.activeBuildingType.hasResourceGeratorData) { 
                resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);                
            }
            else
            {
                resourceNearbyOverlay.Hide();
            }
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
