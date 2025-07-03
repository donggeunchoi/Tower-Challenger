using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShadowUI : MonoBehaviour
{
    public Image shadowImage;
    public Image[] choicesImage;

    public Image[] LP;
    public Sprite fullLP;
    public Sprite emptyLP;

    [HideInInspector] public int randomIndex;


    public void shadowGameInit()
    {
        shadowInti();
        shoicesInti();

        for (int i = 0; i < LP.Length; i++)
        {
            LP[i].sprite = fullLP;
        }
    }

    public void AnswerButton(int index)
    {
        ShadowManager.instance.shadow.Answer(index);
    }
    
    private void shadowInti()
    {
        randomIndex = Random.Range(0, ShadowManager.instance.shadowData.Length);
        var p = ShadowManager.instance.shadowData[randomIndex];// 랜덤으로 문제를 생성

        shadowImage.sprite = p.shadowSprite;

        StartCoroutine(ShadowFalse());
    }

    private void shoicesInti()
    {
        for (int i = 0; i < choicesImage.Length; i++)
        {
            choicesImage[i].sprite = ShadowManager.instance.shadowData[randomIndex].choices[i];
            choicesImage[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator ShadowFalse()
    {
        yield return new WaitForSeconds(ShadowManager.instance.shadowData[randomIndex].shadowTime);

        shadowImage.gameObject.SetActive(false);
        for (int i = 0; i < choicesImage.Length; i++)
        {
            choicesImage[i].gameObject.SetActive(true);
        }
    }
}
