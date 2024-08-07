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
    [SerializeField] private float maxVelocity;
    [SerializeField] private float minVelocity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float bulletSpeed = 8f;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Rigidbody2D bulletPrefab;



    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Se estiver vivo, chama os m�todos de acelera��o e rota��o
        if (isAlive)
        {
            ShipAcceleration();
            ShipRotation();
            ShipShooting();
        }
        
    }

    private void FixedUpdate()
    {
        if(isAlive && isAccelerating)
        {
            // Adiciona for�a ao rigid multiplicando pela acelera��o e tranform.up
            rb.AddForce(shipAcceleration * transform.up);
            // Definindo o limite da velocidade da nave
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        }
        else if(isAlive && !isAccelerating) 
        {
            // Parando a nave se estiver vivo e n�o acelerar
            rb.velocity = Vector2.zero;
        }
    }

    private void ShipAcceleration()
    {
        // Definindo que isAccelerating ser� true quando apertar seta para cima ou W
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

            bullet.velocity = shipDirection * shipForwardSpeed;

            bullet.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);


        }

    }
    
}
