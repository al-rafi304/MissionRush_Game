using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public RunnerScript playerScript;
    public AudioManager audioManager;
    public DatabaseManager database;
    public AudioMixer mixerBG;
    public AudioMixer mixerSFX;

    public GameObject pauseMenu;
    public GameObject loginScreen;
    public GameObject registerScreen;
    public GameObject reward;

    public Toggle musicToggle;
    public Toggle sfxToggle;
    public Toggle postProssesingToggle;

    public GameObject postProcessing;

    public Animator uiAnime;
    public static bool isPaused;
    private float vol;

    private void Start() 
    {
        uiAnime = GetComponent<Animator>();   

        if(PlayerPrefs.GetInt("musicToggle", 0) == 0) musicToggle.isOn = true;  // 0 = true, 1 = false
        else musicToggle.isOn = false;

        if(PlayerPrefs.GetInt("sfxToggle", 0) == 0) sfxToggle.isOn = true;
        else sfxToggle.isOn = false;

        if(PlayerPrefs.GetInt("ppToggle", 0) == 0) postProssesingToggle.isOn = true;
        else postProssesingToggle.isOn = false;

        if(PlayerPrefs.GetInt("reward", 0) == 1) reward.SetActive(true);
        else reward.SetActive(false);
    }

    private void Update()
    {
        // if(musicToggle.isOn == false){ 
        //     PlayerPrefs.SetInt("musicToggle", 1); 
        //     Debug.Log("MusicToggled" + PlayerPrefs.GetInt("musicToggle", 0));
        // }
        // else PlayerPrefs.SetInt("musicToggle", 0);

        // if(sfxToggle.isOn == false) PlayerPrefs.SetInt("sfxToggle", 1);
        // else PlayerPrefs.SetInt("sfxToggle", 0);

        // if(sfxToggle.isOn == false) PlayerPrefs.SetInt("ppToggle", 1);
        // else PlayerPrefs.SetInt("ppToggle", 0);
    }
    public void Play()
    {
        playerScript.isRunning = true;
        if(PlayerPrefs.GetInt("reward", 0) == 1)
        {
            GlobalVariable.BoostBool = true;
            PlayerPrefs.SetInt("reward", 0);
        }
        uiAnime.SetBool("Play", true);
        
    }
    public void Pause()
    {
        if(isPaused)
        {
            Time.timeScale = GameManagerScript.timeScale;
            isPaused = false;
            mixerBG.SetFloat("Volume", vol);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseMenu.SetActive(true);
            mixerBG.SetFloat("Volume", -80);
        }
    }

    public void Shop()
    {
        uiAnime.SetBool("EnterShop", true);
    }

    public void Settings()
    {
        uiAnime.SetBool("EnterSettings", true);
    }

    public void Back()
    {
        if(uiAnime.GetBool("EnterShop") == true)
        {
            uiAnime.SetBool("EnterShop", false);
        }
        else if(uiAnime.GetBool("EnterSettings") == true)
        {
            uiAnime.SetBool("EnterSettings", false);
        }
    }

    public void SetVolume(float volume)
    {
        vol = volume;
        mixerBG.SetFloat("Volume", volume);
    }

    public void SetQuallity(int Index)
    {
        QualitySettings.SetQualityLevel(Index);
    }

    public void LoginScreen()
    {
        loginScreen.SetActive(true);
        registerScreen.SetActive(false);
    }
    public void RegisterScreen()
    {
        registerScreen.SetActive(true);
        loginScreen.SetActive(false);
    }

    public void Close(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void Open(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void MusicToggle(bool boolien)
    {

        if(boolien == false)
        {
            PlayerPrefs.SetInt("musicToggle", 1);
            mixerBG.SetFloat("Volume", -80);
        }
        else
        {
            PlayerPrefs.SetInt("musicToggle", 0);
            mixerBG.SetFloat("Volume", 0);;
        }
    }
    public void SFXToggle(bool Boolien)
    {

        if(Boolien == false)
        {
            PlayerPrefs.SetInt("sfxToggle", 1);
            mixerSFX.SetFloat("Volume", -80);
        }
        else
        {
            PlayerPrefs.SetInt("sfxToggle", 0);
            mixerSFX.SetFloat("Volume", 0);
        }
    }
    public void ppToggle(bool Boolien)
    {
        postProcessing.SetActive(Boolien);

        if(Boolien == false)
        {
            PlayerPrefs.SetInt("ppToggle", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ppToggle", 0);
        }
    }


    public void RefreshLeaderboard()
    {
        StartCoroutine(database.LoadLeaderboard());
    }

}
