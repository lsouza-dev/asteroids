using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance;
    [SerializeField] public string gameDiff;

    private void Awake()
    {
        Instance = this;
    }
}
