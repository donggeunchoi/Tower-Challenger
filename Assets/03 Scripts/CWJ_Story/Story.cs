using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Story : MonoBehaviour, IInteractable
{
    private PlayerInput playerInput;
    private StoryUi ui;

    private SpriteRenderer sr;
    private SpriteRenderer potalSr;
    private Map map;

    public GameObject storyPrefab;

    [HideInInspector] public int count;
    [HideInInspector] public List<StoryData> storys;

    private GameObject story;
    private GameObject button;
    private bool isTalking;
    private bool isButton;
    private int floor;

    private void Awake()
    {
        map = GameObject.FindAnyObjectByType<Map>();
    }

    private void OnEnable()
    {
        potalSr = map.nextFloorPortal.gameObject.GetComponent<SpriteRenderer>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        count = 0;
        isTalking = false;
        isButton = false;

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
        Layer();
        Debug.Log(StoryManager.storyInstance.storyUi);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInput = collision.gameObject.GetComponentInChildren<PlayerInput>();  
        }
    }

    public void Dialogue()
    {
        isTalking = true;

        if (isTalking == true && count < storys[floor].lines.Length)
        {
            ui.talk.text = storys[floor].lines[count].dialogueTest;
            ui.image.sprite = storys[floor].lines[count].charImage;
        }
        else
        {
            story.SetActive(false);
            isTalking = false;
            playerInput.ismove = true;
        }

        count++;
    }

    private void Layer()
    {
        if (map.nextFloorPortal.layer == 20)
        {
            this.gameObject.layer = 20;
            sr.sortingLayerName = potalSr.sortingLayerName;
        }
        else if(map.nextFloorPortal.layer == 21)
        {
            this.gameObject.layer = 21;
            sr.sortingLayerName = potalSr.sortingLayerName;
        }
        else if(map.nextFloorPortal.layer == 22)
        {
            this.gameObject.layer = 22;
            sr.sortingLayerName = potalSr.sortingLayerName;
        }
    }

    public void Interact()
    {
        if (!isButton)
        {
            story = Instantiate(StoryManager.storyInstance.storyUi.canvas);
            story.SetActive (true);
            ui = story.GetComponent<StoryUi>();
            isButton = true;
        }
        else
        {
            playerInput.ismove = false;
            Dialogue();
        }

    }
}