using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObjects/EnemyTypeList"))]

public class EnemyTypeListSO : ScriptableObject
{
    public List<EnemyTypeSO> list;
}
