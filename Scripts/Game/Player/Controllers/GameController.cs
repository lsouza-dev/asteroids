using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.SearchService;
using UnityEditor.Tilemaps;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    [Header("Text Mesh")]
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] public TMP_Text nextLevelText;
    [SerializeField] private TMP_Text comboPoints;
    public int points = 0;
    float nextLevelTimer = 3.5f;

    [Header("Level Variables")]
    private int gameLevel = 1;
    public bool levelTransition;
    public bool nextLevel;

    [Header("Rocks Variables")]
    [SerializeField] public GameObject rockPrefab;
    [SerializeField] private Rock rock;
    [SerializeField] public int rocksQuantity;
    [SerializeField] public int rockSpawn = 1;
    [SerializeField] private int rocksAdd= 1;
    [SerializeField] public bool firstRockSpawn;
    [SerializeField] private float timeToFirstSpawn;
    public int rocksToPowerUp = 5;

    [Header("Position Variables")]
    [SerializeField] private float xLimit;
    [SerializeField]  private float yMin;
    [SerializeField] private float yMax;

    [Header("Respawn Variables")]
    [SerializeField] public bool isRespawn;
    [SerializeField] public float respawnTime = 2;
    
    [Header("Sprites Variables")]
    [SerializeField] public List<Sprite> energySprites;
    [SerializeField] public Image energyImage;

    [Header("Difficulty Variables")]
    [SerializeField] public TogglesManager diffToggles;
    [SerializeField] private string diff;

    [Header("Player Variables")]
    [SerializeField] private Player player;
    [SerializeField] public GameObject gameOver;


    [Header("PowerUps")]
    [SerializeField] private List<GameObject> powerUps;
    [SerializeField] public int destroyedRocks;
    [SerializeField] public Bullet bulletPrefab;

    [Header("Points Variables")]
    public int highscorePoints;
    public int rockPoints = 50;
    public int smallRockPoints = 25;
    public bool isOnCombo;
    public int comboMultiplier;
    public int rocksDestroyedWithotDie;

    [SerializeField] public int playerLifes = 4;
    [SerializeField] public GameObject canva;

    // Start is called before the first frame update

    private void Awake()
    {
        highscorePoints = PlayerPrefs.GetInt("highscore");
        player.movementIntrodution = true;
    }
    void Start()
    {
        if (highscorePoints == 0) highscoreText.text = $"HIGHSCORE: 0000";
        else
        {
            highscoreText.text = $"HIGHSCORE: {highscorePoints}";
        }

        UpdatePlayerEnergy(playerLifes);
        
        //bulletPrefab.isMissil = true;

        try
        {
            diff = TogglesManager.instance.diff;
        }
        catch
        {
            if (diff == null)
            {
                diff = "facil";
                rock.rocksDivision = 2;
            }
            
        }

        if(diff == "facil" )
        {
            rocksAdd = 1;
            rock.rocksDivision = 2; 

        }else if (diff == "medio")
        {
            rocksAdd = 3;
            rock.rocksDivision = 3;
        }
        else if (diff == "dificil")
        {
            rocksAdd = 5;
            rock.rocksDivision = 3;
        }
            rockSpawn = gameLevel + rocksAdd;
            rocksQuantity = rockSpawn;
            levelText.text = $"LEVEL: {gameLevel}";

            //player.invencibleTime = 3;
            //levelTransition = true;
            //nextLevelText.text = string.Empty;
        
    }

    // Update is called once per frame
    void Update()
    {
       if(nextLevel)
       {
            player.shipAcceleration = 0f;
            LevelCronometer();
            if (nextLevelTimer <= 0)
            {
               
                NextLevel();
                nextLevelText.text = string.Empty;
            }
       }

        if (rocksDestroyedWithotDie >= 20 && rocksDestroyedWithotDie < 50)
        {
            comboMultiplier = 2;
            isOnCombo = true;
            GameObject comboPointsGO = comboPoints.gameObject;
            comboPointsGO.SetActive(true);
            comboPoints.text = $"X{comboMultiplier}";
        }
        else if (rocksDestroyedWithotDie >= 50)
        {
            comboMultiplier = 3;
            comboPoints.text = $"X{comboMultiplier}";
        }
        else
        {
            GameObject comboPointsGO = comboPoints.gameObject;
            comboPointsGO.SetActive(false);
        }

        if (isRespawn)
        {
            respawnTime -= Time.deltaTime;
            if(respawnTime < 0)
            {
                PlayerRespawn();
            }
            
        }
        pointsText.text = $"POINTS: {points}";
    }

    public void NextLevel()
    {
        nextLevel = false;
        gameLevel += 1;
        rockSpawn = gameLevel + rocksAdd;
        levelText.text = $"LEVEL: {gameLevel}";
        rocksQuantity = rockSpawn;
        nextLevelTimer = 3.5f;
        InstantiateRocks(rockSpawn);
        
    }

    public void LevelCronometer()
    {
        nextLevelTimer -= Time.deltaTime;
        nextLevelText.text = $"NEXT LEVEL...{Mathf.Round(nextLevelTimer)}";
    }


    public void InstantiateRocks(int quantity)
    {
        if(player.isAlive)
        {
            for (int i = 0; i < quantity; i++)
            {
                Vector2 randomPos = new Vector2(xLimit, UnityEngine.Random.Range(-yMin, yMax));
                GameObject obstacleInstance = Instantiate(rockPrefab, randomPos, Quaternion.identity);
            }
        }
    }

    public void InstantiatePowerUp(Vector2 pos)
    {
        GameObject powerUp = powerUps[UnityEngine.Random.Range(0, powerUps.Count)];
        GameObject selectedPowerUp = Instantiate(powerUp, pos, Quaternion.identity);
        destroyedRocks = 0;
    }


    public void UpdatePlayerEnergy(int lifes)
    {
        energyImage.sprite = energySprites[lifes];
    }
        
    public void PlayerRespawn()
    {
        Player respawnPlayer = Instantiate(player, Vector2.zero, Quaternion.identity);
        respawnTime = 2f;
        isRespawn = false;
    }

}
