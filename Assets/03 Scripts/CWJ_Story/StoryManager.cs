using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StoryManager : MonoBehaviour
{
    public static StoryManager storyInstance;
    public Story story;
    public StoryUi storyUi;
    public StoryTalk storyTalk;
    public Story_14Floor story_14Floor;

    public StoryData[] data;

    private GameObject backGround;
    private Image image;
    private TextMeshProUGUI textTalk;

    private void Awake()
    {
        if (storyInstance == null)
        {
            storyInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        storyTalk.storyList = new List<StoryData>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        for (int i = 0; i < data.Length; i++)
        {
            storyTalk.storyList.Add(data[i]);
        }

        image = storyUi.image;
        textTalk = storyUi.talk;
        backGround = storyUi.canvas;
    }
}
