using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowManager : MonoBehaviour
{
    public static ShadowManager instance;

    public Shadow shadow;
    public ShadowUI shadowUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shadowUI.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
