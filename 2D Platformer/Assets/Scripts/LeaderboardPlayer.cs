using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardPlayer : MonoBehaviour
{
    public SkinManager skinManager;

    public TMP_Text playerName;
    private string orderNumber;
    public TMP_Text points;
    public Image playerSprite;

    // void Start()
    // {
    //     skinManager = FindObjectOfType<SkinManager>();
    //     Debug.Log(skinManager + "added");
    // }

    public void setData(string _name, string place, string _points, int skinIndex)
    {
        orderNumber = place;
        playerName.text = orderNumber + ". " + _name;
        points.text = _points;

        skinManager = FindObjectOfType<SkinManager>();
        var skin = skinManager.skins[skinIndex];

        if(skin.colorBool)
        {
            playerSprite.color = skin.color;
            //playerSprite.sprite = defaultSprite;
            //playerTrail.colorGradient = skin.trailColor;
            //Debug.Log(playerSprite.color);
        }
        else if(skin.spriteBool)
        {
            playerSprite.sprite = skin.sprite;
            playerSprite.color = Color.white;
            //playerTrail.colorGradient = skin.trailColor;
            //Debug.Log(playerSprite.sprite.name);
        }
    }
}
