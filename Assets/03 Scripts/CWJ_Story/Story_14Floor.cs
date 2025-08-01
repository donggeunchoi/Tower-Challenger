using UnityEngine;
using UnityEngine.UI;

public class Story_14Floor : MonoBehaviour
{
    GameObject button;
    static public bool setCount = false;

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
        if (StageManager.instance.floor != 14)
        {
            gameObject.SetActive(false);
            setCount = false;
        }
        else
        {
            Debug.Log("숫자 : " + setCount);
            if (setCount == false)
            {
                gameObject.SetActive(true);
                setCount = true;
            }
        }
    }
}
