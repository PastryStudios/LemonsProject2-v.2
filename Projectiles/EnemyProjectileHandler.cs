using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHandler : MonoBehaviour
{
    public static EnemyProjectileHandler Create(Vector3 position, BuildingController targetBuilding)
    {
        Transform pfImpBullet = Resources.Load<Transform>("pfImpBullet");
        //buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        Transform bulletTransform = Instantiate(pfImpBullet, position, Quaternion.identity);
        EnemyProjectileHandler bulletProjectile = bulletTransform.GetComponent<EnemyProjectileHandler>();
        bulletProjectile.SetTarget(targetBuilding);
        return bulletProjectile;
    }
    private Vector3 lastMoveDir;
    private float timeToDie = 2f;

    private BuildingController targetBuilding;
    private void Update()
    {
        Vector3 moveDir;
        if(targetBuilding != null)
        {
            moveDir = (targetBuilding.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        float moveSpeed = 10f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        timeToDie -= Time.deltaTime;
        if(timeToDie <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(BuildingController targetBuilding)
    {
        this.targetBuilding = targetBuilding;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BuildingController building = collision.GetComponent<BuildingController>();
        if(building != null)
        {
            //Building Hit
            int damageAmount = 10;
            targetBuilding.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
