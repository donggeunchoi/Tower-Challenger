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
    public float fadeDuration = 1.0f;

    private bool videoFinished = false;
    private bool videoStarted = false;

    void Start()
    {
        videoPanel.SetActive(false);
        fadeImage.gameObject.SetActive(false);
        fadeImage.color = new Color(0, 0, 0, 0);

        startButton.onClick.AddListener(OnStartButtonClick);

        videoPlayer.loopPointReached += OnVideoEnd;
        videoFinished = false;
        videoStarted = false;
    }

    private void OnStartButtonClick()
    {
        StartCoroutine(PlayVideoSequence());
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (videoFinished) return;
        videoFinished = true;
        StartCoroutine(EndSequenceAndLoadScene());
    }

    void Update()
    {
        // 혹시 이벤트가 안 들어왔을 때, 마지막 프레임 감시 - "영상이 끝과 동시에 씬 전환"
        if (!videoFinished && videoStarted && videoPlayer.isPlaying && videoPlayer.frame > 0 &&
            videoPlayer.frame >= (long)videoPlayer.frameCount - 1)
        {
            OnVideoEnd(videoPlayer);
        }
    }

    IEnumerator PlayVideoSequence()
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        videoPanel.SetActive(true);
        startPanel.SetActive(false);

        videoFinished = false;
        videoStarted = true;
        videoPlayer.Play();

        yield return StartCoroutine(FadeOut());

        // 영상 끝날 때까지 대기(타임아웃 병행, 영상 길이+2초)
        float maxWait = (float)videoPlayer.length + 2.0f;
        float timer = 0f;
        while (!videoFinished && timer < maxWait)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        // 만약 끝까지 안 들어오면 강제 종료
        if (!videoFinished)
        {
            OnVideoEnd(videoPlayer);
        }
    }

    IEnumerator EndSequenceAndLoadScene()
    {
        videoPanel.SetActive(false);

        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("VillageScene");
    }

    IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1);
    }

    IEnumerator FadeOut()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(false);
    }
}