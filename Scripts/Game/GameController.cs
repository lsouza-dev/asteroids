using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.SearchService;
using UnityEditor.Tilemaps;
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
    private Rock rock;
    public int rocksQuantity;
    [SerializeField] public int rocksSpawn = 1;
    [SerializeField] private int rocksAdd= 1;

    [Header("Position Variables")]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField]  private float yMin;
    [SerializeField] private float yMax;


    [SerializeField] public TogglesManager diffToggles;
    [SerializeField] private string diff;

    [SerializeField] public List<Sprite> energySprites;
    [SerializeField] public Image energyImage;
    [SerializeField] public int playerLifes = 4;

    // Start is called before the first frame update
    

    private void Awake()
    {
        rock = FindObjectOfType<Rock>();                                                                                                                                                                                                                                                                                                                                                                                                               
    }

    void Start()
    {
        UpdatePlayerEnergy(playerLifes - 1);

        try
        {
            diff = TogglesManager.instance.diff;
        }
        catch
        {
            if (diff == null)
            {
                diff = "facil";
            }
            
        }

        if(diff == "facil" )
        {
            rocksAdd = 2;
        }else if (diff == "medio")
        {
            rocksAdd = 4;
        }else if (diff == "dificil")
        {
            rocksAdd = 6;
        }

        levelTransition = true;
        rocksSpawn = gameLevel + rocksAdd;
        rocksQuantity = rocksSpawn;
        InstantiateRocks(rocksSpawn);
        levelText.text = $"LEVEL: {gameLevel}";
        timerText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
       if(nextLevel)
       {
            Player player = FindObjectOfType<Player>();
            player.transform.position = Vector3.zero;
            LevelCronometer();
            if (nextLevelTimer <= 0)
            {
                NextLevel();
                timerText.text = string.Empty;
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


    public void UpdatePlayerEnergy(int lifes)
    {
        energyImage.sprite = energySprites[lifes];
    }
}
