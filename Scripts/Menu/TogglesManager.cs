using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TogglesManager : MonoBehaviour
{
    public static TogglesManager instance;

    [SerializeField] public List<Toggle> toggles = new List<Toggle>();
    [SerializeField] public string diff;
    
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    void Start()
    {
        diff = "facil";
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(state =>
            {
                if (toggle.isOn)
                {
                    diff = toggle.name.ToLower();
                }
            });
        }
    }
}
