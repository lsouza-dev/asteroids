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
    [SerializeField] private float shipStopping;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float minVelocity;
    [SerializeField] private float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Se estiver vivo, chama os métodos de aceleração e rotação
        if (isAlive)
        {
            ShipAcceleration();
            ShipRotation();
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
            rb.velocity = Vector2.zero;
        }
    }

    private void ShipAcceleration()
    {
        // Definindo que isAccelerating será true quando apertar seta para cima ou W
        isAccelerating = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
    }

    private void ShipRotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            // Se apertar para esquerda, rotaciona a nave incrementando o rotation speed
            // multiplicado pelo deltaTime e transform.forward
            transform.Rotate(rotationSpeed * Time.deltaTime * transform.forward);
        }else if (Input.GetKey(KeyCode.RightArrow))
        {

            // Se apertar para esquerda, rotaciona a nave decrementando o rotation speed
            // multiplicado pelo deltaTime e transform.forward
            transform.Rotate(-rotationSpeed * Time.deltaTime * transform.forward);
        }
    }
}
