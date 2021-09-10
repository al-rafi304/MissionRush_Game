using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public GameObject Player;
    public DatabaseManager database;
    public SkinManager skinManager;
    public GameObject deathScene;
    public Camera cam;
    public AdsManager adsManager;

    private int currentPoints;
    public int totalPoints;
    private int lastLevelScore;
    public int highScore;
    private int score;
    private int deathCount;

    public Text PointsUI;
    public Text ScoreUI;
    public Text highScoreUI;
    public TMP_Text highScoreStartUi;
    public Text totalPointsUI;

    public static float timeScale;

    int startPosition;
    int currentPosition;

    private void Start() 
    {
        Application.targetFrameRate = 120;
        Time.timeScale = 1;
        currentPoints = 0;
        //PlayerPrefs.SetInt("totalPoints", 100000);
        totalPoints = PlayerPrefs.GetInt("totalPoints",0);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        //database.SetInt("HighScore", highScore);

        startPosition = (int) Player.transform.position.x;

        deathCount = PlayerPrefs.GetInt("deathCount", 0);

        Debug.Log(totalPoints);
    }

    public void ScorePoints(int coinValue)
    {
        currentPoints += coinValue;
        
    }

    public void SavePoints(int value)
    {
        PlayerPrefs.SetInt("totalPoints", value);
        totalPoints = PlayerPrefs.GetInt("totalPoints", 0);
        totalPointsUI.text = totalPoints.ToString();
    }

    public void ApplyPointsChange()
    {
        //totalPoints = PlayerPrefs.GetInt("totalPoints",0);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void SaveTotalPoints()
    {
        totalPoints = currentPoints + totalPoints;
        //PlayerPrefs.SetInt("totalPoints", totalPoints);
        database.SetInt("totalPoints", totalPoints);

        if(highScore < score)
        {
            highScore = score;
            //PlayerPrefs.SetInt("HighScore", highScore);
            database.SetInt("HighScore", highScore);
            database.SetInt("HighScoreSkin", PlayerPrefs.GetInt(skinManager.skinIndexPP, 0));
            Debug.Log("Highscore Set");
        }
        Debug.Log(totalPoints);
    }

    public void Die()
    {
        deathCount += 1;
        PlayerPrefs.SetInt("deathCount", deathCount);

        Debug.Log("DIE");
        SaveTotalPoints();
        if(deathCount > 2)
        {
            adsManager.showVideoAd();
            deathCount = 0;
            PlayerPrefs.SetInt("deathCount", deathCount);
        }

        deathScene.SetActive(true);
        FindObjectOfType<AudioManager>().Mute();
        //SceneManager.LoadScene(0);
        //Time.timeScale = 1;
        Time.timeScale = 0;
    }

    public void DiebyCam()
    {
        RunnerScript playerScript = Player.GetComponent<RunnerScript>();
        playerScript.playerRB.position = new Vector2(playerScript.playerRB.position.x + 6, playerScript.playerRB.position.y);
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void Respawn()
    {
        RunnerScript playerScript = Player.GetComponent<RunnerScript>();
        Vector2 viewPos = cam.WorldToViewportPoint(Player.transform.position);

        if(viewPos.x < 0.1 || viewPos.x > 1)
        {
            playerScript.playerRB.position = new Vector2(playerScript.playerRB.position.x + 6, playerScript.playerRB.position.y);
        }

        GlobalVariable.BoostBool = true;
        playerScript.isRunning = true;
        playerScript.isDead = false;
        deathScene.SetActive(false);

        
    }

    void Update() 
    {
        totalPoints = PlayerPrefs.GetInt("totalPoints",0);
        PointsUI.text = currentPoints.ToString();
        highScoreUI.text = "Highscore: " + highScore.ToString();
        highScoreStartUi.text = "Highscore: " + highScore.ToString();
        totalPointsUI.text = PlayerPrefs.GetInt("totalPoints", 0).ToString();

        currentPosition = (int) Player.transform.position.x;

        if(score > 0 && !MenuScript.isPaused) 
        {
            timeScale = Time.timeScale += (float) 0.002 * Time.deltaTime;
            //Debug.Log(Time.timeScale);
        }

        if(Time.timeScale >= 2)
        {
            Time.timeScale = 2;
        }

        //Mathf.Clamp(Time.timeScale, 1f, 1.1f);
        score = (currentPosition - startPosition) * 2;
        ScoreUI.text = score.ToString();
        //Debug.Log((currentPosition - startPosition) * 2);
    }
}


