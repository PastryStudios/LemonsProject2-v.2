using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newUnitData", menuName = "Data/UnitData/Base Data")]

public class TestUnitData : ScriptableObject
{
    #region Movement Data
    public float moveSpeed = 10f;
    #endregion

    #region Attack Data
    public float attackRadius = 1f;
    public float attackDamage = 1f;
    public LayerMask whatIsEnemy;
    public float attackDelay = 1f;

    #endregion
}
