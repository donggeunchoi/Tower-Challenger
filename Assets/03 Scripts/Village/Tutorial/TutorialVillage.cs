using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVillage : MonoBehaviour
{
    public List<GameObject> Tutorial = new List<GameObject>();
    private int index = 0;


    private void Start()
    {
       index = 0;
       ShowUI();
    }

    private void OnEnable()
    {
        index = 0;
        ShowUI();
    }

    public void OnClickNext()
    {
        if (index < Tutorial.Count)
        {
            index++;
            ShowUI();
        }
    }

    public void OnClickBack()
    {
        if (index < Tutorial.Count)
        {
            index--;
            ShowUI();
        }
    }

    public void ShowUI()
    {
        for (int i = 0; i < Tutorial.Count; i++)
        {
            Tutorial[i].SetActive(i == index);
        }
    }
    
}
