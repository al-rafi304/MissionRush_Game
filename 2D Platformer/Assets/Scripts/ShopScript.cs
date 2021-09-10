using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    private GameManagerScript gmScript;
    private PowerUpManager pmScript;

    public int upgradeValue;

    [SerializeField]
    private int magnetLevel=1;
    [SerializeField]
    private int boostLevel=1;
    [SerializeField]
    private int doublePointsLevel=1;

    public Scrollbar magnetSB;
    public Scrollbar boostSB;
    public Scrollbar doublePointsSB;
    
    private void Start() 
    {
        gmScript = GetComponent<GameManagerScript>();
        pmScript = GetComponent<PowerUpManager>();

        magnetLevel = PlayerPrefs.GetInt("MagnetLevel", 1);
        boostLevel = PlayerPrefs.GetInt("BoostLevel", 1);
        doublePointsLevel = PlayerPrefs.GetInt("DoublePointsLevel", 1);

        //magnetSB.size = (PlayerPrefs.GetInt("MagnetLevel", 1) * 25) / 100 ;
        magnetSB.size = (magnetLevel * 25f) / 100f ;
        boostSB.size = (boostLevel * 25f) / 100f;
        doublePointsSB.size = (doublePointsLevel * 25f) / 100f;
    }

    public void ResetUpgrades()
    {
        PlayerPrefs.SetFloat("mTime", 15);
        PlayerPrefs.SetFloat("bTime", 5);
        PlayerPrefs.SetFloat("dpTime", 15);

        PlayerPrefs.SetInt("MagnetLevel", 1);
        PlayerPrefs.SetInt("BoostLevel", 1);
        PlayerPrefs.SetInt("DoublePointsLevel", 1);

        PlayerPrefs.SetInt("totalPoints", 100000);

        Debug.Log("Reseted");
    }

    public void magnetUpgrade()
    {
        
        if(PlayerPrefs.GetInt("MagnetLevel", 0) < 4)
        {
            //Reduces Total Points
            gmScript.totalPoints -= upgradeValue;
            gmScript.SaveTotalPoints();

            //Increases Magnet Time
            float upgradedTime_M = pmScript.magnetTime += 2;
            PlayerPrefs.SetFloat("mTime", upgradedTime_M);

            //Increase Level Index
            magnetLevel += 1;
            PlayerPrefs.SetInt("MagnetLevel", magnetLevel);
            magnetSB.size = (magnetLevel * 25f) / 100f;

            Debug.Log(pmScript.magnetTime);
        }

        
    }

    public void boostUpgrade()
    {
        if(PlayerPrefs.GetInt("BoostLevel") < 4)
        {
            gmScript.totalPoints -= upgradeValue;
            gmScript.SaveTotalPoints();

            float upgradedTime_B = pmScript.boostTime += 2;
            PlayerPrefs.SetFloat("bTime", upgradedTime_B);

            boostLevel += 1;
            PlayerPrefs.SetInt("BoostLevel", boostLevel);
            boostSB.size = (boostLevel * 25f) / 100f;

            Debug.Log(pmScript.boostTime);
        }
        
    }

    public void doublePointsUpgrade()
    {
        if(PlayerPrefs.GetInt("DoublePointsLevel") < 4)
        {
            gmScript.totalPoints -= upgradeValue;
            gmScript.SaveTotalPoints();

            float upgradedTime_DP = pmScript.doublePointsTime += 2;
            PlayerPrefs.SetFloat("dpTime", upgradedTime_DP);

            doublePointsLevel += 1;
            PlayerPrefs.SetInt("DoublePointsLevel", doublePointsLevel);
            doublePointsSB.size = (doublePointsLevel * 25f) / 100f;

            Debug.Log(pmScript.doublePointsTime);
        }
        
    }

}
