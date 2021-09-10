using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRandomizer : MonoBehaviour
{
    private GameObject[] parts;
    private GameObject choosenPart;

    private bool PartSpawned;

    void OnEnable()
    {
        PartSpawned = false;
        parts = new GameObject[transform.Find("Parts").childCount];
        
        for(int i = 0; i < this.transform.Find("Parts").childCount; i++)
        {
            parts[i] = this.transform.Find("Parts").GetChild(i).gameObject;
            parts[i].SetActive(false);
        }

    }
    
    private void Update() 
    {
        if(!PartSpawned && isActiveAndEnabled)
        {
            Randomize();
            PartSpawned = !PartSpawned;
        }
    }

    public void Randomize()
    {
        choosenPart = parts[Random.Range(0, parts.Length)];
        choosenPart.SetActive(true);
        //print("DONE");
    }
    
}
