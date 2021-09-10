using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSystem : MonoBehaviour
{
    private ThemeActivator[] themedObjects;

    public enum key{one = 0, two = 1};
    public key selector;
    
    //[Range(0,5)]
    public int themeIndex;

    public ColorVar[] colorVar;

    private string themeidxPP = "themeIndex";

    void Start()
    {
        themeIndex = PlayerPrefs.GetInt(themeidxPP, 0);
        change(themeIndex);
    }

    // Update is called once per frame
    void Update()
    {
        

        change(PlayerPrefs.GetInt(themeidxPP, 0));
    }

    void getIndex(int index)
    {
        themeIndex = index;
        PlayerPrefs.SetInt(themeidxPP, themeIndex);
    }
    public void changeTheme(int index)
    {
        themeIndex = index;
        PlayerPrefs.SetInt(themeidxPP, themeIndex);
        Debug.Log("CurrentTheme: " + themeIndex);
        //change(themeIndex);
    }

    public void change(int idx)
    {
        themedObjects = FindObjectsOfType<ThemeActivator>();
        foreach(var item in themedObjects)
        {
            if(item.color01)
            {
                item.GetComponent<SpriteRenderer>().color = colorVar[idx].color01;
            }
            else if(item.color02)
            {
                item.GetComponent<SpriteRenderer>().color = colorVar[idx].color02;
            }
            else if(item.accent01)
            {
                item.GetComponent<SpriteRenderer>().color = colorVar[idx].accent01;
            }
            else if(item.accent02)
            {
                item.GetComponent<SpriteRenderer>().color = colorVar[idx].accent02;
            }
            else if(item.accent03)
            {
                item.GetComponent<SpriteRenderer>().color = colorVar[idx].accent03;
            }
            else if(item.text01)
            {
                item.GetComponent<TMPro.TMP_Text>().color = colorVar[idx].text01;
            }
            else if(item.text02)
            {
                item.GetComponent<TMPro.TMP_Text>().color = colorVar[idx].text02;
            }
            else if(item.image01)
            {
                item.GetComponent<Image>().color = colorVar[idx].image01;
            }
            else if(item.image02)
            {
                item.GetComponent<Image>().color = colorVar[idx].image02;
            }
            // else if(item.sprite01)
            // {
            //     item.GetComponent<SpriteRenderer>().sprite = colorVar[idx].sprite01;
            // }
            // else if(item.sprite02)
            // {
            //     item.GetComponent<SpriteRenderer>().sprite = colorVar[idx].sprite02;
            // }
            // else if(item.sprite03)
            // {
            //     item.GetComponent<SpriteRenderer>().sprite = colorVar[idx].sprite03;
            //}
        }
    }
}

[System.Serializable]
public class ColorVar
{
    public string ThemeName;
    public Color color01;
    public Color color02;
    public Color accent01;
    public Color accent02;
    public Color accent03;

    public Color text01;
    public Color text02;
    public Color image01;
    public Color image02;

    // public Sprite sprite01;
    // public Sprite sprite02;
    // public Sprite sprite03;
}
