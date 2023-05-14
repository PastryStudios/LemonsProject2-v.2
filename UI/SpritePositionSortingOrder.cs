using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float precisionMultiplier = 5f;
    [SerializeField] private Vector2 positionOffset;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)((transform.position.y + positionOffset.y) * precisionMultiplier);
        if (runOnce)
        { Destroy(this);
        }
    }
}
