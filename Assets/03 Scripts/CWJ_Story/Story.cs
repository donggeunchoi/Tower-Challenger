using System.Collections.Generic;
using UnityEngine;


public class Story : MonoBehaviour
{
    [HideInInspector] public int count;
    [HideInInspector] public List<StoryData> storys;
    [HideInInspector] public float posY;

    private bool isTalking = false;
    private bool isClear = false;
    private int floor;

    private void Start()
    {
        if (StageManager.instance.floor == 6)
        {
            floor = 0;
        }
        else if (StageManager.instance.floor == 10)
        {
            floor = 1;
        }
        else if (StageManager.instance.floor == 14)
        {
            floor = 2;
        }
        else if (StageManager.instance.floor == 20)
        {
            floor = 3;
        }
        else if (StageManager.instance.floor == 30)
        {
            if (isClear)
            {
                floor = 5;
                return;
            }    
            floor = 4;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTalking)
        {
            isTalking = true;
        }
    }

    public void DialogueButton()
    {
        Dialogue();
    }

    public void Dialogue()
    {
        var s = StoryManager.storyInstance.storyUi;

        if (isTalking == true && count < storys[floor].lines.Length)
        {
            s.backGround.SetActive(true);
            s.talk.text = storys[floor].lines[count].dialogueTest;
            s.image.sprite = storys[floor].lines[count].charImage;
        }
        else
        {
            s.backGround.SetActive(false);
            isTalking = false;
        }

        count++;
    }
}