using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerScript : MonoBehaviour
{
    private RunnerScript playerScript;

    //public bool keyboardInput;
    //public bool mobileInput;


    public enum inputs
    {
        KeyboardInput,
        MobileInput
    };

    [SerializeField]private inputs currentInput;

    [HideInInspector]
    public bool upInput;
    [HideInInspector]
    public bool downInput;

    private Touch touch;
    private Vector2 touchStartPos, touchEndPos;


    private void Start() 
    {
        playerScript = GetComponent<RunnerScript>();   

        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            currentInput = inputs.MobileInput;
            //Debug.Log(SystemInfo.deviceType);
        }
        else
        {
            currentInput = inputs.KeyboardInput;
            //Debug.Log(SystemInfo.deviceType);
        }
    }

    private void Update() 
    {
        //if(keyboardInput) GetKeyboardInput();
        //if(mobileInput) GetTouchInput();

        if(currentInput == inputs.KeyboardInput) GetKeyboardInput();
        if(currentInput == inputs.MobileInput) GetTouchInput();

        
    }

    public void GetKeyboardInput()
    {
        if(Input.GetKeyDown(KeyCode.Space)) upInput = true;
        else upInput = false;
        
        if(Input.GetKeyDown(KeyCode.LeftControl)) downInput = true;
        else downInput = false;
    }

    public void GetTouchInput()
    {
        if(Input.touchCount > 0 )
        {
            touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                touchEndPos = touch.position;

                float y = touchEndPos.y - touchStartPos.y;

                //Debug.Log(touch.phase);
                
                if(y > 0)
                {
                    upInput = true;
                } 
                else if(y < 0)
                {
                    downInput = true;
                }
            }
            
        }
        else
        {
            upInput = false;
            downInput = false;
        }
    }
}
