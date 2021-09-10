using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public FirebaseManager firebaseManager;

    public GameObject loginPage;
    public GameObject registerPage;

    public GameObject loginButton;
    public GameObject logoutButton;
    public GameObject leaderBoardLogIN;
    public GameObject leaderBoardLogOUT;

    public TMP_Text usernameUIText;
    public GameObject playerProfileUi_loggedIn;
    public GameObject playerProfileUi_loggedOut;

    IEnumerator Start()
    {
        yield return new WaitUntil(()=> firebaseManager.auth != null);
        if(firebaseManager.auth.CurrentUser != null)
        {
            SetState(true);
        }
        else
        {
            SetState(false);
        }
    }

    public void SetState(bool isLoggedIn)
    {
        if(isLoggedIn)
        {
            loginButton.SetActive(false);
            logoutButton.SetActive(true);

            leaderBoardLogIN.SetActive(true);
            leaderBoardLogOUT.SetActive(false);

            usernameUIText.text = firebaseManager.auth.CurrentUser.DisplayName.ToString();
            playerProfileUi_loggedIn.SetActive(true);
            playerProfileUi_loggedOut.SetActive(false);
        }
        else
        {
            loginButton.SetActive(true);
            logoutButton.SetActive(false);

            leaderBoardLogIN.SetActive(false);
            leaderBoardLogOUT.SetActive(true);

            usernameUIText.text = "";
            playerProfileUi_loggedIn.SetActive(false);
            playerProfileUi_loggedOut.SetActive(true);
        }
    }

    public void LogIN()
    {
        firebaseManager.LogInButton();
        SetState(true);
    }
    public void register()
    {
        firebaseManager.RegisterButton();
    }
    public void LogOUT()
    {
        firebaseManager.Logout();
        SetState(false);
    }

    public void ShowLoginPage()
    {
        loginPage.SetActive(true);
    }
    public void CloseLoginPage()
    {
        loginPage.SetActive(false);
    }

    public void ShowRegisterPage()
    {
        registerPage.SetActive(true);
    }
    public void CloseRegisterPage()
    {
        registerPage.SetActive(false);
    }
}
