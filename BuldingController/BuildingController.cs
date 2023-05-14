using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;
    void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
       
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(buildingType.maxHealth,true);
        healthSystem.OnDeath += HealthSystem_OnDeath;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        Debug.Log(healthSystem.GetHealthAmount());
    }

    void Update()
    {
       
    }
    private void HealthSystem_OnDeath(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }
}
