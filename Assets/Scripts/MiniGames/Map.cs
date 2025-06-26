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
    }

    private void Start()
    {
        for (int i = 0; i < nextStagePortal.Length; i++) //일단 다 꺼주고
        {
            nextStagePortal[i].GetComponent<InteractionPortal>().portalNumber = i;
            nextStagePortal[i].gameObject.SetActive(false);
        }
        stageManager = StageManager.instance;  //스테이지 매니저에서 정보를 받아서
        
        for (int i = 0; i < stageManager.stageClearPortal.Count; i++)  //스테이지 매니저에 있는 클리어 정보값을 받기
        {
            nextStagePortal[stageManager.stageClearPortal[i]].gameObject.SetActive(true);
        }
    }

    public void SetRandomPortal()
    {
        int[] randomPortal = Enumerable.Range(0, nextStagePortal.Length).OrderBy(x => Random.value).Take(stageManager.stageCount).ToArray();
        for (int i = 0; i < randomPortal.Length; i++)
        {
            stageManager.AddPortal(i);
        }
    }
}
