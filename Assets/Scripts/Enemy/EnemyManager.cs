using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private GameObject[] spawners;

    private bool spawning = false;

    [SerializeField]
    private float spawnDelay = 3f;

    [SerializeField]
    private float minimumSpawnDelay = 0.1f;

    [SerializeField]
    private float spawnShortening = 1f;

    [SerializeField]
    private float spawnNumbers = 1f;

    [SerializeField]
    private GameObject[] enemies;

    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("Enemy Spawn");
    }

    private void EnemySpawning()
    {
        int random = Random.Range(0, spawners.Length);
        GameObject loc = spawners[random];
        random = Random.Range(0, enemies.Length);
        Instantiate(enemies[random], loc.transform);
    }

    IEnumerator SpawnEnemy()
    {
        int j = (int)spawnNumbers;
        for(int i = 0; i < j; i++)
        {
            EnemySpawning();
        }

        yield return new WaitForSeconds(spawnDelay);

        if (spawnDelay > minimumSpawnDelay)
        {
            spawnDelay -= spawnShortening * Time.deltaTime;
        }
        if (j < 5)
        {
            spawnNumbers += spawnShortening * Time.deltaTime;
        }

        spawning = false;
    }


    private void Update()
    {
        if(spawning == false)
        {
            spawning = true;
            StartCoroutine(SpawnEnemy());
        }
    }


}
