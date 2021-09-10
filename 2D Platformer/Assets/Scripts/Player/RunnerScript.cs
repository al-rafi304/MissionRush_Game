using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerScript : MonoBehaviour
{
    private InputManagerScript inputManager;

    public Rigidbody2D playerRB;
    public BoxCollider2D PlayerCollider;
    public GameManagerScript GameManager;
    public Camera cam;
    private TrailRenderer trail;
    public ParticleSystem particle;

    public LayerMask Ceilling;
    public LayerMask Ground;
    public LayerMask Obstacle;

    public float speed;
    public float jumpForce;
    public float CrouchFallForce;
    public float PosResetTime;

    public bool isRunning;
    private bool isCrouching;
    private bool isBehind;
    public bool isDead;
    public bool onWater;
    public bool moveCam;
    

    private float time = 0;
    private float time2 = 0;
    public float CrouchTime;

    public float Buoyancy;

    public Animator CrouchAnime;

    private void Start()
    {
        isRunning = false;
        isCrouching = false;
        isBehind = false;
        isDead = false;
        onWater = false;
        Buoyancy = 3;

        trail = GetComponent<TrailRenderer>();
        inputManager = GetComponent<InputManagerScript>();

    }
    void Update()
    {
        GetInputs();
        speed += 0.08f * Time.deltaTime;

        playerVariables.isPlaying = isRunning;

        if (isRunning == true) 
            Run();

        if (isCrouching == true)
        {
            time = Time.deltaTime + time;
            //Debug.Log(time);
            if (time >= CrouchTime)
            {
                time = 0f;
                CrouchAnime.SetBool("Crouch", false);
                isCrouching = false;

            }
        }

        //Resetting Player Position
        Vector2 ViewToPlayerRatio;
        ViewToPlayerRatio = cam.WorldToViewportPoint(this.transform.position);

        if((ViewToPlayerRatio.x * 100) < 15 && isRunning)
        {
            isBehind = true;
            time2 = time2 + Time.deltaTime;
            //Debug.Log(time2);
        }else time2 = 0;

        if(isBehind && time2 >= PosResetTime)
        {
            playerRB.velocity = new Vector2((speed + 1),playerRB.velocity.y);
            //trail.enabled = true;
            //Debug.Log("RESETING");
            isBehind = false;
        }//else trail.enabled = false;

        
        //

        if(onWater && !particle.isPlaying) 
            particle.Play();
        else if(!onWater && particle.isPlaying)
            particle.Stop();

    }

    private void GetInputs()
    {
        /*if (Input.GetMouseButtonDown(0)) 
            isRunning = true;
        */
        if (inputManager.upInput && (TouchingGround() || TouchingObstacle()) && !onWater )  
            Jump();
        
        if(inputManager.upInput && onWater)
            Float();

        if (inputManager.downInput && isCrouching == false && (!onWater || !moveCam)) 
            Crouch();

        if(inputManager.downInput && (onWater || moveCam))
            Sink();
    }

    public void Run()
    {
        playerRB.velocity = new Vector2(speed, playerRB.velocity.y);
    }

    public void Jump()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce );
        CrouchAnime.SetBool("Crouch", false);
        isCrouching = false;
        time = 0f;
    }

    public void Crouch()
    {
        if (TouchingGround() || TouchingObstacle())
        {
            isCrouching = true;
            CrouchAnime.SetBool("Crouch", isCrouching);

            if (time < CrouchTime) time = 0;
        }

        if (!TouchingGround() || !TouchingObstacle())
        {
            playerRB.AddForce(Vector2.down * CrouchFallForce, ForceMode2D.Impulse);

            isCrouching = true;
            CrouchAnime.SetBool("Crouch", isCrouching);

            if (time < CrouchTime) time = 0;
        }
    }

    public void Sink()
    {
        Buoyancy = 0;
        playerRB.velocity = new Vector2(playerRB.velocity.x, -5 );
        CrouchAnime.SetBool("Crouch", true);
    }
    public void Float()
    {
        Buoyancy = 3;
        playerRB.velocity = new Vector2(playerRB.velocity.x, 5 );
        CrouchAnime.SetBool("Crouch", false);
    }

    private bool TouchingCeilling()
    {
        RaycastHit2D hit = Physics2D.BoxCast(PlayerCollider.bounds.center, PlayerCollider.bounds.size, 0f, Vector2.up, 0.01f, Ceilling);

        return hit.collider != null;
    }

    private bool TouchingGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(PlayerCollider.bounds.center, PlayerCollider.bounds.size, 0f, Vector2.down, 0.5f, Ground);
        
        //Debug.DrawRay(PlayerCollider.bounds.center + new Vector3(PlayerCollider.bounds.extents.x, 0), Vector2.down * (PlayerCollider.bounds.extents.y + 0.01f), Color.red);
        
        return hit.collider != null;
    }
    private bool TouchingObstacle()
    {
        RaycastHit2D hit = Physics2D.BoxCast(PlayerCollider.bounds.center, PlayerCollider.bounds.size, 0f, Vector2.down, 0.5f, Obstacle);
        
        //Debug.DrawRay(PlayerCollider.bounds.center + new Vector3(PlayerCollider.bounds.extents.x, 0), Vector2.down * (PlayerCollider.bounds.extents.y + 0.01f), Color.red);
        
        return hit.collider != null;
    }
    
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Water"))
        {
            onWater = true;
            FindObjectOfType<AudioManager>().underWater = true;
        }
        if(other.CompareTag("WaterArea"))
        {
            moveCam = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Water"))
        {
            onWater = false;
            FindObjectOfType<AudioManager>().underWater = false;
            //particle.Stop();
        }
        if(other.CompareTag("WaterArea"))
        {
            moveCam = false;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Spike") && !GlobalVariable.BoostBool && !isDead)
        {
            isRunning = false;
            GameManager.Die();
            isDead = true;
            // playerRB.velocity = new Vector2(0, 0);
        }
    }
    

}

public static class playerVariables
{
    public static bool isPlaying;
}
