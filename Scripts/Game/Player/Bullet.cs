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

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, bulletLifetime);
        shootSound.Play();
    }

    // Update is called once per frame
    private void Awake()
    {
        shootSound = GetComponent<AudioSource>();
        Destroy(gameObject, bulletLifetime);
    }

    
}
