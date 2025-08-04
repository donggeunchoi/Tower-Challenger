using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Story : MonoBehaviour
{
    private Map map;
    GameObject button;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StoryManager.storyInstance.storyTalk.BoolInit();

            button = Instantiate(StoryManager.storyInstance.storyUi.talkButton);
            button.GetComponent<Button>();
            button.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            button.SetActive(false);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //     //StoryManager.storyInstance.storyTalk.SetPlayer(PlayerManager.Instance.player);
    //}

    private void Start()
    {
        Layer();
    }

    private void Layer()
    {
        sr.sortingLayerName = potalSr.sortingLayerName;
    }
}