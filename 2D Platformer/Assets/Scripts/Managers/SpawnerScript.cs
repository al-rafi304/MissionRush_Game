using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public enum SpawnType{ Obstacle, Ground};
    public SpawnType type;

    public Transform[] ObstacleParts;
    public GameObject Player;

    public float SpawnDistance;                         //Spawn distance from player
    public float LevelDistance;                         //Spawn distance from each level

    public bool isExpanding;
    private bool hasSpawned;

    Vector3 SpawnPoint;
    public Transform SpawnedPart;

    void Start()
    {
        if(type == SpawnType.Ground) SpawnedPart = ObstacleParts[0];
        else if(type == SpawnType.Obstacle) SpawnedPart = Instantiate(ObstacleParts[Random.Range(0,ObstacleParts.Length)], transform.position, Quaternion.identity);
    }

    void Update()
    {
        if(isExpanding)
            LevelDistance += 0.008f * Time.deltaTime;

        if(Vector3.Distance(Player.transform.position, SpawnedPart.Find("EndPosition").position) <= SpawnDistance)
        {
            //FindSpawnPoint();
            Spawn();
        }
    }

    void FindSpawnPoint()
    {
        SpawnPoint = SpawnedPart.Find("EndPosition").position;
        Spawn();
    }

    void Spawn()
    {
        Transform RandomObject = ObstacleParts[Random.Range(0,ObstacleParts.Length)];
        SpawnPoint = SpawnedPart.Find("EndPosition").position;
        
        //Debug.Log(RandomObject.name);
        if(RandomObject = SpawnedPart)
        {
            RandomObject = ObstacleParts[Random.Range(0,ObstacleParts.Length)];
            SpawnedPart = Instantiate(RandomObject, SpawnPoint + new Vector3(LevelDistance, 0,0), Quaternion.identity);
        }
        else if(RandomObject != SpawnedPart)
            SpawnedPart = Instantiate(RandomObject, SpawnPoint + new Vector3(LevelDistance, 0,0), Quaternion.identity);
            
    }
}
