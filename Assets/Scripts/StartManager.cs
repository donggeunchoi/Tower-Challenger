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

    void Start()
    {
        videoPanel.SetActive(false);
        startButton.onClick.AddListener(PlayIntroVideo);
    }

    public void PlayIntroVideo()
    {
        videoPanel.SetActive(true);
        startPanel.SetActive(false);
        videoPlayer.Play();
        StartCoroutine(WaitforVideoToEnd());
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
    // public void OnclickStart()
    // {
    //     SceneManager.LoadScene("VillageScene");
    // }
}
