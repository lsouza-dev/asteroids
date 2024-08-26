using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime = 3f;
    [SerializeField] private Animation shoot;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, bulletLifetime);
        
        
    }

    // Update is called once per frame
    private void Awake()
    {
        Destroy(gameObject, bulletLifetime);
    }

    
}
