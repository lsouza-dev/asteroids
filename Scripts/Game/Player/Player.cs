using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CapsuleCollider2D coll;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private Bullet bulletScript;




    [SerializeField] private bool isAlive = true;
    private bool isAccelerating = true;

    [SerializeField] private Animator animator;
    [SerializeField] public ScreenShakeController shakeController;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] public GameController gameController;
    [SerializeField] public GameObject shield;
    [SerializeField] public GameObject explosion;
    [SerializeField] public SpriteRenderer playerRenderer;
    

    [SerializeField] public float shipAcceleration;
    [SerializeField] private float shipDisacceleration;

    [SerializeField] private float maxVelocity;
    [SerializeField] private float minVelocity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float bulletSpeed = 30f;

    

    [SerializeField] public float invencibleTime = 3;
    [SerializeField] public float powerUpTime;
    [SerializeField] public float respawnTime;
    [SerializeField] public bool isRespawning;
    [SerializeField] public bool havePowerUp;

    [SerializeField] private bool doubleShoot;


    // Start is called before the first frame update
    private void Awake()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        shakeController = FindObjectOfType<ScreenShakeController>();
        gameController = FindObjectOfType<GameController>();
        coll = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        bulletScript.isMissil = false;
        invencibleTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        // Se estiver vivo, chama os métodos de aceleração e rotação
        if (isAlive)
        {
            if (gameController.nextLevel)
            {
                transform.position = Vector2.zero;
                shipAcceleration = 0;
                invencibleTime = 3f;
            }
            else
            {
                shipAcceleration = 20f;
            }

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

            ShipAcceleration();
            ShipRotation();
            ShipShooting();
        }
        else
        {

        }
    }

    private void FixedUpdate()
    {
        if(isAlive && isAccelerating)
        {
            // Adiciona força ao rigid multiplicando pela aceleração e tranform.up
            rb.AddForce(shipAcceleration * transform.up);
            // Definindo o limite da velocidade da nave
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

        }
        else if(isAlive && !isAccelerating) 
        {
            // Parando a nave se estiver vivo e não acelerar
            rb.velocity *= shipDisacceleration;
          
        }
    }

    private void ShipAcceleration()
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 shipVelocity = rb.velocity;
            Vector2 shipDirection = transform.up;
            float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);

            Rigidbody2D bullet = Instantiate(bulletPrefab,transform.position, Quaternion.identity);

            if (shipForwardSpeed < 0)
                shipForwardSpeed = 0;

            bullet.transform.rotation = transform.rotation;
            bullet.velocity = shipDirection * shipForwardSpeed;
            bullet.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);

            shakeController.shakeActive = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Rock" || other.tag == "SmallRock")
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            

            int lifesRemain = gameController.playerLifes -= 1;

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
            }

            gameController.UpdatePlayerEnergy(lifesRemain);

            shakeController.shakeAmount = 1f;
            shakeController.shakeDuration = 1f;
            shakeController.shakeActive = true;

            Destroy(explosion, 1f);   
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
}