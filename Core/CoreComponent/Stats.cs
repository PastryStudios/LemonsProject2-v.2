using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : CoreComponent
{
    public float maxHealth;
    public float currentHealth { get; private set; }
    //private HealthBar healthBar;
    //private Healthbar healthbar;

    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
        
    }

    #region Set Functions

    public void DecreaseHealth(float amount)
    {
        //Change health when damage is taken
        currentHealth -= amount;
        
        //Call HealthBar to change by "amount"
        //core.HealthBar.ChangeHealthBar(currentHealth);

        if (currentHealth <= 0.01)
        {
            //death.OnDeath();
            Destroy(core.transform.parent.gameObject);
        }
    }
    public void ChangeHealth(float amount)
    {
        //Change health when damage is taken
        currentHealth -= amount;

        //Call HealthBar to change by "amount"
        //core.HealthBar.ChangeHealthBar(currentHealth);

        if (currentHealth <= 0.01)
        {
            //death.OnDeath();
        }
    }
    #endregion
}
