using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerActivator : MonoBehaviour
{
    public PowerUpManager powerUpManager;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Magnet"))
        {
            powerUpManager.timer1 = 0;
            GlobalVariable.magnetBool = true;

            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
        if(other.CompareTag("Shield"))
        {
            powerUpManager.timer2 = 0;
            GlobalVariable.shieldBool = true;

            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
        if(other.CompareTag("DoublePoints"))
        {
            powerUpManager.timer3 = 0;
            GlobalVariable.doubelPointsBool = true;

            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
        if(other.CompareTag("Boost"))
        {
            powerUpManager.timer4 = 0;
            GlobalVariable.BoostBool = true;
            //Debug.Log("Collided with Boost");

            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
    }
}
