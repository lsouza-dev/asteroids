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
    [SerializeField]private TMP_Text pointsText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timerText;
    public int points = 0;
    float nextLevelTimer = 3.5f;

    [Header("Level Variables")]
    private int gameLevel = 1;
    public bool levelTransition;
    public bool nextLevel;

    [Header("Rocks Variables")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private Rock rock;
    public int rocksQuantity;
    [SerializeField] public int rocksSpawn = 1;
    [SerializeField] private int rocksAdd= 1;

    [Header("Position Variables")]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
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

    [SerializeField] private Player player;
    [SerializeField] public GameObject gameOver;


    [Header("PowerUps")]
    [SerializeField] private List<GameObject> powerUps;
    [SerializeField] public int destroyedRocks;
    public int rocksToPowerUp = 25;

    [SerializeField] public int playerLifes = 4;

    // Start is called before the first frame update

    void Start()
    {
        UpdatePlayerEnergy(playerLifes);

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

        levelTransition = true;
        rocksSpawn = gameLevel + rocksAdd;
        rocksQuantity = rocksSpawn;
        InstantiateRocks(rocksSpawn);
        player.invencibleTime = 3;
        levelText.text = $"LEVEL: {gameLevel}";
        timerText.text = string.Empty;
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
                timerText.text = string.Empty;
            }
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
        rocksSpawn = gameLevel + rocksAdd;
        levelText.text = $"LEVEL: {gameLevel}";
        rocksQuantity = rocksSpawn;
        nextLevelTimer = 3.5f;
        InstantiateRocks(rocksSpawn);
        
    }

    public void LevelCronometer()
    {
        nextLevelTimer -= Time.deltaTime;
        timerText.text = $"{Mathf.Round(nextLevelTimer)}...";
    }


    private void InstantiateRocks(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector2 randomPos = new Vector2(UnityEngine.Random.Range(-xMin, xMax), UnityEngine.Random.Range(-yMin, yMax));
            GameObject obstacleInstance = Instantiate(rockPrefab, randomPos, Quaternion.identity);
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
