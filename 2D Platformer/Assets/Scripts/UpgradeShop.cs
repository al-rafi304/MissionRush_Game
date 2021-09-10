using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    public string playerPrefName;
    public UpgradeItems[] items;
    //public ScoreManager scoreManager;

    void Start()
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i].playerPref_Total = playerPrefName;
            items[i].playerPref_Level = items[i].upgradeName + "_Level";
            items[i].playerPref_valueToUpgrade = items[i].upgradeName + "_increament";
            items[i].upgradeButton.onClick.AddListener(items[i].OnClick);
            items[i].balance = PlayerPrefs.GetInt(playerPrefName, 0);
            items[i].progressBar.setSlider(PlayerPrefs.GetInt(items[i].playerPref_Level, 1));
            items[i].audioManager = FindObjectOfType<AudioManager>();
            if(PlayerPrefs.GetInt(items[i].playerPref_valueToUpgrade, 0) == 0)
            {
                PlayerPrefs.SetInt(items[i].playerPref_valueToUpgrade, items[i].valueToUpgrade);
            }
        }
    }

    private void Update() {
        
    }
    public void Reset(){
        for(int i = 0; i < items.Length; i++)
        {
            PlayerPrefs.SetInt(items[i].playerPref_valueToUpgrade, 10);
            PlayerPrefs.SetInt(items[i].playerPref_Level, 1);
        }
        PlayerPrefs.SetInt(playerPrefName, 0);
    }

}

[System.Serializable]
public class UpgradeItems
{
    [HideInInspector]
    public AudioManager audioManager;
    [HideInInspector]
    public int balance;
    [HideInInspector]
    public string playerPref_Total;
    [HideInInspector]
    public string playerPref_Level;
    [HideInInspector]
    public string playerPref_valueToUpgrade;

    public string upgradeName;
    public Button upgradeButton;
    public ProgressBarScript progressBar;
    // public Scrollbar upgradeStatus;
    [HideInInspector]
    public int currentLevel;
    public int maxLevel;                //Max level of upgrade or the amount of time item can be upgraded
    public int price;                   //Price of upgrading
    public int valueToUpgrade;            //Value that upgrades
    public int valueInceament;          //Inceament of valueToUpgrade or the value that gets added to valueToUpgrade after each upgrade
    [HideInInspector]
    public int value;

    public void OnClick()
    {
        // balance = PlayerPrefs.GetInt(playerPref_Total, 0);
        currentLevel = PlayerPrefs.GetInt(playerPref_Level, 1);
        valueToUpgrade = PlayerPrefs.GetInt(playerPref_valueToUpgrade, valueToUpgrade);
        balance = PlayerPrefs.GetInt("totalPoints", 0);
        if(currentLevel < maxLevel && balance >= price)
        {
            audioManager.Play("PowerUp");

            currentLevel += 1;
            PlayerPrefs.SetInt(playerPref_Level, currentLevel);
            progressBar.setSlider(currentLevel);

            balance -= price;
            PlayerPrefs.SetInt(playerPref_Total, balance);

            valueToUpgrade += valueInceament;
            PlayerPrefs.SetInt(playerPref_valueToUpgrade, valueToUpgrade);

            Debug.Log("Clicked " + upgradeName + "Level: " + currentLevel + "TotalPoints: " + balance + "Value" + valueToUpgrade);


        }
        else
            Debug.Log("Max Reached" + currentLevel + balance);

    }

}
