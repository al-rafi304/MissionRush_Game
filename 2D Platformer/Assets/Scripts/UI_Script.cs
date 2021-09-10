using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Script : MonoBehaviour
{
    public GameObject startUI;
    public GameObject inGameUI;
    
    void Update()
    {
        if(!playerVariables.isPlaying) isPlaying(false);
        else isPlaying(true);
    }

    void isPlaying(bool yes)
    {
        //startUI.SetActive(!yes);
        inGameUI.SetActive(yes);
    }
}
