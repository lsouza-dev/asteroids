using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] public float shipAcceleration;
    [SerializeField] private float shipDisacceleration;
    [SerializeField] private float rotationSpeed;
    [SerializeField] public float invencibleTime;
    [SerializeField] public float powerUpTime;
    [SerializeField] public float respawnTime;
    [SerializeField] public float maxVelocity;
    [SerializeField] public float rechargeTime;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    [Header("Bullet Variables")]
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private Bullet bulletScript;
    [SerializeField] private float bulletSpeed = 30f;

    [Header("Controller Variables")]
    [SerializeField] public GameController gameController;
    [SerializeField] public ScreenShakeController shakeController;
    [SerializeField] public SpawnController spawnController;

    [Header("Game Objects")]
    [SerializeField] public GameObject shield;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] public GameObject explosion;

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] public SpriteRenderer playerRenderer;
    [SerializeField] public AudioListener listener;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CapsuleCollider2D coll;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> shootSounds;

    [Header("Bool Variables")]
    [SerializeField] public bool isRespawning;
    [SerializeField] public bool havePowerUp;
    [SerializeField] private bool doubleShoot;
    [SerializeField] public bool isAlive = true;
    [SerializeField] public bool movementIntrodution = true;
    private bool isAccelerating = true;
    private bool sideAcceleration = false;

    [Header("Shake Variables")]
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeDuration;

    private int firstPlay = 0;
    private float disabledCollider = 1f;
    

    // Start is called before the first frame update
    private void Awake()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        shakeController = FindObjectOfType<ScreenShakeController>();
        gameController = FindObjectOfType<GameController>();
        coll = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        listener = GetComponent<AudioListener>();
        audioSource = GetComponent<AudioSource>();
        spawnController = FindObjectOfType<SpawnController>();
    }
    void Start()
    {
        bulletScript.isMissil = false;
        PlayerPrefs.SetInt("firstGameplay", 1);
        firstPlay = PlayerPrefs.GetInt("firstPlay");

        if (firstPlay == 0) movementIntrodution = true;
        else movementIntrodution = false;

        if (isAlive) invencibleTime = 3f;

        if (!gameController.isAsteroidGameMode) transform.Rotate(0f, 0f, -90f);
    }

    // Update is called once per frame
    void Update()
    {
        if (disabledCollider >= 0) disabledCollider -= Time.deltaTime;
        else coll.enabled = true;

        if (Input.GetKeyDown(KeyCode.L))PlayerPrefs.SetInt("firstPlay", 0);
        // Se estiver vivo, chama os métodos de aceleração e rotação
        if (isAlive)
        {
            if (gameController.nextLevel)
            {
                transform.position = Vector2.zero;
                shipAcceleration = 0;
                invencibleTime = 3f;
            }


            if (rechargeTime >= 0) rechargeTime -= Time.deltaTime;

            if (invencibleTime > 0)
            {
                invencibleTime -= Time.deltaTime;
                coll.enabled = false;
                shield.SetActive(true);

            }
            else
            {
                coll.enabled = true;
                shield.SetActive(false);
            }

            if (powerUpTime >= 0)
            {
                powerUpTime -= Time.deltaTime;
            }
            else
            {
                bulletScript.isMissil = false;
            }

            if (!movementIntrodution && gameController.isAsteroidGameMode)
            {
                ShipAcceleration();
                ShipRotation();
                ShipShooting();

            }else if(!movementIntrodution && !gameController.isAsteroidGameMode)
            {
                ShipAcceleration();
                ShipShooting();
            }

            if (movementIntrodution)
            {
                shield.SetActive(false);
                invencibleTime = 0f;
                rb.velocity = Vector2.up * 8f;
                animator.SetBool("isBoost", true);
                if (transform.position.y >= 0 && transform.position.x == 0)
                {
                    invencibleTime = 3f;
                    isAlive = true;
                    movementIntrodution = false;
                    rb.velocity = Vector2.zero;
                    animator.SetBool("isBoost", false);
                    gameController.canva.gameObject.SetActive(true);
                    spawnController.InstantiateRocks(gameController.rockSpawn);

                    PlayerPrefs.SetInt("firstPlay", 1);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameController.isAsteroidGameMode)
        {
            if (isAlive && isAccelerating)
            {
                // Adiciona força ao rigid multiplicando pela aceleração e tranform.up
                rb.AddForce(shipAcceleration * transform.up);
                // Definindo o limite da velocidade da nave
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

            }
            else if (isAlive && !isAccelerating)
            {
                // Parando a nave se estiver vivo e não acelerar
                rb.velocity *= shipDisacceleration;

            }
        }
        else
        {
            if (isAlive && isAccelerating || sideAcceleration)
            {
                if (isAccelerating)
                {
                    // Definindo o limite da velocidade da nave
                    rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
                }
                else
                {
                    if (isAlive && !isAccelerating)
                    {
                        // Parando a nave se estiver vivo e não acelerar
                        rb.velocity *= shipDisacceleration;

                    }
                }
            }
        }
        
    }

    private void ShipAcceleration()
    {
        if (gameController.isAsteroidGameMode)
        {
            // Definindo que isAccelerating será true quando apertar seta para cima ou W
            isAccelerating = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);

            if (isAccelerating)
            {
                animator.SetBool("isBoost", true);
            }
            else
            {
                animator.SetBool("isBoost", false);
            }
        }
        else
        {
            float yInput = Input.GetAxis("Vertical") * shipAcceleration * Time.deltaTime;
            float xInput = Input.GetAxis("Horizontal") * shipAcceleration * Time.deltaTime;

            if (xInput != 0 || yInput != 0) animator.SetBool("isBoost", true);
            else animator.SetBool("isBoost",false);
            
            transform.position = new Vector2(Mathf.Clamp(transform.position.x + xInput,-xOffset,xOffset), Mathf.Clamp(transform.position.y + yInput,-yOffset,yOffset));
            
        }

    }

    private void ShipRotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            // Se apertar para esquerda, rotaciona a nave incrementando o rotation speed
            // multiplicado pelo deltaTime e transform.forward
            transform.Rotate(rotationSpeed * Time.deltaTime * transform.forward);

        }else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {

            // Se apertar para esquerda, rotaciona a nave decrementando o rotation speed
            // multiplicado pelo deltaTime e transform.forward
            transform.Rotate(-rotationSpeed * Time.deltaTime * transform.forward);
        }
    }

    private void ShipShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rechargeTime <= 0)
        {
            Vector2 shipVelocity = rb.velocity;
            Vector2 shipDirection = transform.up;
            float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);

            Rigidbody2D bullet = Instantiate(bulletPrefab,bulletSpawn.transform.position, Quaternion.identity);

            if (shipForwardSpeed < 0)
                shipForwardSpeed = 0;

            bullet.transform.rotation = transform.rotation;
            bullet.velocity = shipDirection * shipForwardSpeed;
            bullet.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);

            if (!bulletScript.isMissil) audioSource.clip = shootSounds[0]; 
            else audioSource.clip = shootSounds[1];

            rechargeTime = .15f;
            audioSource.Play();
            shakeController.shakeActive = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Rock" || other.tag == "SmallRock")
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameController.playerLifes -= 1;

            shakeAmount = 1f;
            shakeDuration = 1f;

            PlayerDeath();
            PlayerHitShake(shakeAmount,shakeDuration);

            Destroy(explosion, 1f);   
        }

        if (other.CompareTag("EnemieBullet"))
        {
            gameController.playerLifes -= 1;
            shakeAmount = .5f;
            shakeDuration = .5f;
            gameController.UpdatePlayerEnergy(gameController.playerLifes);
            PlayerDeath();
            PlayerHitShake(shakeAmount, shakeDuration);
            Destroy(other);
        }

        if (other.CompareTag("PUpHealth"))
        {
            Destroy(other.gameObject);

            if(gameController.playerLifes < 4)
            {
                gameController.playerLifes += 1;
                gameController.UpdatePlayerEnergy(gameController.playerLifes);
            }
            else
            {
                gameController.points += 500;                
            }
        }

        if (other.CompareTag("PUpPower"))
        {
            Bullet bullet = bulletPrefab.GetComponent<Bullet>();
            bullet.isMissil = true;
            powerUpTime = 5f;

            Destroy(other.gameObject);
        }
    }

    public void PlayerDeath()
    {
        if (gameController.isAsteroidGameMode)
        {
            if (gameController.playerLifes > 0)
            {
                gameController.isRespawn = true;
                Destroy(gameObject);
            }
            else
            {
                gameController.gameOver.SetActive(true);
                gameController.isRespawn = false;
                Destroy(gameObject);
                PlayerPrefs.SetInt("firstPlay", 0);

                if (gameController.points > gameController.highscorePoints) PlayerPrefs.SetInt("highscore", gameController.points);
            }
        }
        else
        {
            if(gameController.playerLifes == 0)
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void PlayerHitShake(float amount,float duration)
    {
        if (gameController.isAsteroidGameMode)
        {
            gameController.rocksDestroyedWithotDie = 0;
            gameController.isOnCombo = false;
        }
        else
        {
            amount = .5f;
            duration = .5f;
        }


        int lifesRemain = gameController.playerLifes;
        gameController.UpdatePlayerEnergy(lifesRemain);

        shakeController.shakeAmount = amount;
        shakeController.shakeDuration = duration;
        shakeController.shakeActive = true;

    }
}