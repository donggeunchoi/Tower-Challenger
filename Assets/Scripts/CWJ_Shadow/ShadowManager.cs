using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowManager : MonoBehaviour
{
    public static ShadowManager instance;

    public Shadow shadow;
    public ShadowUI shadowUI;
    public ShadowData[] shadowData;

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
        shadowUI.shadowGameInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
