using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.SearchService;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Text Mesh")]
    [SerializeField]private TMP_Text pointsText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timerText;
    public int points = 0;
    float nextLevelTimer = 3f;

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
    [SerializeField] private float xMin = 25;
    [SerializeField] private float xMax = 34;
    [SerializeField]  private float yMin = 15;
    [SerializeField] private float yMax = 18;

    // Start is called before the first frame update
    

    private void Awake()
    {
        rock = FindObjectOfType<Rock>();
    }

    void Start()
    {
        levelTransition = true;
        rocksSpawn = gameLevel + rocksAdd;
        rocksQuantity = rocksSpawn;
        InstantiateRocks(rocksSpawn);
    }

    // Update is called once per frame
    void Update()
    {
       if(nextLevel)
       {
            NextLevel();
       }

        if (levelTransition)
        {
            nextLevelTimer -= Time.time;
            LevelText();
        }


        pointsText.text = $"POINTS: {points}";
    }

    public void NextLevel()
    {
        nextLevel = false;
        gameLevel += 1;
        rocksSpawn = gameLevel + rocksAdd;
        rocksQuantity = rocksSpawn;
        InstantiateRocks(rocksSpawn);
        //rock.qtdRocks = rocksQuantity;
    }

    public void LevelText()
    {
        
        if(nextLevelTimer <= 0)
        {
            Debug.Log("Inicia");
        }

        timerText.text = $"{Mathf.Round(nextLevelTimer)}...";
    }

    

    private void InstantiateRocks(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(-xMin, xMax), Random.Range(-yMin, yMax));
            GameObject obstacleInstance = Instantiate(rockPrefab, randomPos, Quaternion.identity);
        }
    }
}
