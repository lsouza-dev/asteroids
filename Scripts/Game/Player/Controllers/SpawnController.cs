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
    [SerializeField] public float xOffset;
    [SerializeField] public float yOffset;
    [SerializeField] public float timeToSpawnEnemy = 2f;

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
        
    }

    void Update()
    {
        if(timeToSpawnEnemy > 0) timeToSpawnEnemy -= Time.deltaTime;
        if(timeToSpawnEnemy < 0){
            StartCoroutine(SpawnEnemies());
            timeToSpawnEnemy = 1f;
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
        while (spawning)
        {
            yield return new WaitForSeconds(1f);
            int randomEnemy = Random.Range(0, enemiesList.Count);
            Vector2 randomPos = new Vector2(xOffset, Random.Range(-yOffset, yOffset));
            Instantiate(enemiesList[randomEnemy],randomPos, Quaternion.identity);

        }
    }
}
