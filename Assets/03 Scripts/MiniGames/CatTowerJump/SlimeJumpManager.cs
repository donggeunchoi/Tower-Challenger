using System;
using UnityEngine;

public class SlimeJumpManager : MonoBehaviour
{
    public ResetTable[] resetters;

    public void ResetAll()
    {
        foreach (var r in resetters)
        {
            r.ResetTable();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
