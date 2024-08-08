using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rock : MonoBehaviour
{
    [SerializeField] private Vector2 dir;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int qtdRocks = 0;
    [SerializeField] private float rockScale;
    [SerializeField] private SpriteRenderer spriteRenderer ;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameController controller;
    
    private bool bigRock = true;
    


    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        RandomDirection();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
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
                qtdRocks = 2;
                controller.rocksQuantity = qtdRocks;

                for (int i = 0; i < qtdRocks; i++)
                {
                    Rock mediumRock = Instantiate(this, transform.position, Quaternion.identity);
                    rockScale = .8f;
                    mediumRock.transform.localScale = new Vector3(rockScale, rockScale, rockScale);
                    mediumRock.spriteRenderer.sprite = sprites[1];
                    mediumRock.bigRock = false;
                    controller.rocksQuantity++;
                }

                Debug.Log($"Destruiu ROCK");
                controller.rocksQuantity--;
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
            else
            {
                controller.rocksQuantity--;
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                Debug.Log($"Destruiu SMALLROCK");
            }
            
            if(controller.rocksQuantity == 0)
            {
                controller.nextLevel = true;
            }
        }
    }
}
