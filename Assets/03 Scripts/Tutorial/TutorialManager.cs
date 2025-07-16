using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration;
    public List<GameObject> tutorialTextUI;

    private int currentTutorial = 0;
    private bool isWaitingForInput = false;

    
    private void Start()
    {
        foreach (GameObject obj in tutorialTextUI)
        {
            obj.SetActive(false);
        }
        
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        yield return StartCoroutine(FadeOut());
        tutorialTextUI[0].SetActive(true);
        isWaitingForInput = true;
    }

    private void ShowOnlyPanel(int index)
    {
        for (int i = 0; i < tutorialTextUI.Count; i++)
        {
            tutorialTextUI[i].SetActive(i == index);
        }
    }

    IEnumerator EndTutorial()
    {
        yield return StartCoroutine(FadeIn());
    }
    
    
    public void TriggerNextPanel()
    {
        if (isWaitingForInput)
        {
            isWaitingForInput = false;
            currentTutorial++;

            if (currentTutorial < tutorialTextUI.Count)
            {
                ShowOnlyPanel(currentTutorial);
                isWaitingForInput = true;
            }
            else
            {
                Debug.Log("이제 포탈을 이용해야할때");
            }
        }
    }

    #region Fade
    
    IEnumerator FadeIn()//투명에서 검은거로 
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0,1, time/fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        
        fadeImage.color = new Color(0, 0, 0, 1);
    }

    IEnumerator FadeOut()//검은거에서 투명으로
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, time/fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);
    }

        #endregion
}
