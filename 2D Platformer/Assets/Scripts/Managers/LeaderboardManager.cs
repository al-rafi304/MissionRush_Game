using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    private SkinManager skinManager;
    public GameObject parent;
    public GameObject playerObject;
    public TMP_Text warningText;

    [Header("Self Data")]
    public TMP_Text playerName;
    private string orderNumber;
    public TMP_Text points;
    public Image playerSprite;

    private LeaderboardPlayer leaderboardPlayer;

    void Start()
    {
        skinManager = FindObjectOfType<SkinManager>();
    }

    void Update()
    {
        
    }

    public void LoadPlayer(string _name, string _place, string _points, int _skinIdx)
    {
        var player = Instantiate(playerObject, parent.transform);
        leaderboardPlayer = player.GetComponent<LeaderboardPlayer>();
        leaderboardPlayer.setData(_name, _place, _points, _skinIdx);
    }

    public void LoadSelfPosition(string _name, string _place, string _points, int skinIdx)
    {
        orderNumber = _place;
        playerName.text = orderNumber + ". " + _name;
        points.text = _points;
        playerSprite.sprite = skinManager.skins[skinIdx].sprite;
    }

    public void DestroyChilds()
    {
        foreach(Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
