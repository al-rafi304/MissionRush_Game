using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public Transform Player;
    public float spawnDistance;
    public WeightedObject[] Parts;

    private Transform spawnedPart;
    private Vector3 spawnPoint;

    private int randomNumber;
    private int totalSum;
    private Transform randomObject;

    private void Start() 
    {
        for(int i = 0; i < Parts.Length; i++)
        {
            WeightedObject data = Parts[i];
            totalSum += data.weight;
        }
        // Debug.Log(totalSum);
        spawnedPart = Parts[0].item.transform;
    }

    private void Update() 
    {
        if(Vector2.Distance(Player.transform.position, spawnedPart.Find("EndPosition").position) <= spawnDistance)
        {
            randomNumber = Random.Range(0, totalSum + 1);
            //Debug.Log(randomNumber);
            Spawn();
        }    

    }

    private void Spawn()
    {
        for(int i = 0; i < Parts.Length; i++)
        {
            if(randomNumber <= Parts[i].weight)
            {
                randomObject = Parts[i].item;
                break;
            }
            else
            {
                randomNumber -= Parts[i].weight;
                continue;
            }
        }

        // Debug.Log(randomObject.name);
        spawnPoint = spawnedPart.Find("EndPosition").position;

        spawnedPart = Instantiate(randomObject, spawnPoint, Quaternion.identity);
        
        
    }

}

[System.Serializable]
public class WeightedObject
{
    public Transform item;
    public int weight;
}
