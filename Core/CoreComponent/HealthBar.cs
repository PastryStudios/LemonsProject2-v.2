using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //public Slider Slider;
    [SerializeField] private Image _healthBarSprite;
    //private EnemyUnit enemyUnit;
    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;

    private void Awake()
    {
        //SetHealthbarVisible(false);
        barTransform = transform.Find("Foreground");
    }
    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        UpdateBar();
        SetHealthbarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
        SetHealthbarVisible();
    }

    //public void SetHealth(float CurrentHealth, float maxHealth)
    //{
    //    if(CurrentHealth < maxHealth)
    //    {
    //        //gameObject.SetActive(true);
    //        SetHealthbarVisible(true);
    //    }
    //    else
    //    {
    //        //gameObject.SetActive(false);
    //        //SetHealthbarVisible(false);

    //    }
    //    _healthBarSprite.fillAmount = CurrentHealth / maxHealth;
    //}

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.CurrentHealthNormalized(), 1, 1);
        //SetHealthbarVisible(true);

    }

    private void SetHealthbarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}
