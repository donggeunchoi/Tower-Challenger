using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTalk : MonoBehaviour
{
    public Map map;
    public List<StoryData> storyList;
    public GameObject button;

    private Button interactionButton;          // 대화 상호 작용 버튼
    [SerializeField] private StoryUi prefabUi; // 프리펩의 ui를 담을 변수
    private GameObject storyCanvas;            // 대화 UI복사본을 담는 변수

    public bool isTalk;         // 스토리가 시작했는지
    public bool isClear;        // 클리어 했는지 여부(30층에서 사용)
    private bool isDialogue;    // 대화가 시작했는지
    private int storyFloor;
    private int count;          // 다음 대사로 넘기는 숫자

    private void Start()
    {
        BoolInit();
    }
    //public void SetPlayer(GameObject _player) // 플레이어 오브젝트 안에있는 버튼을 찾는 함수
    //{
    //    Transform button = FindDeepChild(_player.transform, "InteractionButton ");
    //    button.GetComponent<Button>().onClick.AddListener(Interact);
    //}

    //private Transform FindDeepChild(Transform parent, string name)
    //{
    //    foreach (Transform child in parent)
    //    {
    //        if (child.name == name)
    //            return child;
    //        var result = FindDeepChild(child, name);
    //        if (result != null)
    //            return result;
    //    }
    //    return null;
    //}
    public void Dialogue()
    {
        Debug.Log(isDialogue + ", " + count + ", " + storyList[storyFloor].lines.Length);
        isDialogue = true; // 대화 시작
        count++; // 다음 대사로 넘어감

        if (isDialogue && count < storyList[storyFloor].lines.Length)
        {
            Debug.Log("다음 대사");
            if (prefabUi.talk.text != null && storyList[storyFloor].lines[count].dialogueTest != null)
                prefabUi.talk.text = storyList[storyFloor].lines[count].dialogueTest;

            if (prefabUi.image.sprite != null && storyList[storyFloor].lines[count].charImage != null)
            prefabUi.image.sprite = storyList[storyFloor].lines[count].charImage;
        }
        else
        {
            if (storyCanvas != null)
                storyCanvas.SetActive(false);

            if (map.myuraPrefab != null && map != null)
            {
                if (StageManager.instance.floor != 14)
                {
                    map.myuraPrefab.SetActive(false);
                    map.nextFloorPortal.SetActive(true);
                }
            }
            else if(map != null)
            {
                if (StageManager.instance.floor != 14)
                    map.nextFloorPortal.SetActive(true);
            }
            map.mnyura_14.SetActive(false);

            if (StageManager.instance.floor != 6 && StageManager.instance.floor != 14)
            {
                button.SetActive(false);
            }
            // 초기화
            PlayerManager.Instance.isMove = true;
        }
    }

    public void StoryInit()
    {
        map = GameObject.FindAnyObjectByType<Map>();

        // 플레이어의 움직임을 멈춰
        PlayerManager.Instance.isMove = false;

        // 층에 따라 대화 배열을 변경
        if (StageManager.instance.floor == 6) storyFloor = 0;
        else if (StageManager.instance.floor == 10) storyFloor = 1;
        else if (StageManager.instance.floor == 14) storyFloor = 2;
        else if (StageManager.instance.floor == 20) storyFloor = 3;
        else if (StageManager.instance.floor == 30 && !isClear) storyFloor = 4;
        else if (StageManager.instance.floor == 30 && isClear) storyFloor = 5;

        // 스토리 대화 프리펩을 생성
        storyCanvas = Instantiate(StoryManager.storyInstance.storyUi.canvas);

        // 복사본의 UI를 할당
        prefabUi = storyCanvas.GetComponent<StoryUi>();
        
        // 가장 첫번 째의 대화 텍스트와 이미지를 생성
        if (prefabUi.talk.text != null && storyList[storyFloor].lines[count].dialogueTest != null)
        {
            prefabUi.talk.text = storyList[storyFloor].lines[count].dialogueTest;
        }
        if (prefabUi.image.sprite != null && storyList[storyFloor].lines[count].charImage != null)
        {
            prefabUi.image.sprite = storyList[storyFloor].lines[count].charImage;
        }

        if (StageManager.instance.floor != 6 && StageManager.instance.floor != 14)
        {
            button = Instantiate(StoryManager.storyInstance.storyUi.talkButton);
            button.GetComponent<Button>();
            button.SetActive(true);
        }

        // 스토리가 시작했는지 여부
        isTalk = true;
    }

    public void BoolInit()
    {
        isDialogue = false;
        isTalk = false;
        isClear = false;
        count = 0;
    }
}
