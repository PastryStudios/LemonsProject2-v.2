using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] EnemyTypeSO enemyType;
    //private HealthSystem healthSystem;


    public static EnemyGenerator Create(Vector3 position)
    {
        Transform pfEnemyGenerator = Resources.Load<Transform>("pfGeneratorImp");
        //buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        Transform generatorTransform = Instantiate(pfEnemyGenerator, position, Quaternion.identity);
        EnemyGenerator enemyGenerator = generatorTransform.GetComponent<EnemyGenerator>();
        return enemyGenerator;
    }

    private void Start()
    {
        StartCoroutine(spawnEnemy(Random.Range(enemyType.spawnRateMin, enemyType.spawnRateMax), enemyType.prefab));
    }
    
   
    private IEnumerator spawnEnemy(float interval, Transform enemy)
    {
        yield return new WaitForSeconds(interval);
        Transform newenemy = Instantiate(enemy, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
