using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDownUfoBoss : MoveDownUFO                                  // INHERITANCE
{
    int healthUfoBoss;

    void Start()
    {
        if (GameManager.GM.gameIsStarted)
            player = GameObject.Find("Player");

        objectRB = GetComponent<Rigidbody>();
        healthBar = gameObject.transform.GetComponentInChildren<Slider>();
        healthUfoBoss = GameManager.GM.healthUfoBoss;
        healthBar.maxValue = healthUfoBoss;
        healthBar.value = healthUfoBoss;
        totalBoom = gameObject.transform.GetChild(3).gameObject;
        totalBoomPS = totalBoom.GetComponent<ParticleSystem>();
        totalBoomPS.Pause();
        smallBoom = gameObject.transform.GetChild(4).gameObject;
        smallBoomPS = smallBoom.GetComponent<ParticleSystem>();
        smallBoomPS.Pause();
        if (!GameManager.GM.gameIsStarted)
            healthBar.gameObject.SetActive(false);

    }
    private void Update()
    {
        if (!gameObject.CompareTag("SafeSpace"))
            if (GameManager.GM.gameIsStarted && !GameManager.GM.playerDead && !GameManager.GM.gameWon)
            {
                ObstacleBehaviour();                        //ABSTRACTION
                DamageControl();                            //ABSTRACTION
            }
    }
    public override void ObstacleBehaviour()                //ABSTRACTION
    {                                                           // POLYMORPHISM
        if (GameManager.GM.bossAlive)
        {
            Vector3 lookDirection = player.transform.position - transform.position;
            objectRB.AddForce(lookDirection.normalized * GameManager.GM.speedRB * 325 * Time.deltaTime, ForceMode.Force);
            if (transform.position.z < GameManager.GM.zDestroyUfo)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, GameManager.GM.zDestroyUfo);
            }
            if (transform.position.x < -20)
            {
                transform.position = new Vector3(-20, transform.position.y, transform.position.z);
            }
            if (transform.position.x > 20)
            {
                transform.position = new Vector3(20, transform.position.y, transform.position.z);
            }
        }
    }
    public override void DamageControl()                                            //ABSTRACTION
    {                                                                   // POLYMORPHISM
        if (GameManager.GM.gotBomb)
        {
            smallBoomPS.Play();
            healthUfoBoss -= 1;
            healthBar.value = healthUfoBoss;
            GameManager.GM.gotBomb = false;
            if (healthUfoBoss == 0)
            {
                totalBoomPS.Play();
                GameManager.GM.score += 3 * GameManager.GM.diffMultiplier;
                GameManager.GM.bossAlive = false;
                GameManager.GM.bossKillCounter += 1;
                GameManager.GM.ufoKillCounter += 1;
                Destroy(gameObject, 2f);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            Destroy(collision.gameObject);
        }
        Vector3 lookDirection = player.transform.position - transform.position;
        if (collision.gameObject.CompareTag("Player"))
        {
            objectRB.AddForce(lookDirection.normalized * GameManager.GM.speedRB * -325 * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
