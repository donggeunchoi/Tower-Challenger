using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class StoryTalk : MonoBehaviour, IInteractable
{
    [HideInInspector] public Map map;

    private Button interactionButton;          //상호작용 버튼
    [SerializeField]private StoryUi ui;
    private GameObject story; // 프리펩 복사본

    public bool isButton; // 버튼을 눌렀는지 여부
    public bool isClear; // 클리어 했는지 여부(30층에서 사용)
    private bool isTalking; // 스토리 진행중인지 여부
    private int storyFloor;

    [HideInInspector] public int count;
    [HideInInspector] public List<StoryData> storys;

    private void Start()
    {
        isButton = false;
        isClear = false;
    }
    public void SetPlayer(GameObject _player)
    {
        Transform button = FindDeepChild(_player.transform, "InteractionButton ");
        button.GetComponent<Button>().onClick.AddListener(Interact);
    }
    private Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;
            var result = FindDeepChild(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
    public void Dialogue()
    {
        PlayerManager.Instance.isMove = false;
        isTalking = true;
        count++;
        
        if (isTalking == true && count < storys[storyFloor].lines.Length)
        {
            ui.talk.text = storys[storyFloor].lines[count].dialogueTest;
            ui.image.sprite = storys[storyFloor].lines[count].charImage;
        }
        else
        {
            story.SetActive(false);
            isTalking = false;
            PlayerManager.Instance.isMove = true;

            if (map.pPrefab != null)
            {
                map.pPrefab.SetActive(false);
                map.nextFloorPortal.SetActive(true);
            }
            else
            {
                map.nextFloorPortal.SetActive(true);
            }
        }
    }

    public void StoryInit()
    {
        map = GameObject.FindAnyObjectByType<Map>();

        if (StageManager.instance.floor == 6) storyFloor = 0;
        else if (StageManager.instance.floor == 10) storyFloor = 1;
        else if (StageManager.instance.floor == 14) storyFloor = 2;
        else if (StageManager.instance.floor == 20) storyFloor = 3;
        else if (StageManager.instance.floor == 30 && !isClear) storyFloor = 4;
        else if (StageManager.instance.floor == 30 && isClear) storyFloor = 5;

        count = 0;
        isTalking = false;

        // 스토리 대화 프리펩을 생성
        story = Instantiate(StoryManager.storyInstance.storyUi.canvas);

        ui = story.GetComponent<StoryUi>();
        if (ui.talk.text != null && storys[storyFloor].lines[count].dialogueTest != null)
            ui.talk.text = storys[storyFloor].lines[count].dialogueTest;
        if (ui.image.sprite != null && storys[storyFloor].lines[count].charImage != null)
            ui.image.sprite = storys[storyFloor].lines[count].charImage;
        //ui.talk.text = storys[storyFloor].lines[count].dialogueTest;
        //ui.image.sprite = storys[storyFloor].lines[count].charImage;

        isButton = true;
    }
    public void Interact()
    {
        // 
        if (!isButton)
        {
            Debug.Log("스토리 초기화");
            StoryInit();
        }
        else
        {
            Debug.Log("스토리 대화진행");
            Dialogue();
        }
    }
}
