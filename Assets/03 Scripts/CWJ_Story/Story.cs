using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Story : MonoBehaviour
{
    private Map map;

    private SpriteRenderer sr;
    private SpriteRenderer potalSr;

    private void Awake()
    {
        map = GameObject.FindAnyObjectByType<Map>();
    }
    private void OnEnable()
    {
        // 다음 층으로 가는 오브젝트의 렌더러 컴퍼넌트를 참조
        potalSr = map.nextFloorPortal.gameObject.GetComponent<SpriteRenderer>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Layer();
    }

    private void Layer()
    {
        // 소환될 때 레이어 변경
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

    //public void Interact()
    //{
    //    Debug.Log(StoryManager.storyInstance.storyTalk.isButton);
    //    if (!StoryManager.storyInstance.storyTalk.isButton)
    //    {
    //        Debug.Log("스토리스크립트 초기화");
    //        StoryManager.storyInstance.storyTalk.StoryInit();
    //    }
    //    else
    //    {
    //        Debug.Log("스토리스크립트 대화진행");
    //        StoryManager.storyInstance.storyTalk.Dialogue();
    //    }
    //}
}