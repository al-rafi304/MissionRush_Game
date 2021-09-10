using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;

public class FirebaseManager : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference DBreference;
    public DatabaseManager databaseManager;
    //private FirebaseAuthState authState;

    [Header("Ui Management")]
    public SettingsMenu settingsMenu;
    //public TMP_Text userNameUi;
    
    public TMP_Text welcomeText;
    //public DatabaseManager databaseManager;

    [Header("Login")]
    public TMP_InputField email_loginInput;
    public TMP_InputField password_loginInput;
    public TMP_Text warningLoginText;

    [Header("Register")]
    public TMP_InputField email_registerInput;
    public TMP_InputField password_registerInput;
    public TMP_InputField username_input;
    public TMP_Text warningRegisterText;


    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus =  task.Result;
            if(dependencyStatus == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                DBreference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Setting up Firebase Auth");
                //CheckAuthState();     cause crashes
            }
            else
            {
                Debug.Log("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Tab))
        // {
        //     checkStatus();
        // }
    }

    void checkStatus()
    {
        if(auth.CurrentUser != null)
        {
            Debug.Log("Have User: " + auth.CurrentUser.DisplayName);
        }
        else
        {
            Debug.Log("No user ");
        }
    }

    // public void SetInt(string _key, int _value)
    // {
    //     PlayerPrefs.SetInt(_key, _value);
    //     Debug.Log("PlayerPrefs Added");
    //     StartCoroutine(UpdateDB_Int(_key, _value));
    // }
    // public void SetString(string _key, string _value)
    // {
    //     PlayerPrefs.SetString(_key, _value);
    //     Debug.Log("PlayerPrefs Added");
    //     StartCoroutine(UpdateDB_String(_key, _value));
    // }

    // public string GetString(string _key, string _defaultValue)
    // {
    //     string value = "";
    //     StartCoroutine(LoadDB_Value(_key, value));
    //     return value;
    // }

    // private IEnumerator LoadDB_Value(string _key, string _valStorage)
    // {
    //     var DBTask = DBreference.Child("users").Child(auth.CurrentUser.UserId).Child(_key).GetValueAsync();

    //     yield return new WaitUntil(predicate: ()=> DBTask.IsCompleted);
    //     if(DBTask.Exception != null)
    //     {
    //         Debug.LogWarning(message: $"Falied to register task with {DBTask.Exception}");
    //     }
    //     else if(DBTask.Result.Value == null)
    //     {
    //         Debug.Log("No value");
    //     }
    //     else
    //     {
    //         DataSnapshot snapshot = DBTask.Result;

    //         _valStorage = "";
    //         _valStorage = snapshot.Value.ToString();
            
    //     }
    // }

    // private IEnumerator UpdateDB_Int(string _key, int _value)
    // {
    //     var DBTask = DBreference.Child("users").Child(auth.CurrentUser.UserId).Child(_key).SetValueAsync(_value);

    //     yield return new WaitUntil(predicate: ()=> DBTask.IsCompleted);
    //     if(DBTask.Exception != null)
    //     {
    //         Debug.LogWarning(message: $"Falied to register task with {DBTask.Exception}");
    //     }
    //     else
    //     {
    //         Debug.Log("DB Updated !");
    //     }
    // }
    // private IEnumerator UpdateDB_String(string _key, string _value)
    // {
    //     var DBTask = DBreference.Child("users").Child(auth.CurrentUser.UserId).Child(_key).SetValueAsync(_value);

    //     yield return new WaitUntil(predicate: ()=> DBTask.IsCompleted);
    //     if(DBTask.Exception != null)
    //     {
    //         Debug.LogWarning(message: $"Falied to register task with {DBTask.Exception}");
    //     }
    //     else
    //     {
    //         Debug.Log("DB Updated !");
    //     }
    // }

    void AuthStateChanged(object sender, System.EventArgs eventArgs) 
    {
        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null) {
                Debug.Log("Signed out " + user.UserId);

                Debug.Log("No Current user");
                settingsMenu.SetState(false);
                welcomeText.text = "Please Login to save progress";
            }
            user = auth.CurrentUser;
            if (signedIn) {
                Debug.Log("Signed in " + user.UserId);

                Debug.Log("Already Logged in: " + auth.CurrentUser.DisplayName);
                settingsMenu.SetState(true);
                welcomeText.text = "Welcome" + auth.CurrentUser.DisplayName.ToString();

            }
        }
    }
    public void LogInButton()
    {
        StartCoroutine(Login(email_loginInput.text, password_loginInput.text));
    }
    public void RegisterButton()
    {
        StartCoroutine(Register(email_registerInput.text, password_registerInput.text, username_input.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        warningLoginText.text = "Logging in...";
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            user = LoginTask.Result;

            StartCoroutine(databaseManager.LoadDB_Value("HighScore"));
            StartCoroutine(databaseManager.LoadDB_Value("totalPoints"));

            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.Email);
            warningLoginText.text = "";
            warningLoginText.text = "Logged In";

            CheckAuthState();

            ClearLoginInputs();

            // authState.CheckUser();
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        // else if(passwordRegisterField.text != passwordRegisterVerifyField.text)
        // {
        //     //If the password does not match show a warning
        //     warningRegisterText.text = "Password Does Not Match!";
        // }
        else 
        {
            warningRegisterText.text = "Creating Account...";
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                user = RegisterTask.Result;

                if (user != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = user.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        // UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                        warningRegisterText.text = "Account Created ! Login using Email & Password";

                        ClearRegisterInputs();
                        StartCoroutine(databaseManager.CreateDatabase(auth.CurrentUser.UserId, auth.CurrentUser.DisplayName));
                        StartCoroutine(databaseManager.UpdateDB_Int("HighScore", PlayerPrefs.GetInt("HighScore", 0)));
                        StartCoroutine(databaseManager.UpdateDB_Int("totalPoints", PlayerPrefs.GetInt("totalPoints", 0)));
                        StartCoroutine(databaseManager.UpdateDB_Int("HighScoreSkin", PlayerPrefs.GetInt("skinIndex", 0)));

                        // authState.CheckUser();
                    }
                }
            }
        }
    }

    private void ClearLoginInputs()
    {
        email_loginInput.text = "";
        password_loginInput.text = "";
    }
    private void ClearRegisterInputs()
    {
        email_registerInput.text = "";
        password_registerInput.text = "";
        username_input.text = "";
    }

    public void CheckAuthState()
    {
        if(auth.CurrentUser != null)
        {
            Debug.Log("Already Logged in: " + auth.CurrentUser.DisplayName);
            settingsMenu.SetState(true);
            welcomeText.text = "Welcome" + auth.CurrentUser.DisplayName.ToString();
        }
        else
        {
            Debug.Log("No Current user");
            settingsMenu.SetState(false);
            welcomeText.text = "Please Login to save progress";
        }
    }

    public void Logout()
    {
        auth.SignOut();
        warningLoginText.text = "";
        warningRegisterText.text = "";
        Debug.Log("Logged Out");
    }
}
