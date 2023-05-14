using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get => GenericNotImplementedError<Movement>.TryGet(movement, transform.parent.name);
        private set => movement = value;
    }
    public DirectPath DirectPath
    {
        get => GenericNotImplementedError<DirectPath>.TryGet(directPath, transform.parent.name);
        private set => directPath = value;
    } 
    public Stats Stats
    {
        get => GenericNotImplementedError<Stats>.TryGet(stats, transform.parent.name);
        private set => stats = value;
    }
    public HealthBar HealthBar
    {
        get => GenericNotImplementedError<HealthBar>.TryGet(healthBar, transform.parent.name);
        private set => healthBar = value;
    }
    public CollisionSenses CollisionSenses
    {
        get => GenericNotImplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name);
        private set => collisionSenses = value;
    }
    public Combat Combat
    {
        get => GenericNotImplementedError<Combat>.TryGet(combat, transform.parent.name);
        private set => combat = value;
    }

    private Movement movement;
    private DirectPath directPath;
    private Stats stats;
    private HealthBar healthBar;
    private CollisionSenses collisionSenses;
    private Combat combat;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        DirectPath = GetComponentInChildren<DirectPath>();
        Stats = GetComponentInChildren<Stats>();
        HealthBar = GetComponentInChildren<HealthBar>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
        //Combat.LogicUpdate();
    }
}
