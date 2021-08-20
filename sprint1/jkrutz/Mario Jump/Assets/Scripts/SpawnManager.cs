using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 5;
    private int waveNumber;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 1;
        SpawnEnemyWave();
    }

    // Update is called once per frame
    void Update()
    {
        int numEnemies = FindObjectsOfType<EnemyController>().Length;
        if(numEnemies == 0)
        {
            waveNumber++;
            SpawnEnemyWave();
        }

    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(1, 1.5f);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, spawnPosY, spawnPosZ);
    }

    private void SpawnEnemyWave()
    {
        for(int i = 0; i < waveNumber; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
