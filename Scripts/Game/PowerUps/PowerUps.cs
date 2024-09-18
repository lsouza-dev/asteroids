using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 dir;
    [SerializeField] private float speed;
    [SerializeField] private float xLimit = 38f;
    [SerializeField] private float yLimit = 22f;
    // Start is called before the first frame update
    void Start()
    {
        RandomDirection();
        dir = GetComponent<Transform>().position;

    }

    private void RandomDirection()
    {
        rb = GetComponent<Rigidbody2D>();

        int direction = Random.Range(0, 7);

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
            case 4:
                dir = new Vector2(speed, 0);
                break;
            case 5:
                dir = new Vector2(-speed, 0);
                break;
            case 6:
                dir = Vector2.up * speed;
                break;
            case 7:
                dir = Vector2.down * speed;
                break;
        }

        rb.velocity = dir;
    }

    private void Update()
    {
        if (dir.x > xLimit)
        {
            Destroy(gameObject);
        }
        else if (dir.x < -xLimit)
        {
            Destroy(gameObject);
        }
        else if (dir.y > yLimit)
        {
            Destroy(gameObject);
        }
        else if (dir.y < -yLimit)
        {
            Destroy(gameObject);
        }
        dir = transform.position;
    }
}
