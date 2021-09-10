using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkinManager : MonoBehaviour
{
    public GameObject skinGO;
    public SpriteRenderer playerSprite;
    public TrailRenderer playerTrail;
    public Sprite defaultSprite;

    //[Range(0,5)]
    public int skinIndex;
    public string skinIndexPP = "skinIndex";
    public skins[] skins;

    void Start()
    {
        // playerSprite = skinGO.GetComponent<SpriteRenderer>();
        // playerTrail = skinGO.GetComponent<TrailRenderer>();

        skinIndex = PlayerPrefs.GetInt(skinIndexPP, 0);
        changeSkin(skinIndex);
    }

    void Update()
    {
        changeSkin(PlayerPrefs.GetInt(skinIndexPP, 0));
    }

    public void changeSkin(int index)
    {
        PlayerPrefs.SetInt(skinIndexPP, index);
        var skin = skins[index];

        if(skin.colorBool)
        {
            playerSprite.color = skin.color;
            playerSprite.sprite = defaultSprite;
            playerTrail.colorGradient = skin.trailColor;
            //Debug.Log(playerSprite.color);
        }
        else if(skin.spriteBool)
        {
            playerSprite.sprite = skin.sprite;
            playerSprite.color = Color.white;
            playerTrail.colorGradient = skin.trailColor;
            //Debug.Log(playerSprite.sprite.name);
        }
    }
}

[System.Serializable]
public class skins
{
    public Color color;
    public Gradient trailColor;
    public Sprite sprite;

    public bool colorBool;
    public bool spriteBool;
}
