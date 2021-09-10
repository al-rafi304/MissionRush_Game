using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;


public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string playStoreID = "4109881";

    private string videoAd = "video";
    private string rewardedAd = "rewardedVideo";

    public bool isTest;
    public bool adWatched;

    public GameObject adButton;
    public Button buttonAd;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(playStoreID, isTest);
        adWatched = false;

    }

    void Update()
    {
        if(Advertisement.IsReady() && !adWatched)
        {
            adButton.SetActive(true);
        }
        else if(!Advertisement.IsReady() || adWatched)
        {
            adButton.SetActive(false);
            //Debug.Log("Ad Off");
        }
    }
    
    void InitializeAd()
    {
        Advertisement.Initialize(playStoreID, isTest);
    }

    public void showVideoAd()
    {
        if(!Advertisement.IsReady(videoAd)) return;

        Advertisement.Show(videoAd);
    }
    public void showRewardedAd()
    {
        if(!Advertisement.IsReady(rewardedAd)) return;

        Advertisement.Show(rewardedAd);
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
        // if(!adWatched)
        // {
        //     adButton.SetActive(true);
        // }  
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //Add a muting audio function so that the audio does not overlap with ads audio
        FindObjectOfType<AudioManager>().Mute();
        Debug.Log("Muted");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            // case ShowResult.Failed:
            //     break;
            // case ShowResult.Skipped:
            //     break;
            case ShowResult.Finished:
                if(placementId == rewardedAd)
                {
                    Debug.Log("Player has been rewarded !");
                    //adButton.SetActive(false);
                    adWatched = true;
                    //buttonAd.interactable = false;
                    PlayerPrefs.SetInt("reward", 1);
                }
                break;
        }
    }
}
