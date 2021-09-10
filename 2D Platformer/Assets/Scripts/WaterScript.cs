using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private RunnerScript playerScript;
    private BuoyancyEffector2D effector2D;
    
    void Start()
    {
        playerScript = FindObjectOfType<RunnerScript>();
        effector2D = GetComponent<BuoyancyEffector2D>();
    }

    void Update()
    {
        if(GlobalVariable.BoostBool)
        {
            effector2D.linearDrag = 0;
            effector2D.angularDrag = 0;
            effector2D.flowMagnitude = 0;
        }
        else if(!GlobalVariable.BoostBool)
        {
            effector2D.density = playerScript.Buoyancy;

            effector2D.linearDrag = 5;
            effector2D.angularDrag = 5;
            effector2D.flowMagnitude = -50;
        }
    }
}
