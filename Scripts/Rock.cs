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
    [SerializeField] public int qtdRocks;
    [SerializeField] private float rockScale;
    [SerializeField] private SpriteRenderer spriteRenderer ;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameController controller;
    
    private bool bigRock = true;


    private void Awake()
    {
        controller = FindObjectOfType<GameController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomDirection();
        //qtdRocks = controller.rocksQuantity;
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
                for (int i = 0; i < 2; i++)
                {
                    Rock mediumRock = Instantiate(this, transform.position, Quaternion.identity);
                    rockScale = .8f;
                    mediumRock.transform.localScale = new Vector3(rockScale, rockScale, rockScale);
                    mediumRock.spriteRenderer.sprite = sprites[1];
                    mediumRock.bigRock = false;
                    controller.rocksQuantity++;
                }

                controller.rocksQuantity--;
                Destroy(other.gameObject);
                Destroy(this.gameObject);

                controller.points += 50;
            }
            else
            {
                controller.rocksQuantity--;
                Destroy(other.gameObject);
                Destroy(this.gameObject);

                controller.points += 25;
            }

            if (controller.rocksQuantity == 0)
            {

                controller.nextLevel = true;
            }

            Debug.Log($"Remain Rocks: {controller.rocksQuantity}");
        }
    }
}
