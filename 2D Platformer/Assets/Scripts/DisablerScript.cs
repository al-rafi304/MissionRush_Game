using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablerScript : MonoBehaviour
{
    private void OnEnable() 
    {
        RaycastHit2D hit2D_Start = Physics2D.Raycast(transform.position, Vector2.down, 50, 13);
        RaycastHit2D hit2D_End = Physics2D.Raycast(transform.Find("EndPosition").position, Vector2.down, 50, 13);

        Debug.DrawRay(transform.Find("EndPosition").position, Vector2.down * 10, Color.red, 20f);
        Debug.DrawRay(transform.position, Vector2.down * 10, Color.red, 20f);

        if(hit2D_Start || hit2D_End)
        {
            // Debug.Log(this.name);
            gameObject.SetActive(false);
        }
        else if(!hit2D_Start && !hit2D_End)
            gameObject.SetActive(true);
    }
    
    // private void OnDisable() 
    // {
    //     Destroy(this.gameObject);
    // }
}
