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
    [SerializeField] private Collider2D rockCollider;
    [SerializeField] private SpriteRenderer spriteRenderer ;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameController controller;
    [SerializeField] private ScreenShakeController screenShake;
    [SerializeField] private SmallRock smallRock;
    [SerializeField] private int rocksDivision = 2;


    [SerializeField] private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rockCollider = GetComponent<Collider2D>();
        controller = FindObjectOfType<GameController>();
        screenShake = FindObjectOfType<ScreenShakeController>();
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
        
        if (other.CompareTag("Bullet"))
        {

            for (int i = 0; i < rocksDivision; i++)
            {
                SmallRock small =  Instantiate(smallRock, transform.position, Quaternion.identity);
                small.direction = i;
                rockScale = .8f;
                small.transform.localScale = new Vector3(rockScale, rockScale, rockScale);
                controller.rocksQuantity++;
            }

            controller.rocksQuantity--;
            Destroy(other.gameObject);
            Destroy(gameObject);

            screenShake.shakeAmount = .2f;
            screenShake.shakeDuration = .2f;
            screenShake.shakeActive = true;

            controller.points += 50;
            
        }
    }
}
