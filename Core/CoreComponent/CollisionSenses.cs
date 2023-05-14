using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms

    public Transform MeleeAttack
    {
        get => GenericNotImplementedError<Transform>.TryGet(meleeAttack, core.transform.parent.name);
        private set => meleeAttack = value;
    }

    public float MeleeAttackRadius { get => meleeAttackRadius; set => meleeAttackRadius = value; }
    public LayerMask WhatIsEnemy { get => whatIsEnemy; set => whatIsEnemy = value; }

    [SerializeField] private Transform meleeAttack;
    [SerializeField] private float meleeAttackRadius;
    [SerializeField] private LayerMask whatIsEnemy;

    #endregion

    #region Set Functions

    public bool Enemy
    {
        get => Physics2D.OverlapCircle(MeleeAttack.position, meleeAttackRadius, whatIsEnemy);
    }

    #endregion

}
