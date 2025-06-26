using System.Linq;
using UnityEngine;


public class Map : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private InteractionPortal[] nextStagePortal;
    [SerializeField] private GameObject startPlayerPosition;
    Vector3 playerPosition;

    StageManager stageManager;

    private void Awake()  //최초 1회만 실행하고싶은데 함수로 만들고 연결방법을 찾아야할꺼같은데..
    { 
        for (int i = 0; i < nextStagePortal.Length; i++) //일단 다 꺼주고
        {
            nextStagePortal[i].gameObject.SetActive(false);
        }
        
        //플레이어 소환 이것도 최초1회만 시행해야..
        playerPosition = startPlayerPosition.transform.position;                        //플레이어 위치를 받아서
        playerPrefab = Instantiate(playerPrefab, playerPosition, Quaternion.identity);  //플레이어를 해당위치로 놔주고
    }

    private void Start()
    {
        stageManager = StageManager.instance;  //스테이지 매니저에서 정보를 받아서

        //스테이지 카운트 만큼 포탈을 랜덤으로뽑아서 활성화
        for (int i = 0; i < stageManager.stageCount;  i++)
        {
            Random.Range(0, nextStagePortal.Length);
        }
        //하려고했는데 링큐가 더좋아보여서 링큐공부중...
        int[] randomPortal = Enumerable.Range(0, nextStagePortal.Length).OrderBy(x => Random.value).Take(stageManager.stageCount).ToArray();
        // Enumerable : 정수를 차례대로 추출 예시) 0 1 2 3[포탈이 네개 일 시]
        // OrderBy : 무작위로 섞기 3 1 2 0 이런식으로 섞이면
        // Take : 예시) 스테이지가 두개일시 3 1 추출
        // ToArray : 3, 1 반환  결론: randomPortal에는 3, 1이 들어있음

        //링큐로 추출한거만 활성화 시켜주고
        for (int i = 0; i < randomPortal.Length; i++)
        {

        }


        for (int i = 0; i < stageManager.stageClearPortal.Count; i++)  //스테이지 매니저에 있는 클리어 정보값을 받기
        {
            nextStagePortal[stageManager.stageClearPortal[i]].gameObject.SetActive(true);
        }
    }
}
