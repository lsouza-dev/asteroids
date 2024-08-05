using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Vector2 dir;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Sprite[] sprites = new Sprite[2];
    [SerializeField] private GameObject rock; 

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
        Hit();
    }

    private void Hit()
    {
        Sprite teste = rock.GetComponent<SpriteRenderer>().sprite;
        teste = sprites[0];

        rock.GetComponent<SpriteRenderer>().sprite = teste;
        Instantiate(rock,transform.position,Quaternion.identity);
       
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
}
