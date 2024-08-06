using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Vector2 dir;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Sprite[] sprites = new Sprite[2];
    [SerializeField] private GameObject rock; 
    [SerializeField] private int qtdSmallRocks;
    private bool bigRock = true;

    // Start is called before the first frame update
    void Start()
    {
        RandomDirection();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    
    private void RandomDirection()
    {
        rb = GetComponent<Rigidbody2D>();

        int direction = Random.Range(0, 4);

        switch (direction)
        {
            case 0:
                dir = new Vector2(-speed, speed);
                break;
            case 1:
                dir = new Vector2(speed, speed);
                break;
            case 2:
                dir = new Vector2(-speed, -speed);
                break;
            case 3:
                dir = new Vector2(speed, -speed);
                break;
        }

        rb.velocity = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            if (bigRock)
            {
                qtdSmallRocks = 2;

                for (int i = 0; i < qtdSmallRocks; i++)
                {
                    Rock smallRock = Instantiate(this, transform.position, Quaternion.identity);
                    smallRock.bigRock = false;
                }
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }else
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
        }
    }
}
