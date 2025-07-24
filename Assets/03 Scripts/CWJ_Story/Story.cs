using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    public StoryData[] data;

    public TextMeshProUGUI talk;
    public Image image;
    public GameObject backGround;

    private bool isTalking = false;
    private int count;
    private int floor;

    private List<StoryData> storys;
    private bool isClear = false;
    private void OnEnable()
    {
        storys = new List<StoryData>();
    }

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
        for (int i = 0; i < data.Length; i++)
        {
            storys.Add(data[i]);
            Debug.Log(storys[i]);
        }
        
        backGround.SetActive(false);
        count = 0;
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
        if (isTalking == true && count < storys[floor].lines.Length)
        {
            backGround.SetActive(true);
            talk.text = storys[floor].lines[count].dialogueTest;
            image.sprite = storys[floor].lines[count].charImage;
        }
        else
        {
            backGround.SetActive(false);
            isTalking = false;
        }
        count++;
    }
}
