using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StoryManager : MonoBehaviour
{
    public static StoryManager storyInstance;
    public Story story;
    public StoryUi storyUi;

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
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        story.storys = new List<StoryData>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        image = storyUi.image;
        textTalk = storyUi.talk;
        backGround = storyUi.backGround;

        for (int i = 0; i < data.Length; i++)
        {
            story.storys.Add(data[i]);
        }

        backGround.SetActive(false);
        story.count = 0;
    }
}
