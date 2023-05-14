using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyType")]
public class EnemyTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public Sprite sprite;
    public int maxHealth;
    public float moveSpeed = 5f;
    public float spawnRateMin, spawnRateMax;
    public float detectionRadius = 15f;
    public float attackRadius = 3f;
    public float attackTimerDelayMax = 3f;

}
