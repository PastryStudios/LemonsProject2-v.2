using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    private void Start()
    {
        SetUnitUIVisible(false);
    }

    public void SetUnitUIVisible(bool active)
    {
        gameObject.SetActive(active);
    }
}
