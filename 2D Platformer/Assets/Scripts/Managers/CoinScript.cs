using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private GameObject gmObject;
    private GameObject Player;
    private GameManagerScript gmScript;
    private PowerUpManager pmScript;

    private SpriteRenderer spriteRenderer;
    public Color magnetColor;
    public Color increasedValueColor;
    private Color baseColor;
/*
    public ParticleSystem ParticleEffect;
    private ParticleSystem.MainModule ParticleEffect_Main;
*/
    public int Value;
    private float MagnetRange = 5.0f;
    private float MagnetPower = 15;

    private void Start() 
    {
        gmObject = GameObject.FindWithTag("GameManager");
        gmScript = gmObject.GetComponent<GameManagerScript>();
        Player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        baseColor = spriteRenderer.color;
    }

    private void Update() 
    {
        if(GlobalVariable.magnetBool)
        {
            spriteRenderer.color = magnetColor;
            /*
            ParticleEffect.Play();
            ParticleEffect_Main.startColor = magnetColor;
            */
            if(((Mathf.Abs(gameObject.transform.position.x - Player.transform.position.x)) <= MagnetRange))
            {
                transform.position =  Vector2.MoveTowards(transform.position, Player.transform.position, MagnetPower * Time.deltaTime);
            }
        }
        else if(GlobalVariable.doubelPointsBool)
        {
            spriteRenderer.color = increasedValueColor;
            /*
            ParticleEffect.Play();
            ParticleEffect_Main.startColor = increasedValueColor;
            */
        }
        else
        {
            spriteRenderer.color = baseColor;
            //ParticleEffect.Stop();
        } 

        if(GlobalVariable.BoostBool) MagnetPower = 60;
        else MagnetPower = 15;
        
    }
/*
    void Magnet(bool Active, Color color)
    {
        if(Active)
        {
            spriteRenderer.color = color;
            ParticleEffect_Main.startColor = color;
            if(((Mathf.Abs(gameObject.transform.position.x - Player.transform.position.x)) <= MagnetRange))
            {
                transform.position =  Vector2.MoveTowards(transform.position, Player.transform.position, MagnetPower * Time.deltaTime);
            }
        }
        else
            spriteRenderer.color = baseColor;
    }
*/


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(GlobalVariable.doubelPointsBool)
            {
                gmScript.ScorePoints(Value * 2);
            }else
            {
                gmScript.ScorePoints(Value);
            }
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Coin");
        }
    }
    
}
