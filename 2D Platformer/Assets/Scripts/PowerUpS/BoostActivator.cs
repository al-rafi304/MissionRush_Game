using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostActivator : MonoBehaviour
{
    public SpawnerScript spawnerScript;
    private RunnerScript playerScript;
    public PowerUpManager powerUpManager;
    private Animator boostAnim;

    public float boostSpeed;
    private float regularSpeed;
    private float timer;

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    void Start()
    {
        playerScript = GetComponent<RunnerScript>();
        boostAnim = GetComponent<Animator>();
        regularSpeed = playerScript.speed;
    }

    void Update()
    {
        Vector2 ViewToPlayerRatio;
        ViewToPlayerRatio = Camera.main.WorldToViewportPoint(this.transform.position);

        if(GlobalVariable.BoostBool)
        {
            timer += Time.deltaTime;
            Physics2D.IgnoreLayerCollision(11, 12, true);
            playerScript.speed = boostSpeed;
            
            if(timer >= (powerUpManager.boostTime - (powerUpManager.boostTime * 0.3f))) //timer >= GlobalVariable.bTime - 3)
            {
                playerScript.speed = regularSpeed;
                boostAnim.SetBool("boostEnding", true);
            }
        }
        else
        {
            timer = 0;

            playerScript.speed = regularSpeed;
            Physics2D.IgnoreLayerCollision(11, 12, false);

            boostAnim.SetBool("boostEnding", false);
        }

        
    }
}
