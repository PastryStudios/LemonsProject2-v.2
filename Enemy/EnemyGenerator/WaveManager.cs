using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private enum State
    {
        WaitingToSpawnWave,
        SpawingWave,
    }
    private State state;
    [SerializeField] private float waveSpawnTimerMax = 55;
    private float waveSpawnTimer;
    [SerializeField] private float generatorSpawnTimerMax = 2;
    private float generatorSpawnTimer;
    [SerializeField] private List<Transform> spawnPositionTransformList;
    private Vector3 spawnPosition;
    private int remainingEnemySpawn;

    private int waveNumber;

    private void Start()
    {
        state = State.WaitingToSpawnWave;
        SpawnWave();
        waveNumber = 0;
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnWave:
                waveSpawnTimer -= Time.deltaTime;

                if (waveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;
            case State.SpawingWave:
                if (remainingEnemySpawn > 0)
                {
                    generatorSpawnTimer -= Time.deltaTime;
                    if (generatorSpawnTimer < 0f)
                    {
                        generatorSpawnTimer = Random.Range(0, generatorSpawnTimerMax);
                        EnemyGenerator.Create(spawnPosition + (new Vector3(1, 3, 0) * Random.Range(-2, 5)));
                        remainingEnemySpawn--;
                        if(remainingEnemySpawn <= 0)
                        {
                            state = State.WaitingToSpawnWave;
                        }
                    }
                }
                break;
        }
    }
    private void SpawnWave()
    {
        spawnPosition = spawnPositionTransformList[Random.Range(0, spawnPositionTransformList.Count)].position;
        //int remainingSpawnAmount = 1;

        remainingEnemySpawn = 1;// + 2 * waveNumber;
        waveSpawnTimer = waveSpawnTimerMax;
        waveNumber++;
        state = State.SpawingWave;
    }
}
