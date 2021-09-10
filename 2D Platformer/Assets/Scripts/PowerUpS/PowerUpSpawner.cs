using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public Transform[] powerUps;
    private Transform spawnedObject;
    private GameObject player;
    private RunnerScript playerScript;
    
    public float maxSpawnTime;
    public float minSpawnTime;
    private float randomTime;
    private float timer;

    void Start()
    {
        randomTime = Random.Range(minSpawnTime - 1, maxSpawnTime +1);
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<RunnerScript>();
    }

    void Update()
    {
        spawnedObject = GetComponent<SpawnerScript>().SpawnedPart;
        
        if(playerScript.isRunning) timer += Time.deltaTime;
        
        if(timer >= randomTime)
        {
            Spawn();
            timer = 0;
        }

        //Debug.Log(randomTime);
    }

    void Spawn()
    {
        Vector2 spawnLocation = spawnedObject.Find("PowerUpHolder").position;
        Transform RandomPower = powerUps[Random.Range(0, powerUps.Length)];

        Instantiate(RandomPower, spawnLocation, Quaternion.identity);
        randomTime = Random.Range(minSpawnTime - 1, maxSpawnTime +1);
    }


}
