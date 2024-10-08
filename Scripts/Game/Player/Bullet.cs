using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animation shoot;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private SpriteRenderer renderer;

    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float bulletLifetime = 3f;
    [SerializeField] private GameController gameController;
    [SerializeField] public bool isMissil = false;


    // Start is called before the first frame update
    void Start()
    {
        if (isMissil)
        {
            renderer.sprite = sprites[1];
            animator.Play("missil");
        }
        else
        {
            renderer.sprite = sprites[0];
            animator.Play("bullet");
        }

        shootSound.Play() ;
    }

    // Update is called once per frame
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        shootSound = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        Destroy(gameObject, bulletLifetime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rock"))
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

        if (other.CompareTag("SmallRock"))
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
