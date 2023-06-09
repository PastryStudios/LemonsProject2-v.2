using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position,
           resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                //It's a resource node
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0,
            resourceGeneratorData.maxResourceAmount);

        return nearbyResourceAmount;
    }
    private float timer;
    private float timerMax;
    private ResourceGeneratorData resourceGeneratorData;

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        if (nearbyResourceAmount == 0)
        {
            //Disable resource generation
            enabled = false; 
        }
        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) +
                resourceGeneratorData.timerMax *
                (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
        }
        //Debug.Log("nearbyResource Amount: " + nearbyResourceAmount + "; " + "timerMax: " + timerMax);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timerMax;
            //Debug.Log("Ding!" + buildingType.resourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }

    public ResourceGeneratorData GetGeneratorData()
    {
        return resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        return timer / timerMax;
    }
    public float AmountGenerated()
    {
        return 1 / timerMax;
    }
}
