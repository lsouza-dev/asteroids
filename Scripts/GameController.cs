using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Text Mesh")]
    [SerializeField]private TMP_Text pointsText;
    public int points = 0;

    [Header("Level Variables")]
    private int gameLevel = 1;
    public bool nextLevel;

    [Header("Rocks Variables")]
    [SerializeField] private GameObject rockPrefab;
    private Rock rock;
    public int rocksQuantity;
    [SerializeField] public int rocksSpawn = 1;
    [SerializeField] private int rocksAdd= 1;

    [Header("Position Variables")]
    [SerializeField] private float xMin = 15;
    [SerializeField] private float xMax = 33;
    [SerializeField]  private float yMin = 10;
    [SerializeField] private float yMax = 18;

    // Start is called before the first frame update
    

    private void Awake()
    {
        rock = FindObjectOfType<Rock>();
    }

    void Start()
    {
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

    private void InstantiateRocks(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(-xMin, xMax), Random.Range(-yMin, yMax));
            GameObject obstacleInstance = Instantiate(rockPrefab, randomPos, Quaternion.identity);
        }
    }
}
