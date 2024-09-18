using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime = 3f;
    [SerializeField] private Animation shoot;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private SpriteRenderer renderer;
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

        shootSound.Play();
    }

    // Update is called once per frame
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        shootSound = GetComponent<AudioSource>();
        Destroy(gameObject, bulletLifetime);
    }

    
}
