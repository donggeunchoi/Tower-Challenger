using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    StageManager stageManager;
    Vector3 playerPosition;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private InteractionPortal[] nextStagePortal;
    [SerializeField] private GameObject startPlayerPosition;
    public GameObject nextFloorPortal;
    [SerializeField] private GameObject tutorialPortal;
    [SerializeField] private GameObject tutorialBox;
    [SerializeField] private GameObject stairs;

    public GameObject mnyura_14;
    public GameObject mnyura;
    public GameObject myuraPrefab;

    public void Init()
    {
        StoryManager.storyInstance.storyTalk.isClear = false; // 아직 클리어 전

        playerPosition = startPlayerPosition.transform.position; // 빈 오브젝트로 플레이어가 생성되는 위치를 저장
        if (PlayerManager.Instance != null)
        {
            GameObject player = PlayerManager.Instance.playerPrefab;
            if (player != null)
            {
                PlayerManager.Instance.player = Instantiate(player, startPlayerPosition.transform.position, Quaternion.identity);
                PlayerManager.Instance.player.layer = startPlayerPosition.layer;
            }
        }
        if (StageManager.instance.floor == 14)
        {
            StoryManager.storyInstance.storyTalk.BoolInit();

            mnyura_14.SetActive(true);
            StoryManager.storyInstance.story_14Floor = mnyura_14.GetComponent<Story_14Floor>();
            StoryManager.storyInstance.storyTalk.SetPlayer(PlayerManager.Instance.player);
        }
        else if (StageManager.instance.floor == 30 && !StoryManager.storyInstance.storyTalk.isClear)
        {
            StoryManager.storyInstance.storyTalk.BoolInit();

            StoryManager.storyInstance.storyTalk.StoryInit();
            StoryManager.storyInstance.storyTalk.SetPlayer(PlayerManager.Instance.player);
        }
    }

    private void Start()
    {
        nextFloorPortal.SetActive(false);
        for (int i = 0; i < nextStagePortal.Length; i++) //일단 다 꺼주고
        {
            nextStagePortal[i].GetComponent<InteractionPortal>().portalNumber = i;
            nextStagePortal[i].gameObject.SetActive(false);
        }
        //playerPosition = startPlayerPosition.transform.position;//임시
        //Instantiate(playerPrefab, playerPosition, Quaternion.identity); //임시
        TutorialPortalClose();

        StartCoroutine(RandomPortal());
    }

    public IEnumerator RandomPortal()
    {
        while (stageManager == null)
        {
            stageManager = StageManager.instance;
            yield return null;
        }

        for (int i = 0; i < stageManager.stageClearPortal.Count; i++)  //스테이지 매니저에 있는 클리어 정보값을 받기
        {
            nextStagePortal[stageManager.stageClearPortal[i]].gameObject.SetActive(true);
        }
        yield return null;
    }

    public void SetRandomPortal()
    {
        if (StageManager.instance == null) Debug.Log(StageManager.instance.totalStageCount);
        
        StageManager.instance.ResetClearPortal();

        int[] randomPortal = Enumerable.Range(0, nextStagePortal.Length).OrderBy(x => Random.value).Take(StageManager.instance.totalStageCount).ToArray();
        // Enumerable : 정수를 차례대로 추출 예시) 0 1 2 3[포탈이 네개 일 시]
        // OrderBy x => Random.value : 무작위로 섞기 3 1 2 0 이런식으로 섞이면
        // Take : 예시) 스테이지가 두개일시 3 1 추출
        // ToArray : 3, 1 반환  결론: randomPortal에는 3, 1이 들어있음

        //OrderBy 종류에대에서는 이 외에도
        //x => x 오름차순 정렬, word => word.Length 등 //글자수 
        //추출할값 => 정렬기준 으로 써서 그외에 것들도 오더로 내릴 수 있게됩니다
        //그외에도 ThenBy로 추가 오더를 내릴 수 있습니다  OrderBy(x => word.Length).ThenBy(x => Age.age) 이름수와 나이순으로 정렬
        for (int i = 0; i < randomPortal.Length; i++)
        {
            StageManager.instance.AddPortal(randomPortal[i]);
        }
    }

    public void AllClearFloor()
    {
        if (PlayerManager.Instance.player == null)
            PlayerManager.Instance.PlayerSetting();

        StoryManager.storyInstance.storyTalk.isClear = true;

        if (StageManager.instance.floor == 6)
        {
            StoryManager.storyInstance.storyTalk.BoolInit();

            Vector3 spawnPos = nextFloorPortal.transform.position + new Vector3(0, 0, 0); // 포탈 위치 반환
            myuraPrefab = Instantiate(mnyura, spawnPos, Quaternion.identity); // 포탈 위치에 프리펩 생성
            
            StoryManager.storyInstance.story = myuraPrefab.GetComponent<Story>(); // 프리펩으로 변경
        }
        else if (StageManager.instance.floor == 10)
        {
            StoryManager.storyInstance.storyTalk.BoolInit();
            StoryManager.storyInstance.storyTalk.StoryInit();
            StoryManager.storyInstance.storyTalk.SetPlayer(PlayerManager.Instance.player);
        }
        else if (StageManager.instance.floor == 20)
        {
            StoryManager.storyInstance.storyTalk.BoolInit();
            StoryManager.storyInstance.storyTalk.StoryInit();
            StoryManager.storyInstance.storyTalk.SetPlayer(PlayerManager.Instance.player);
        }
        else if (StageManager.instance.floor == 30 && StoryManager.storyInstance.storyTalk.isClear)
        {
            StoryManager.storyInstance.storyTalk.BoolInit();
            StoryManager.storyInstance.storyTalk.StoryInit();
            StoryManager.storyInstance.storyTalk.SetPlayer(PlayerManager.Instance.player);
        }

        else { nextFloorPortal.SetActive(true); }
    }

    public void TutorialPortalClose()
    {
        if (tutorialPortal != null)
        {
            tutorialPortal.SetActive(false);
        }
    }

    public void TutorialPortalOpen()
    {
        if (tutorialPortal != null)
        {
            tutorialPortal.SetActive(true);
        }
    }
}
