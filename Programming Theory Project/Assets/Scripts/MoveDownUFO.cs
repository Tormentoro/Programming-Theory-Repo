using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveDownUFO : MoveDownObstacles                            // INHERITANCE
{
    protected GameObject player;
    [SerializeField] protected Rigidbody objectRB;
    public Slider healthBar;
    [SerializeField] protected GameObject totalBoom;
    [SerializeField] protected GameObject smallBoom;
    [SerializeField] protected ParticleSystem totalBoomPS;
    public ParticleSystem smallBoomPS;

    public int healthUfo;
    public bool counted;

    public bool isAimed;
    public bool ufoDown;

    protected Vector3 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.GM.gameIsStarted)
            player = GameObject.FindGameObjectWithTag("Player");
        
        totalBoomPS.Pause();
        smallBoomPS.Pause();
        healthUfo = GameManager.GM.healthUfo;
        healthBar.maxValue = healthUfo;
        healthBar.value = healthUfo;
        if (!GameManager.GM.gameIsStarted)
            healthBar.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (GameManager.GM.gameIsStarted)
        {
            lookDirection = player.transform.position - transform.position;
            ObstacleBehaviour();                                                //ABSTRACTION
            DamageControl();                                                    //ABSTRACTION
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!gameObject.CompareTag("SafeSpace"))
        {
            if (collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Alien"))
            {
                healthUfo -= 1;
                healthBar.value = healthUfo;
                if (lookDirection.magnitude < GameManager.GM.scoreLimit)
                {
                    GameManager.GM.score += 1 * GameManager.GM.diffMultiplier;
                }
                objectRB.AddForce(lookDirection.normalized * GameManager.GM.speedRB * -200 * Time.deltaTime, ForceMode.Impulse);
            }
            if (collision.gameObject.CompareTag("AlienBoss"))
            {
                if (counted)
                    GameManager.GM.ufoList.Remove(this.gameObject);
                totalBoomPS.Play();
                healthUfo = 0;
                healthBar.value = healthUfo;
                gameObject.SetActive(false); 
                ResetUfoParameters();                                           //ABSTRACTION
                Debug.Log("Parameters resetted");
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                objectRB.AddForce(lookDirection.normalized * GameManager.GM.speedRB * -200 * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }
    public override void ObstacleBehaviour()                                    //ABSTRACTION
    {                                                                           // POLYMORPHISM
        lookDirection = player.transform.position - transform.position;
        if ((lookDirection.magnitude < 20 || isAimed) && healthUfo > 0)
        {
            isAimed = true;
            objectRB.AddForce(lookDirection.normalized * GameManager.GM.speedRB * 200 * Time.deltaTime, ForceMode.Force);

            if (transform.position.z < GameManager.GM.zDestroyUfo)
            {
                gameObject.SetActive(false);    
                ResetUfoParameters();                                               //ABSTRACTION
                Debug.Log("Parameters resetted");
            }
        }
        else
        {
            base.ObstacleBehaviour();                                                   //ABSTRACTION
        }
    }
    public virtual void DamageControl()                                             // POLYMORPHISM
    {
        if (healthUfo < 0)
            healthUfo = 0;
        if (healthUfo == 0 && !ufoDown)
        {
            if (counted)
                GameManager.GM.ufoList.Remove(this.gameObject);
            ufoDown = true;
            totalBoomPS.Play();
            if (lookDirection.magnitude <= GameManager.GM.scoreLimit)
            {
                GameManager.GM.score += 1 * GameManager.GM.diffMultiplier;
                GameManager.GM.ufoKillCounter += 1;
            }
            gameObject.SetActive(false);                                           
            ResetUfoParameters();                                                           //ABSTRACTION
            Debug.Log("Parameters resetted");
        }
    }
    private void ResetUfoParameters()                                                       //ABSTRACTION
    {
        healthUfo = GameManager.GM.healthUfo;
        healthBar.value = GameManager.GM.healthUfo;
        objectRB.velocity = Vector3.zero;
        isAimed = false;
        counted = false;
        ufoDown = false;
    }
}
