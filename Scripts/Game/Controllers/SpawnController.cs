using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnController : MonoBehaviour
{


    [Header("Rocks Variables")]
    [SerializeField] public GameObject rockPrefab;
    [SerializeField] public Rock rock;
    [SerializeField] public int rocksQuantity;
    [SerializeField] public int rockSpawn = 1;
    [SerializeField] public int rocksAdd = 1;
    [SerializeField] public int rocksDivision;
    [SerializeField] public bool firstRockSpawn;
    [SerializeField] public float timeToFirstSpawn;
    [SerializeField] public int rocksToPowerUp = 30;

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
    [SerializeField] public float delayToSpawnBoss = 3f;
    [SerializeField] public float timeToSpawnBoss = 10f;
    [SerializeField] public float currentTime;
    [SerializeField] public bool spawnBoss = false;
    [SerializeField] public bool bossFight;
    [SerializeField] public int bossKilled;
    [SerializeField] public bool spawning = true;
    [SerializeField] public int enemiesKilled= 20;
    [SerializeField] public int enemiesKilledToPowerUp = 20;
    public bool isShooterMode = true;
    private GameController gameController;
    public bool isRespawn;
    public float respawnTime;
    string gameMode;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        gameController = FindObjectOfType<GameController>();
        gameMode = PlayerPrefs.GetString("gameMode");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (gameMode == "asteroids") isShooterMode = false;
        else if (gameMode == "shooter") isShooterMode = true;

        if (player.isAlive && isShooterMode) StartCoroutine(SpawnEnemies());   
    }

    void Update()
    {
        if (isShooterMode)
        {
            CurrentTimeVerification();
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

    private void CurrentTimeVerification()
    {
        if (!bossFight)
        {
            if (currentTime <= timeToSpawnBoss) currentTime += Time.deltaTime;
            else {
                bossFight = true;
                spawnBoss = true;
            }
        }
        else
        {
            delayToSpawnBoss -= Time.deltaTime;
            if (spawnBoss && delayToSpawnBoss <= 0) SpawnBoss();
         
        }
        
        
    }

    private void SpawnBoss()
    {
        Vector2 pos = new Vector2(xOffset, 0f);
        Instantiate(bossList[Boss.BOSSINDEX], pos, Quaternion.identity);
        spawnBoss = false;
        delayToSpawnBoss = 3f;
        Boss.BOSSINDEX += 1;
        timeToSpawnBoss += 10f;
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawning)
        {
            if (!bossFight)
            {
                float randomTime = Random.Range(1f, 3f);
                yield return new WaitForSeconds(randomTime);
                int randomEnemy = Random.Range(0, enemiesList.Count);
                Vector2 randomPos = new Vector2(xOffset, Random.Range(-yOffset, yOffset));
                Instantiate(enemiesList[randomEnemy], randomPos, Quaternion.identity);
            }
            else yield return null;
        }
    }
}
