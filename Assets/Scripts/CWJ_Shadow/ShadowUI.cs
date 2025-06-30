using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShadowUI : MonoBehaviour
{
    public Image shadowImage;
    public Image[] choicesImage;

    [HideInInspector] public int randomIndex;

    public void shadowGameInit()
    {
        randomIndex = Random.Range(0, ShadowManager.instance.shadowData.Length);
        var p = ShadowManager.instance.shadowData[randomIndex];// 랜덤으로 문제를 생성

        shadowImage.sprite = p.shadowSprite;

        for (int i = 0; i < choicesImage.Length; i++)
        {
            choicesImage[i].sprite = p.choices[i];
        }
    }

    //public void AnswerButton(int index)
    //{
    //    ShadowManager.instance.shadow.Answer(index);
    //}

    //public void shadowGameInit()
    //{
    //    int random = 
    //}
}
