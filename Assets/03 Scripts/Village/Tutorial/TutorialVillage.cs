using System.Collections.Generic;
using UnityEngine;

public class TutorialVillage : MonoBehaviour
{
    public List<GameObject> Tutorial = new List<GameObject>();
    private int index = 0;

    public void OnClickNext()
    {
        if (index < Tutorial.Count - 1)
        {
            index++;
            ShowUI();
        }
    }

    public void OnClickBack()
    {
        if (index < Tutorial.Count - 1)
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
