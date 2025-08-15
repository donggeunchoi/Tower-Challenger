using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("튜토리얼 단계 리스트")] [SerializeField]
    private List<GameObject> tutorialPrefabs;

    private TutorialBase currentTutorial;
    private GameObject cuttentInstance;
    private int currentIndex = -1;
    
    public Image fadeImage;
    public float fadeDuration;

	public GameObject dashWall;


    void Start()
    {
        StartCoroutine(FadeOut());
        NextStep();
    }

    void Update()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Play(this);
        }
    }

    public void NextStep()
    {
        
        if (currentTutorial != null)
        {
            currentTutorial.Exit();
            Destroy(cuttentInstance);
        }

        currentIndex++;
        
        if (currentIndex >= tutorialPrefabs.Count)
        {
            OnAllTutorialsCompleted();
            return;
        }
        
        cuttentInstance = Instantiate(tutorialPrefabs[currentIndex]);
        currentTutorial = cuttentInstance.GetComponent<TutorialBase>();

        var bt = cuttentInstance.GetComponent<BoxTutorial>();

        HandleDashWall();
       
        currentTutorial.Enter();
    }

    private void OnAllTutorialsCompleted()
    {
        currentTutorial = null;
    }

    private void HandleDashWall()
    {
        if (currentIndex == 2)
        {
            if (dashWall != null)
                dashWall.SetActive(false);
        }
        else
        {
            if (dashWall != null)
                dashWall.SetActive(true);
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
