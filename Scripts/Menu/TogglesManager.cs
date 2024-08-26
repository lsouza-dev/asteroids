using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TogglesManager : MonoBehaviour
{
    [SerializeField] public List<Toggle> toggles = new List<Toggle>();
    [SerializeField] public string diff;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(state =>
            {
                if (toggle.isOn)
                {
                    diff = toggle.name;
                }
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
