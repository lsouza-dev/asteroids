using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private bool isAlive = true;
    private bool isAccelerating = true;

    [SerializeField] private float shipAcceleration;
    [SerializeField] private float shipDisacceleration;

    [SerializeField] private float maxVelocity;
    [SerializeField] private float minVelocity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float bulletSpeed = 8f;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private GameObject explosionPrefab;

    float timerToRespawn;

    [SerializeField] public ScreenShakeController shakeController;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shakeController = FindObjectOfType<ScreenShakeController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Se estiver vivo, chama os métodos de aceleração e rotação
        if (isAlive)
        {
            ShipAcceleration();
            ShipRotation();
            ShipShooting();
        }else
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
            Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

            Vector2 shipVelocity = rb.velocity;
            Vector2 shipDirection = transform.up;
            float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);

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
            GameObject explosion = Instantiate(explosionPrefab,transform.position,Quaternion.identity);
            Destroy(explosion,1f);

            shakeController.shakeAmount = 1f;
            shakeController.shakeDuration = 1f;
            shakeController.shakeActive = true;
            RestartGame();
            Destroy(this.gameObject);
            isAlive = false; 
        }
    }

    private void RestartGame()
    {
        if(timerToRespawn >= 2)
        {
            Player player = gameObject.GetComponent<Player>();
            Instantiate(player, new Vector2(0, 0), Quaternion.identity);
        }
    }
}
