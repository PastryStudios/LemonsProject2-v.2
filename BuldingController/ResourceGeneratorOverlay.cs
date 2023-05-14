using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform barTransform;

    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetGeneratorData();

        barTransform = transform.Find("bar");

        transform.Find("resourceIcon").GetComponent<SpriteRenderer>().sprite = 
            resourceGeneratorData.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().
            SetText(resourceGenerator.AmountGenerated().ToString("F1"));
    }

    private void Update()
    {
        barTransform.localScale = new Vector3((1-resourceGenerator.GetTimerNormalized()), 1, 1);
    }
}
