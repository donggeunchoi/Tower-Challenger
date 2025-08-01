using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryUi : MonoBehaviour
{
    public TextMeshProUGUI talk;
    public Image image;
    public GameObject canvas;

    public GameObject talkButton;
    
    public void OnButton()
    {
        var s = StoryManager.storyInstance.storyTalk;
        if (!s.isTalk) // 대화가 시작 안했다면
        {
            s.StoryInit();
        }
        else
        {
            Debug.Log("클릭 대화");

            s.Dialogue();
        }
    }
}