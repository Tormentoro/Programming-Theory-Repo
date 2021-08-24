using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] rocks;
    [SerializeField] GameObject alien;
    [SerializeField] GameObject health;
    [SerializeField] GameObject bomb;
    [SerializeField] GameObject alienBoss;
    [SerializeField] GameObject safeSpace;

    private float zSpawnRocks = 33;
    private float zSpawnAlien = 35;
    private float zSpawnPowerUp = 45;
    private float xSpawnLimitAlien = 17.5f;
    private float xSpawnLimitRocks = 17.5f;
    private float xSpawnLimitPowerup = 15f;
    private float ySpawn = 0.4f;

    private float startDelay = 0;
    private float obstaclesSpawnTime = 2.5f;
    private float healthSpawnTime = 7;
    private float bombSpawnTime = 13;
    private float alienSpawnTime = 2;

    private Quaternion currentRotation;

    void Start()
    {
        InvokeRepeating("spawnRocks", startDelay, obstaclesSpawnTime);
        InvokeRepeating("spawnAlien", startDelay, alienSpawnTime);
        InvokeRepeating("spawnHealth", startDelay, healthSpawnTime);
        InvokeRepeating("spawnAlienBoss", startDelay, healthSpawnTime);
        InvokeRepeating("spawnBomb", startDelay, bombSpawnTime);
    }
    private void Update()
    {
        if ((GameManager.GM.ufoKillCounter >= 10 && GameManager.GM.gameDifficulty == "Easy")
            || (GameManager.GM.ufoKillCounter >= 15 && GameManager.GM.gameDifficulty == "Normal")
            || (GameManager.GM.ufoKillCounter >= 20 && GameManager.GM.gameDifficulty == "Hard"))
            if (!GameManager.GM.bossAlive)
            {
                GameManager.GM.ufoKillCounter = 0;
                Debug.Log("Boss Spawned");
                GameManager.GM.bossSpawned = true;
                GameManager.GM.bossAlive = true;
            }
        SpawnSafeSpace();                                                               //ABSTRACTION
    }
    private void spawnRocks()                                                           //ABSTRACTION
    {
        if (!GameManager.GM.playerDead && !GameManager.GM.safeSpaceActivated)
        {
            float randomX = Random.Range(-xSpawnLimitRocks, xSpawnLimitRocks);
            int randomIndex = Random.Range(0, rocks.Length);
            float randomRotY = Random.Range(0f, 180f);

            Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawnRocks);
            Vector3 currentEulerAngleY = new Vector3(rocks[randomIndex].transform.rotation.x, randomRotY, rocks[randomIndex].transform.rotation.z);
            currentRotation.eulerAngles = currentEulerAngleY;
            rocks[randomIndex].transform.rotation = currentRotation;

            Instantiate(rocks[randomIndex], spawnPos, currentRotation);
        }
    }
    private void spawnAlien()                                                           //ABSTRACTION
    {
        GameObject pooledUfo = ObjectPooler.PO.GetPooledUfo();                           
        if (!GameManager.GM.playerDead && !GameManager.GM.safeSpaceActivated)
            if (!GameManager.GM.bossSpawned && !GameManager.GM.bossAlive)
            {

                float randomX = Random.Range(-xSpawnLimitAlien, xSpawnLimitAlien);
                Vector3 spawnPos = new Vector3(randomX, ySpawn + 2.7f, zSpawnAlien);
                if (pooledUfo != null)
                {
                    pooledUfo.SetActive(true);                                              
                    pooledUfo.transform.position = spawnPos;                                
                }
            }
    }
    private void spawnAlienBoss()                                                       //ABSTRACTION
    {
        if (GameManager.GM.bossSpawned && GameManager.GM.bossAlive && !GameManager.GM.safeSpaceActivated)
        {
            GameManager.GM.bossSpawned = false;
            //GameManager.GM.ufoKillCounter = 0;
            Debug.Log("Boss Spawned");
            float randomX = Random.Range(-xSpawnLimitAlien, xSpawnLimitAlien);
            Vector3 spawnPos = new Vector3(randomX, ySpawn + 2f, zSpawnAlien);

            Instantiate(alienBoss, spawnPos, alienBoss.gameObject.transform.rotation);
        }
    }
    private void spawnHealth()                                                              //ABSTRACTION
    {
        if (!GameManager.GM.playerDead && !GameManager.GM.safeSpaceActivated)
        {
            float randomX = Random.Range(-xSpawnLimitPowerup, xSpawnLimitPowerup);
            Vector3 spawnPos = new Vector3(randomX, ySpawn + 0.9f, zSpawnPowerUp);

            Instantiate(health, spawnPos, health.gameObject.transform.rotation);
        }
    }
    private void spawnBomb()                                                            //ABSTRACTION
    {   
        if (!GameManager.GM.playerDead && !GameManager.GM.safeSpaceActivated)
        {
            float randomX = Random.Range(-xSpawnLimitPowerup, xSpawnLimitPowerup);
            Vector3 spawnPos = new Vector3(randomX, ySpawn + 0.9f, zSpawnPowerUp);

            Instantiate(bomb, spawnPos, bomb.gameObject.transform.rotation);
        }
    }
    private void SpawnSafeSpace()                                                       //ABSTRACTION
    {
        if (!GameManager.GM.playerDead && GameManager.GM.bossKillCounter == GameManager.GM.victoryBossKillCounter)
        {
            GameManager.GM.bossKillCounter = 0;
            GameManager.GM.safeSpaceActivated = true;
            Vector3 spawnPos = new Vector3(0, ySpawn, zSpawnPowerUp+2);
            Instantiate(safeSpace, spawnPos, safeSpace.gameObject.transform.rotation);
        }
    }
}
