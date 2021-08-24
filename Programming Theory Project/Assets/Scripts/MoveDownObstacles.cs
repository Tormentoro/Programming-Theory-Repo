using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownObstacles : MonoBehaviour                              // INHERITANCE
{
    private float zDestroy = -36;

    void Update()
    {
        if (!GameManager.GM.playerDead && !GameManager.GM.gameWon)
        {
            ObstacleBehaviour();                                            //ABSTRACTION
        }
    }
    public virtual void ObstacleBehaviour()                             //ABSTRACTION
    {                                                               // POLYMORPHISM
        if (GameManager.GM.gameIsStarted)
        {
            transform.Translate(Vector3.back * GameManager.GM.speed * Time.deltaTime, Space.World);
            if (transform.position.z < zDestroy && !CompareTag("Ground") && !CompareTag("WallRocks"))
            {
                if (gameObject.CompareTag("Alien"))
                    gameObject.SetActive(false);
                else
                    Destroy(gameObject);

            }
        }
    }

}
