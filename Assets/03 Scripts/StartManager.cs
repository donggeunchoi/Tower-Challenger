using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartManager : MonoBehaviour
{
    public GameObject videoPanel;
    public VideoPlayer videoPlayer;
    public Button startButton;
    public GameObject startPanel;
    public Image fadeImage;
    public float fadeDuration;

    void Start()
    {
        videoPanel.SetActive(false);
        fadeImage.gameObject.SetActive(false);
        fadeImage.color = new Color(0, 0, 0, 0);
        
        startButton.onClick.AddListener(OnStartButtonClick);
        
    }

    private void OnStartButtonClick()
    {
        StartCoroutine(PlayVideoSequence());
    }

    IEnumerator PlayVideoSequence()
    {
        fadeImage.gameObject.SetActive(true);
        
        yield return StartCoroutine(FadeIn());
        
        videoPanel.SetActive(true);
        startPanel.SetActive(false);
        videoPlayer.Play();
        
        yield return StartCoroutine(FadeOut());

        yield return StartCoroutine(WaitforVideoToEnd());
    }

    IEnumerator WaitforVideoToEnd()
    {
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        
        videoPanel.SetActive(false);
        
        SceneManager.LoadScene("VillageScene");
    }

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
    
}
