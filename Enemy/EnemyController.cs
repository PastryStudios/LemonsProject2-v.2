using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private HealthSystem healthSystem;
    private EnemyTypeSO enemyType;

    private void Start()
    {
        enemyType = GetComponent<EnemyTypeHolder>().enemyType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(enemyType.maxHealth, true);
        healthSystem.OnDeath += HealthSystem_OnDeath;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
    }
    private void HealthSystem_OnDeath(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }
    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {

    }
}
