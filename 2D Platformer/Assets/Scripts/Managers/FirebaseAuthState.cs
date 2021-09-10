// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Firebase;
// using Firebase.Auth;
// using System;
// using TMPro;

// public class FirebaseAuthState : MonoBehaviour
// {
//     private FirebaseManager firebaseManager;
//     public TMP_Text username;
//     public GameObject SignInButton;
//     public GameObject SignInScreen;
//     public GameObject SignOutButton;
    
//     void Awake()
//     {
//         CheckUser();
//     }

//     public void CheckUser()
//     {
//         // var currentUser = firebaseManager.auth.CurrentUser;
//         if(firebaseManager.auth.CurrentUser != null)
//         {
//             username.text = firebaseManager.auth.CurrentUser.DisplayName.ToString();
//             SignInButton.SetActive(false);
//             SignInScreen.SetActive(false);
//             SignOutButton.SetActive(true);

//             Debug.Log(firebaseManager.auth.CurrentUser.DisplayName);
//         }
//         else
//         {
//             username.text = "";
//             SignInButton.SetActive(true);
//             SignInScreen.SetActive(true);
//             SignOutButton.SetActive(false);

//             Debug.Log("No one is Logged In");
//         }
//     }

//     public void SignOut()
//     {
//         FirebaseAuth.DefaultInstance.SignOut();
//         CheckUser();
//     }
// }
