using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager GM { get; private set; }

    public float scoreLimit; // area limits fo scorring
    public float speed;
    public float speedRB;
    public float zDestroyUfo;

    public List<GameObject> ufoList;

    [Header("Difficulty Conditions")]
    public int score;
    public int healthPlayer;
    public int healthUfo;
    public int healthUfoBoss;
    public int diffMultiplier;
    public int bossKillCounter;
    public int victoryBossKillCounter;
    public int ufoKillCounter;
    [Header("Game Conditions")]
    public string gameDifficulty;
    public bool gameIsStarted;
    public bool bossSpawned;
    public bool bossAlive;
    public bool gotBomb;
    public bool gotHealth;
    public bool playerDead;
    public bool safeSpaceActivated;
    public bool gameWon;
    [Header("Character Variants")]
    public bool charWrangler;
    public bool charFarmer;
    public bool charWife;
    public bool charDaughter;

    // Start is called before the first frame update
    private void Awake()
    {
        if (GM != null)
        {
            Destroy(gameObject);
            return;
        }
        GM = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        gameIsStarted = false;
        Easy();
    }
    private void Update()
    {
        /*if (gameIsStarted)
            if (!gotBomb && !GameObject.FindWithTag("Alien").GetComponent<MoveDownUFO>().counted)
            {
                ufoList.Add(GameObject.FindWithTag("Alien"));
                GameObject.FindWithTag("Alien").GetComponent<MoveDownUFO>().counted = true;
            }*/

    }
    public void Easy()
    {
        scoreLimit = 25;
        speed = 3.5f;
        speedRB = 225;
        zDestroyUfo = -35;
        healthPlayer = 10;
        healthUfo = 1;
        healthUfoBoss = 2;
        gameDifficulty = "Easy";
        diffMultiplier = 1;
        victoryBossKillCounter = 1;
    }
    public void Normal()
    {
        scoreLimit = 20;
        speed = 5f;
        speedRB = 237.5f;
        zDestroyUfo = -40;
        healthPlayer = 9;
        healthUfo = 2;
        healthUfoBoss = 3;
        gameDifficulty = "Normal";
        diffMultiplier = 2;
        victoryBossKillCounter = 2;
    }
    public void Hard()
    {
        scoreLimit = 15;
        speed = 7.5f;
        speedRB = 250;
        zDestroyUfo = -40;
        healthPlayer = 7;
        healthUfo = 3;
        healthUfoBoss = 4;
        gameDifficulty = "Hard";
        diffMultiplier = 3;
        victoryBossKillCounter = 3;
    }
    public void StartGame()
    {
        gameIsStarted = true;
        bossSpawned = false;
        bossAlive = false;
        gotBomb = false;
        gotHealth = false;
        playerDead = false;
        gameWon = false;
        safeSpaceActivated = false;
        score = 0;
        ufoKillCounter = 0;
        if (playerDead)
        {
            playerDead = false;
            Debug.Log("222");
        }
        SceneManager.LoadScene(1);
    }
    public void ExitToMenu()
    {
        gameIsStarted = false;
        bossSpawned = false;
        bossAlive = false;
        gotBomb = false;
        gotHealth = false;
        playerDead = false;
        gameWon = false;
        safeSpaceActivated = false;
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    /*public void SaveGameData()
    {
        SaveData data = new SaveData();

        data.easyPlays = easyPlays;
        data.normalPlays = normalPlays;
        data.hardPlays = hardPlays;

        data.firstPlayerName = firstPlayerName;
        data.secondPlayerName = secondPlayerName;
        data.thirdPlayerName = thirdPlayerName;

        data.score1 = score1;
        data.score2 = score2;
        data.score3 = score3;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            easyPlays = data.easyPlays;
            normalPlays = data.normalPlays;
            hardPlays = data.hardPlays;

            firstPlayerName = data.firstPlayerName;
            secondPlayerName = data.secondPlayerName;
            thirdPlayerName = data.thirdPlayerName;

            score1 = data.score1;
            score2 = data.score2;
            score3 = data.score3;
        }
    }*/
}
