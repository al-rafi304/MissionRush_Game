using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase;
using Firebase.Database;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public FirebaseManager firebaseManager;
    public LeaderboardManager leaderboard;
    public GameManagerScript gameManager;

    IEnumerator Start()
    {
        yield return new WaitUntil(()=>firebaseManager.auth != null);
        //StartCoroutine(LoadDB_Value("HighScore"));
    }

    void Update()
    {
        
    }

    public IEnumerator LoadLeaderboard()
    {
        var DBTask = firebaseManager.DBreference.Child("users").OrderByChild("HighScore").GetValueAsync();
        yield return new WaitUntil(predicate: ()=> DBTask.IsCompleted);
        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Falied to register task with {DBTask.Exception}");
            leaderboard.warningText.text = "Falided to load leaderboard";
        }
        else if(DBTask.Result.Value == null)
        {
            Debug.Log("No value");
            leaderboard.warningText.text = "No Players Found";
        }
        else
        {
            leaderboard.warningText.text = "";
            //StartCoroutine(LoadDB_Value("HighScore"));
            SetInt("HighScore", PlayerPrefs.GetInt("HighScore", 0));

            leaderboard.DestroyChilds();

            DataSnapshot snapshot = DBTask.Result;
            int place = 1;
            foreach(DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string pName = childSnapshot.Child("Username").Value.ToString();
                string points = childSnapshot.Child("HighScore").Value.ToString();
                string skinIdx = childSnapshot.Child("HighScoreSkin").Value.ToString();

                if(firebaseManager.auth.CurrentUser.DisplayName == pName)
                {
                    leaderboard.LoadSelfPosition(pName, place.ToString(), points, int.Parse(skinIdx));
                }

                leaderboard.LoadPlayer(pName, place.ToString(), points, int.Parse(skinIdx));

                place += 1;
            }

            
            
        }

    }

    public void SetInt(string _key, int _value)
    {
        PlayerPrefs.SetInt(_key, _value);
        Debug.Log("PlayerPrefs Added");
        if(firebaseManager.auth.CurrentUser != null) StartCoroutine(UpdateDB_Int(_key, _value));
        else return;
    }
    public void SetString(string _key, string _value)
    {
        PlayerPrefs.SetString(_key, _value);
        Debug.Log("PlayerPrefs Added");
        StartCoroutine(UpdateDB_String(_key, _value));
    }

    public string GetString(string _key, string _defaultValue)
    {
        string value = "";
        StartCoroutine(LoadDB_Value(_key));
        return value;
    }

    public IEnumerator LoadDB_Value(string _key)
    {
        var DBTask = firebaseManager.DBreference.Child("users").Child(firebaseManager.auth.CurrentUser.UserId).Child(_key).GetValueAsync();

        yield return new WaitUntil(predicate: ()=> DBTask.IsCompleted);
        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Falied to register task with {DBTask.Exception}");
        }
        else if(DBTask.Result.Value == null)
        {
            Debug.Log("No value");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            string value = snapshot.Value.ToString();
            PlayerPrefs.SetInt(_key, int.Parse(value));
            gameManager.ApplyPointsChange();

            Debug.Log(snapshot.Value.ToString());
            
        }
    }

    public IEnumerator UpdateDB_Int(string _key, int _value)
    {
        var DBTask = firebaseManager.DBreference.Child("users").Child(firebaseManager.auth.CurrentUser.UserId).Child(_key).SetValueAsync(_value);

        yield return new WaitUntil(predicate: ()=> DBTask.IsCompleted);
        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Falied to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("DB Updated !");
        }
    }
    public IEnumerator UpdateDB_String(string _key, string _value)
    {
        var DBTask = firebaseManager.DBreference.Child("users").Child(firebaseManager.auth.CurrentUser.UserId).Child(_key).SetValueAsync(_value);

        yield return new WaitUntil(predicate: ()=> DBTask.IsCompleted);
        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Falied to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("DB Updated !");
        }
    }
    
    public IEnumerator CreateDatabase(string userID, string userName)
    {
        var DBTask = firebaseManager.DBreference.Child("users").Child(userID).Child("Username").SetValueAsync(userName);

        yield return new WaitUntil(predicate: ()=> DBTask.IsCompleted);
        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Falied to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("DB Updated !");
        }
    }
}
