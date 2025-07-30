using UnityEngine;

public class Story_14Floor : MonoBehaviour
{
    public GameObject myura_14;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StoryManager.storyInstance.storyTalk.BoolInit();
        }
    }

    private void Start()
    {
        if (StageManager.instance.floor != 14)
        {
            myura_14.SetActive(false);
        }
        else
        {
            myura_14.SetActive(true);
        }
    }
}
