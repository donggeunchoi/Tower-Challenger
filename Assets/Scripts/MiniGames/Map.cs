using System.Linq;
using UnityEngine;


public class Map : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private InteractionPortal[] nextStagePortal;
    [SerializeField] private GameObject startPlayerPosition;
    [SerializeField] private GameObject nextFloorPortal;
    Vector3 playerPosition;

    StageManager stageManager;

    private void Awake()
    {
        playerPosition = startPlayerPosition.transform.position;
        Instantiate(playerPrefab, playerPosition, Quaternion.identity);
    }

    private void Start()
    {
        for (int i = 0; i < nextStagePortal.Length; i++) //일단 다 꺼주고
        {
            nextStagePortal[i].GetComponent<InteractionPortal>().portalNumber = i;
            nextStagePortal[i].gameObject.SetActive(false);
        }
        stageManager = StageManager.instance;  //스테이지 매니저에서 정보를 받아서
        if (stageManager != null)
        {
            for (int i = 0; i < stageManager.stageClearPortal.Count; i++)  //스테이지 매니저에 있는 클리어 정보값을 받기
            {
                nextStagePortal[stageManager.stageClearPortal[i]].gameObject.SetActive(true);
            }
        }
    }

    public void SetRandomPortal()
    {
        int[] randomPortal = Enumerable.Range(0, nextStagePortal.Length).OrderBy(x => Random.value).Take(stageManager.totalStageCount).ToArray();
        // Enumerable : 정수를 차례대로 추출 예시) 0 1 2 3[포탈이 네개 일 시]
        // OrderBy : 무작위로 섞기 3 1 2 0 이런식으로 섞이면
        // Take : 예시) 스테이지가 두개일시 3 1 추출
        // ToArray : 3, 1 반환  결론: randomPortal에는 3, 1이 들어있음

        for (int i = 0; i < randomPortal.Length; i++)
        {
            stageManager.AddPortal(i);
        }
    }
}
