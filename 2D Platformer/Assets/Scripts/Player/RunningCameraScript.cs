using UnityEngine;

public class RunningCameraScript : MonoBehaviour
{
    public RunnerScript playerScript;
    public GameManagerScript gameManagerScript;
    public Transform player;
    private Camera cam;
    private Rigidbody2D cameraRB;

    private bool zoomOut;


    void Start()
    {
        RunnerScript playerScript = GetComponent<RunnerScript>();
        cam = GetComponent<Camera>();
        cameraRB = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        Vector2 viewPos = cam.WorldToViewportPoint(player.position);

        if((viewPos.x < -0.01 || viewPos.x > 1) && !playerScript.isDead)
        {
            gameManagerScript.Die();
            playerScript.isRunning = false;
            playerScript.isDead = true;
            playerScript.playerRB.velocity = new Vector2(0, 0);
            //playerScript.playerRB.transform.position = new Vector2(playerScript.playerRB.transform.position.x + 5, playerScript.playerRB.transform.position.y);
        }

        if (playerScript.isRunning)
        {
            //transform.Translate(Vector2.right * 9 * Time.deltaTime);
            cameraRB.velocity = new Vector2(playerScript.speed, cameraRB.velocity.y);

        }
        else if(!playerScript.isRunning)
        {
            cameraRB.velocity = new Vector2(0,0);
        }


        if(playerScript.moveCam) 
        {
            //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 30, 0.1f);
            cam.transform.position = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, -83, 0.1f), cam.transform.position.z);
        }
        else if( playerScript.moveCam == false)
        {
            //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 12, 0.1f);
            cam.transform.position = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, -69.5f, 0.1f), cam.transform.position.z);
        }
    }
}
