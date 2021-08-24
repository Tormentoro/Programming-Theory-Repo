using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private float zBound = 10f;
    private float zNegativeBound = -30f;
    private float xBound = 21f;
    

    private int health;

    private Animator playerAnim;
    private Rigidbody playerRB;
    private Slider healthBar;
    private ParticleSystem dirtPS;
    //private GameObject baloon;    

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
        dirtPS = gameObject.GetComponentInChildren<ParticleSystem>();
        if (gameObject.CompareTag("Player"))
        {
            playerRB = gameObject.GetComponent<Rigidbody>();
            healthBar = gameObject.GetComponentInChildren<Slider>();
            if (GameManager.GM.gameIsStarted)
            {
                playerAnim.SetBool("Game_Start_b", true);
                dirtPS.Play();
            }
            health = GameManager.GM.healthPlayer;
            healthBar.maxValue = health;
            healthBar.value = health;

            ChoosePlayerAppearance();
        }
        else if (gameObject.CompareTag("SafeSpace"))
        {
            MainMenuBehaviour();
        }
    }
    void Update()
    {        
        if (gameObject.CompareTag("Player") && !GameManager.GM.playerDead && GameManager.GM.gameIsStarted && !GameManager.GM.gameWon)
        {
            PlayerMovementSettings();
            PlayBoundsZ();
            PlayBoundsX();
            //if (!GameManager.GM.bossAlive && respondTime > 0.01f)
            //    GameManager.GM.gotBomb = false;
        }
    }
    void PlayerMovementSettings()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime, Space.World);
    }
    void PlayBoundsZ()
    {
        if (transform.position.z < zNegativeBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zNegativeBound);
        }
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }
    void PlayBoundsX()
    {
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Alien") && !GameManager.GM.gameWon)
        {
            health -= 1;
            healthBar.value = health;
        }
        if (collision.gameObject.CompareTag("AlienBoss") && !GameManager.GM.gameWon)
        {
            health -= 1;
            healthBar.value = health;
            Vector3 lookDirection = collision.gameObject.transform.position - transform.position;
            playerRB.AddForce(lookDirection.normalized * GameManager.GM.speedRB * -100 * Time.deltaTime, ForceMode.Impulse);
        }
        if (gameObject.CompareTag("Player") && health == 0 && GameManager.GM.gameIsStarted)
        {
            GameManager.GM.playerDead = true;
            dirtPS.Stop();
            playerAnim.SetBool("Death_b", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PowerUpBomb"))
        {
            health -= 1;
            healthBar.value = health;
            GameManager.GM.gotBomb = true;
            Destroy(other.gameObject);
            if (!GameManager.GM.bossAlive)
            {
                foreach (GameObject ufo in ObjectPooler.PO.pooledUfo)
                {
                    if (ufo.activeInHierarchy)
                    {
                        ufo.GetComponent<MoveDownUFO>().smallBoomPS.Play();
                        ufo.GetComponent<MoveDownUFO>().healthUfo -= 1;
                        ufo.GetComponent<MoveDownUFO>().healthBar.value = ufo.GetComponent<MoveDownUFO>().healthUfo;
                    }               
                }
                GameManager.GM.gotBomb = false;
            }
        }
        if (other.gameObject.CompareTag("PowerUpHealth"))
        {
            GameManager.GM.gotHealth = true;
            if (health < healthBar.maxValue)
            {
                health += 1;
                healthBar.value = health;
            }
            else if (health == healthBar.maxValue)
                GameManager.GM.score += 5;
            if (health > healthBar.maxValue)
            {
                health = GameManager.GM.healthPlayer;
                healthBar.value = health;
            }
            Destroy(other.gameObject);
            GameManager.GM.gotHealth = false;
        }
        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("SafeSpace"))
        {
            GameManager.GM.gameWon = true;
            dirtPS.Stop();
            playerAnim.SetBool("Game_Start_b", false);
            InvokeRepeating("IntroJumpingFolks", 0, 3);
        }
    }
    void IntroJumpingFolks()
    {
        playerAnim.Play("Standing_Jump");
    }
    void ChoosePlayerAppearance()
    {
        if (GameManager.GM.charWrangler)
        {
            Debug.Log("111");
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if (GameManager.GM.charFarmer)
        {
            Debug.Log("222");
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if (GameManager.GM.charWife)
        {
            Debug.Log("333");
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if (GameManager.GM.charDaughter)
        {
            Debug.Log("444");
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
        }
    }
    void MainMenuBehaviour()
    {
        InvokeRepeating("IntroJumpingFolks", 0, 3);
        gameObject.transform.GetChild(5).gameObject.SetActive(false);
        dirtPS.Stop();
        if (GameManager.GM.gameIsStarted)
        {
            if (GameManager.GM.charWrangler)
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
            else if (GameManager.GM.charFarmer)
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
            else if (GameManager.GM.charWife)
                gameObject.transform.GetChild(3).gameObject.SetActive(false);
            else if (GameManager.GM.charDaughter)
                gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }
    }
}

