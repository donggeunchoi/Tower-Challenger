using UnityEngine;
using UnityEngine.UI;

public class Story_14Floor : MonoBehaviour
{
    GameObject button;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StoryManager.storyInstance.storyTalk.BoolInit();

            button = Instantiate(StoryManager.storyInstance.storyUi.talkButton);
            button.GetComponent<Button>();
            button.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            button.SetActive(false);
        }
    }

    private void Start()
    {
        var b = StoryManager.storyInstance;

        if (StageManager.instance.floor != 14)
        {
            gameObject.SetActive(false);
            b.setCount = false;
            return;
        }
        
        if (b.setCount)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        b.setCount = true;
    }
}
