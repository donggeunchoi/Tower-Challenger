using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;

public class SellGameManager : MonoBehaviour
{

    public GameObject FadeIn, Blocker; //페이드인 브로커
    public GameObject StartGameBtn;

    public Button[] buttons; // UI에 배치된 버튼들

    public Animator anim;//당청 카드 보여주기
    private RectTransform[] rects; // 각 버튼의 RectTransform 컴포넌트 참조
    private Vector3[] positions;   // 버튼들의 원래 위치 저장
    private float duration = 0.15f; // 버튼 이동 애니메이션 시간
    private float WaitShuffle = 0.05f; //카드 대기시간
    private int count = 20;         // 버튼 셔플 횟수

    void Start()
    {
        

        int len = buttons.Length;
        rects = new RectTransform[len];
        positions = new Vector3[len];

        for (int i = 0; i < len; i++)
        {
            // 각 버튼의 RectTransform 가져오고 원래 위치 저장
            rects[i] = buttons[i].GetComponent<RectTransform>();
            positions[i] = rects[i].anchoredPosition;

            // 인덱스 캡처하여 클릭 이벤트 등록
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        // 게임 시작 시 버튼들을 셔플
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        Blocker.SetActive(true);         
        StartGameBtn.SetActive(false);
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1);
        FadeIn.SetActive(false);
        yield return new WaitForSeconds(1);
        anim.SetTrigger("ThisCard");
        yield return new WaitForSeconds(2);
        StartGameBtn.SetActive(true);
        Blocker.SetActive(false);        
    }
    public void StartGameBtnClick()
    {
        Blocker.SetActive(true);        
        StartGameBtn.SetActive(false);
        StartCoroutine(ShuffleRoutine(count));
    }
    IEnumerator OnWrongAnswer()
    {
        Blocker.SetActive(true);
        anim.SetTrigger("ThisCard");
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(ShuffleRoutine(count));
        Blocker.SetActive(false);
    }

    // 버튼 클릭 시 호출되는 함수
    void OnButtonClick(int index)
    {
        if (index == 0)
        {
            Debug.Log("클"); 
        }
        else
        {
            
            StartCoroutine(OnWrongAnswer());

        }
    }

    // 지정된 횟수만큼 셔플 루틴 반복
    IEnumerator ShuffleRoutine(int times)
    {
        for (int i = 0; i < times; i++)
        {
            yield return StartCoroutine(ShuffleOnce());
            yield return new WaitForSeconds(WaitShuffle);
        }

        Blocker.SetActive(false);
    }

    // 버튼 위치를 한번 셔플하는 루틴
    IEnumerator ShuffleOnce()
    {
        int len = buttons.Length;
        int[] order = new int[len]; // 버튼 인덱스 배열

        for (int i = 0; i < len; i++)
            order[i] = i; // 초기 순서 저장


        int[] original = (int[])order.Clone(); // 현재 순서 백업

        
        do
        {
            Shuffle(order);
            
        } while (IsSame(order, original));

        // 각 버튼의 위치를 새 위치로 이동
        for (int i = 0; i < len; i++)
            StartCoroutine(Move(rects[i], positions[order[i]]));

        yield return new WaitForSeconds(duration);
    }

    // 부드럽게 버튼 위치를 이동시키는 코루틴
    IEnumerator Move(RectTransform rect, Vector3 target)
    {
        Vector3 start = rect.anchoredPosition;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            rect.anchoredPosition = Vector3.Lerp(start, target, t / duration); // 선형 보간
            yield return null;
        }

        rect.anchoredPosition = target; // 마지막 위치 보정
    }

    // 배열을 무작위로 섞는 함수
    void Shuffle(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            int r = Random.Range(i, arr.Length); // i~끝까지 중 랜덤 선택
            (arr[i], arr[r]) = (arr[r], arr[i]); // 자리 교환
        }
    }

    // 두 배열이 같은 순서인지 비교
    bool IsSame(int[] a, int[] b)
    {
        for (int i = 0; i < a.Length; i++)
            if (a[i] != b[i]) return false;
        return true;
    }
}