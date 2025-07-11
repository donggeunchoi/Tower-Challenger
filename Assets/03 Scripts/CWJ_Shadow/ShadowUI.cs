using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShadowUI : MonoBehaviour
{
    public Image shadowImage;
    public Image[] choicesImage;

    public void shadowGameInit()
    {
        
        ShadowManager.instance.randomIndex = Random.Range(0, ShadowManager.instance.shadowData.Length);

        shadowInti();
        shoicesInti();
    }

    public void AnswerButton(int index)
    {
        ShadowManager.instance.shadow.Answer(index);
    }
    
    private void shadowInti()
    {
        var p = ShadowManager.instance.shadowData[ShadowManager.instance.randomIndex];// 랜덤으로 문제를 생성

        shadowImage.sprite = p.shadowSprite;
        
        StartCoroutine(ShadowFalse());
    }

    private void shoicesInti()
    {
        for (int i = 0; i < choicesImage.Length; i++)
        {
            choicesImage[i].sprite = ShadowManager.instance.shadowData[ShadowManager.instance.randomIndex].choices[i];
            choicesImage[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator ShadowFalse()
    {
        yield return new WaitForSeconds(ShadowManager.instance.shadowData[ShadowManager.instance.randomIndex].shadowTime);

        shadowImage.gameObject.SetActive(false);
        for (int i = 0; i < choicesImage.Length; i++)
        {
            choicesImage[i].gameObject.SetActive(true);
        }
    }
}
