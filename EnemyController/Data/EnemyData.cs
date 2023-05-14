using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyUnitData", menuName = "Data/EnemyUnitData/Base Data")]

public class EnemyData : ScriptableObject
{
    public float enemyHealth = 100f;
    public float attackRadius = 1f;
}
