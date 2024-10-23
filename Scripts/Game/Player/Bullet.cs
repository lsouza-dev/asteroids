using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animation shoot;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float bulletLifetime = 2f;
    [SerializeField] public int bulletDamage;
    [SerializeField] private GameController gameController;
    [SerializeField] private SpawnController spawnController;
    [SerializeField] public bool isMissil = false;


    // Start is called before the first frame update
    void Start()
    {
        if (isMissil)
        {
            renderer.sprite = sprites[1];
            bulletDamage = 2;
            animator.Play("missil");
        }
        else
        {
            renderer.sprite = sprites[0];
            bulletDamage = 1;
            animator.Play("bullet");
        }
    }

    // Update is called once per frame
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameController = FindObjectOfType<GameController>();
        spawnController = FindObjectOfType<SpawnController>();
        Destroy(gameObject,bulletLifetime);
    }

    private void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RockCollider"))
        {
            if (gameController.isOnCombo)
            {
                gameController.points += gameController.rockPoints * gameController.comboMultiplier;
            }
            else
            {
                gameController.points += gameController.rockPoints;
            }
            gameController.rocksDestroyedWithotDie += 1;
        }

        if (other.CompareTag("SmallRockCollider"))
        {
            if (gameController.isOnCombo)
            {
                gameController.points += gameController.smallRockPoints * gameController.comboMultiplier;
            }
            else
            {
                gameController.points += gameController.smallRockPoints;
            }
            gameController.rocksDestroyedWithotDie += 1;
        }
    }
}
