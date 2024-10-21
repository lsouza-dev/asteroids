using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnController : MonoBehaviour
{


    [Header("Rocks Variables")]
    [SerializeField] public GameObject rockPrefab;
    [SerializeField] private Rock rock;
    [SerializeField] public int rocksQuantity;
    [SerializeField] public int rockSpawn = 1;
    [SerializeField] private int rocksAdd = 1;
    [SerializeField] public bool firstRockSpawn;
    [SerializeField] private float timeToFirstSpawn;
    public int rocksToPowerUp = 5;

    [Header("Position Variables")]
    [SerializeField] private float xLimit;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    [Header("Player Variables")]
    [SerializeField] private Player player;
    [SerializeField] public GameObject gameOver;

    [Header("PowerUps")]
    [SerializeField] private List<GameObject> powerUps;
    [SerializeField] public int destroyedRocks;

    [Header("Enemies")]
    [SerializeField] List<GameObject> enemiesList;
    [SerializeField] List<GameObject> bossList;
    [SerializeField] public float xOffset;
    [SerializeField] public float yOffset;
    [SerializeField] public float timeToSpawnEnemy = 2f;
    [SerializeField] public float timeToSpawnBoss = 10f;
    [SerializeField] public float currentTime;
    [SerializeField] public bool spawnBoss;
    [SerializeField] public bool bossFight;


    bool spawning = true;
    public bool isShooterMode = true;
    private GameController gameController;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        gameController = FindObjectOfType<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(player.isAlive && isShooterMode) StartCoroutine(SpawnEnemies());   
    }

    void Update()
    {
        if (isShooterMode)
        {
            if (currentTime <= timeToSpawnBoss && !bossFight) currentTime += Time.deltaTime;
            else spawnBoss = true;
            print($"Current: {currentTime} \tBoss: {timeToSpawnBoss}");
        }
    }


    public void InstantiateRocks(int quantity)
    {
        if (player.isAlive)
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

    private IEnumerator SpawnEnemies()
    {
        //while (spawning)
        //{
        //    if (!bossFight)
        //    {
        //        if (!spawnBoss)
        //        {
        //            float randomTime = Random.Range(1f, 3f);
        //            yield return new WaitForSeconds(randomTime);
        //            int randomEnemy = Random.Range(0, enemiesList.Count);
        //            Vector2 randomPos = new Vector2(xOffset, Random.Range(-yOffset, yOffset));
        //            Instantiate(enemiesList[randomEnemy], randomPos, Quaternion.identity);
        //        }
        //        else
        //        {
        //            int randomEnemy = Random.Range(0, bossList.Count);
        //            Vector2 pos = new Vector2(xOffset, 0f);
        //            Instantiate(bossList[randomEnemy], pos, Quaternion.identity);
        //            bossFight = true;
        //            spawnBoss = false;
        //            currentTime = 0f;
        //        }
        //    }
                
            
        //}
        yield return null;
    }
}
