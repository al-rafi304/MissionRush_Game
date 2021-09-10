using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonLocker : MonoBehaviour
{
    private ThemeSystem themeScript;
    private DatabaseManager database;
    private SkinManager skinManager;
    private GameManagerScript gameManager;
    private Button lockedButton;
    public Button keyButton;
    public Image lockedButtonImage;
    public TMP_Text priceText;

    private bool isLocked;
    private string ppID;

    public int id;
    public int costToUnlock;

    public bool isTheme;
    public bool isSkin;


    void Awake() 
    {
        //ppID = "button" + id;
    }

    void Start()
    {
        if(isTheme) ppID = "button" + id;
        else if(isSkin) ppID = "button2" + id;

        themeScript = FindObjectOfType<ThemeSystem>();
        skinManager = FindObjectOfType<SkinManager>();
        gameManager = FindObjectOfType<GameManagerScript>();
        database = FindObjectOfType<DatabaseManager>();
        lockedButton = GetComponent<Button>();
        
        priceText.text = costToUnlock.ToString();
        setState();
        keyButton.onClick.AddListener(unlock);
        lockedButton.onClick.AddListener(onClick);
    }

    void Update()
    {
        if(themeScript.themeIndex == id)
        {
            this.GetComponent<Image>().color = Color.green;
        }
    }
    
    void setState()
    {
        if(PlayerPrefs.GetInt(ppID, 0) == 0) isLocked = true;    //1 = false, 0 = true
        else isLocked = false;

        if(isLocked)
        {
            lockedButton.interactable = false;
            keyButton.gameObject.SetActive(true);

            Color tmpColor = lockedButtonImage.color;
            lockedButtonImage.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 0.2f);
        }
        else
        {
            lockedButton.interactable = true;
            keyButton.gameObject.SetActive(false);

            Color tmpColor = lockedButtonImage.color;
            lockedButtonImage.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 1f);
        } 
    }

    void unlock()
    {
        if(gameManager.totalPoints >= costToUnlock)
        {
            FindObjectOfType<AudioManager>().Play("Unlock");

            int totalPoints = gameManager.totalPoints;
            totalPoints -= costToUnlock;
            PlayerPrefs.SetInt("totalPoints", totalPoints);
            database.SetInt("totalPoints", totalPoints);
            
            PlayerPrefs.SetInt(ppID, 1);
            isLocked = false;
            keyButton.gameObject.SetActive(false);
            lockedButton.interactable = true;

            Color tmpColor = lockedButtonImage.color;
            lockedButtonImage.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 1f);
        }
    }

    void onClick()
    {
        //themeScript.changeTheme(id);
        //Debug.Log("Clicked" + id);

        if(isTheme){
            themeScript.changeTheme(id);
        }
        else if(isSkin){
            skinManager.changeSkin(id);
            Debug.Log("Clicked Button: " + id);
        }
        
    }
    public void manualChange(int id)
    {
        themeScript.changeTheme(id);
    }
}
