using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable
{
    private Movement movement;
    private Stats stats;
    private CollisionSenses collisionSenses;
    protected override void Awake()
    {
        base.Awake();
        //currentHP = GetComponentInParent<currentHP>();
    }


    public void Damage(float amount)
    {
        core.Stats.DecreaseHealth(amount);
        //Stats?.DecreaseHealth(amount);
        Debug.Log(core.transform.parent.name + " hit");

    }
}
