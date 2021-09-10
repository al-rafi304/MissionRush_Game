using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private GameManagerScript gmScript;
    private UpgradeShop shopScript;
    public float magnetTime;
    public float shieldTime;
    public float doublePointsTime;
    public float boostTime;

    [HideInInspector]
    public float timer1;
    [HideInInspector]
    public float timer2;
    [HideInInspector]
    public float timer3;
    [HideInInspector]
    public float timer4;
    
    private void Start() 
    {
        gmScript = GetComponent<GameManagerScript>();
        shopScript = GetComponent<UpgradeShop>();

        // magnetTime = PlayerPrefs.GetFloat("mTime", 10);
        // doublePointsTime = PlayerPrefs.GetFloat("dpTime", 10);
        // boostTime = PlayerPrefs.GetFloat("bTime", 10);

        

        /*
        GlobalVariable.mTime = magnetTime;
        GlobalVariable.dpTime = doublePointsTime;
        GlobalVariable.bTime = boostTime;
        GlobalVariable.sTime = shieldTime;
        */

        GlobalVariable.magnetBool = false;
        GlobalVariable.shieldBool = false;
        GlobalVariable.doubelPointsBool = false;
        GlobalVariable.BoostBool = false;

        
    }
    public void updatePlayerPrefs()
    {
        if(PlayerPrefs.GetInt(shopScript.items[0].playerPref_valueToUpgrade, 0) == 0)
        {
            PlayerPrefs.SetInt(shopScript.items[0].playerPref_valueToUpgrade, shopScript.items[0].valueToUpgrade);
            magnetTime = PlayerPrefs.GetInt(shopScript.items[0].playerPref_valueToUpgrade, 1);
        }
        else if(PlayerPrefs.GetInt(shopScript.items[1].playerPref_valueToUpgrade, 0) == 0)
        {
            PlayerPrefs.SetInt(shopScript.items[1].playerPref_valueToUpgrade, shopScript.items[1].valueToUpgrade);
            doublePointsTime = PlayerPrefs.GetInt(shopScript.items[1].playerPref_valueToUpgrade, 1);
        }
        else if(PlayerPrefs.GetInt(shopScript.items[2].playerPref_valueToUpgrade, 0) == 0)
        {
            PlayerPrefs.SetInt(shopScript.items[2].playerPref_valueToUpgrade, shopScript.items[2].valueToUpgrade);
            boostTime = PlayerPrefs.GetInt(shopScript.items[2].playerPref_valueToUpgrade, 1);
        }
        else
        {
            magnetTime = PlayerPrefs.GetInt(shopScript.items[0].playerPref_valueToUpgrade, 1);
            doublePointsTime = PlayerPrefs.GetInt(shopScript.items[1].playerPref_valueToUpgrade, 1);
            boostTime = PlayerPrefs.GetInt(shopScript.items[2].playerPref_valueToUpgrade, 1);
        }
    }

    private void Update() 
    {
        magnetTime = PlayerPrefs.GetInt(shopScript.items[0].playerPref_valueToUpgrade, shopScript.items[0].valueToUpgrade);
        doublePointsTime = PlayerPrefs.GetInt(shopScript.items[1].playerPref_valueToUpgrade, shopScript.items[1].valueToUpgrade);
        boostTime = PlayerPrefs.GetInt(shopScript.items[2].playerPref_valueToUpgrade, shopScript.items[2].valueToUpgrade);


        //Magnet Time Resetting 
        if(GlobalVariable.magnetBool == true)
        {
            timer1 += Time.deltaTime;
        }
        if(timer1 >= magnetTime || GlobalVariable.magnetBool == false)
        {
            timer1 = 0;
            GlobalVariable.magnetBool = false;
        }

        //Shield Time Resetting
        if(GlobalVariable.shieldBool)
        {
            timer2 += Time.deltaTime;
        }
        if(timer2 >= shieldTime || GlobalVariable.shieldBool == false)
        {
            timer2 = 0;
            GlobalVariable.shieldBool = false;
        }

        //Doubling Points Time Resetting
        if(GlobalVariable.doubelPointsBool)
        {
            timer3 += Time.deltaTime;
        }
        if(timer3 >= doublePointsTime || GlobalVariable.doubelPointsBool == false)
        {
            timer3 = 0;
            GlobalVariable.doubelPointsBool = false;
        }

        //Boost Time Resetting
        if(GlobalVariable.BoostBool)
        {
            timer4 += Time.deltaTime;
        }
        if(timer4 >= boostTime || GlobalVariable.BoostBool == false)
        {
            timer4 = 0;
            GlobalVariable.BoostBool = false;
        }
    }
}

public static class GlobalVariable
{
    public static bool magnetBool;
    public static bool shieldBool;
    public static bool doubelPointsBool;
    public static bool BoostBool;

    public static float dpTime;
    public static float sTime;
    public static float mTime;
    public static float bTime;
}