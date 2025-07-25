using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Story : MonoBehaviour
{
    private SpriteRenderer sr;

    [HideInInspector] public int count;
    [HideInInspector] public List<StoryData> storys;

    public GameObject talkButton;

    public GameObject[] grassLayer_1;
    public GameObject[] grassLayer_2;

    private bool isTalking = false;
    private bool isButton = false;
    private int floor;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        count = 0;

        if (StageManager.instance.floor == 6)
        {
            Debug.Log(StageManager.instance.floor);
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

        Debug.Log(StoryManager.storyInstance.storyUi);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isTalking = true;
        if (isButton == false)
        {
            Instantiate(talkButton);
            isButton = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTalking = false;
        
        talkButton.SetActive(false);
    }

    public void DialogueButton()
    {
        Dialogue();
    }

    public void Dialogue()
    {
        Debug.Log(StageManager.instance.floor);

        Debug.Log(isTalking + " " + floor);
        Debug.Log(count + " " + storys[floor].lines.Length);

        Debug.Log(StoryManager.storyInstance.storyUi);

        var s = StoryManager.storyInstance.storyUi;
        
        if (isTalking == true && count < storys[floor].lines.Length)
        {
            Debug.Log("sss");
            s.backGround.SetActive(true);
            s.talk.text = storys[floor].lines[count].dialogueTest;
            s.image.sprite = storys[floor].lines[count].charImage;
            Debug.Log(storys[floor]);
        }
        else
        {
            s.backGround.SetActive(false);
            isTalking = false;
        }

        count++;
    }

    private void Layer()
    {
        for (int i = 0; i < grassLayer_1.Length; i++)
        {
            if (grassLayer_1[i].layer == 20)
            {
                this.gameObject.layer = grassLayer_1[i].layer;
                sr.sortingLayerName = "Layer 1";
            }
            else if(grassLayer_2[i].layer == 21)
            {
                this.gameObject.layer = grassLayer_2[i].layer;
                sr.sortingLayerName = "Layer 2";
            }
        }
    }
}