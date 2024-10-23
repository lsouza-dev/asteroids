using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    private SpawnController spawnController;

    [Header("Level Variables")]
    private int gameLevel = 1;
    public bool levelTransition;
    public bool nextLevel;

    
    
    [Header("Sprites Variables")]
    [SerializeField] public List<Sprite> energySprites;
    [SerializeField] public Image energyImage;

    [Header("Difficulty Variables")]
    [SerializeField] public TogglesManager diffToggles;
    [SerializeField] private string diff;

    [Header("Player Variables")]
    [SerializeField] private Player player;
    [SerializeField] public GameObject gameOver;

    [Header("Points Variables")]
    public int highscorePoints;
    public int rockPoints = 50;
    public int smallRockPoints = 25;
    public bool isOnCombo;
    public int comboMultiplier;
    public int rocksDestroyedWithotDie;

    [SerializeField] public int playerLifes = 4;
    string gameMode;
    [SerializeField] public GameObject canva;
    [SerializeField] public bool isAsteroidGameMode = true;
    // Start is called before the first frame update

    private void Awake()
    {
        spawnController = FindObjectOfType<SpawnController>();
        if (isAsteroidGameMode) highscorePoints = PlayerPrefs.GetInt("highscore");
        else highscorePoints = PlayerPrefs.GetInt("shooterHighscore");
        player.movementIntrodution = true;
        gameMode = PlayerPrefs.GetString("gameMode");
    }
    void Start()
    {

        if (gameMode == "asteroids") isAsteroidGameMode = true;
        else if (gameMode == "shooter") isAsteroidGameMode = false;

        if (isAsteroidGameMode)
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
                    spawnController.rocksDivision = 2;
                }

            }

            if (diff == "facil")
            {
                spawnController.rocksAdd = 1;
                spawnController.rock.rocksDivision = 2;

            }
            else if (diff == "medio")
            {
                spawnController.rocksAdd = 3;
                spawnController.rock.rocksDivision = 3;
            }
            else if (diff == "dificil")
            {
                spawnController.rocksAdd = 5;
                spawnController.rock.rocksDivision = 3;
            }
            spawnController.rockSpawn = gameLevel + spawnController.rocksAdd;
            spawnController.rocksQuantity = spawnController.rockSpawn;
            levelText.text = $"LEVEL: {gameLevel}";
        }

        if (highscorePoints == 0) highscoreText.text = $"HIGHSCORE: 0000";
        {
            highscoreText.text = $"HIGHSCORE: {highscorePoints}";
        }
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
        if (isAsteroidGameMode)
        {
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
        }
       

        if (spawnController.isRespawn)
        {
            spawnController.respawnTime -= Time.deltaTime;
            if(spawnController.respawnTime < 0)
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
        spawnController.rockSpawn = gameLevel + spawnController.rocksAdd;
        levelText.text = $"LEVEL: {gameLevel}";
        spawnController.rocksQuantity = spawnController.rockSpawn;
        nextLevelTimer = 3.5f;
        spawnController.InstantiateRocks(spawnController.rockSpawn);
        
    }

    public void LevelCronometer()
    {
        nextLevelTimer -= Time.deltaTime;
        nextLevelText.text = $"NEXT LEVEL...{Mathf.Round(nextLevelTimer)}";
    }

    public void UpdatePlayerEnergy(int lifes)
    {
         if(lifes >= 0)energyImage.sprite = energySprites[lifes];
    }
        
    public void PlayerRespawn()
    {
        Player respawnPlayer = Instantiate(player, Vector2.zero, Quaternion.identity);
        spawnController.respawnTime = 2f;
        spawnController.isRespawn = false;
    }

}
