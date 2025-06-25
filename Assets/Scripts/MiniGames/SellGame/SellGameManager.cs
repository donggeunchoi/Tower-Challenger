using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;

public class SellGameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject fadeIn;            // 페이드 인 오브젝트
    public GameObject Blocker;          // 입력을 막는 블로커 오브젝트
    public GameObject StartGameBtn;     // 게임 시작 버튼
    public Button[] buttons;            // 선택 가능한 카드 버튼들
    public Animator animator;           // 애니메이션을 위한 애니메이터

    [Header("Settings")]
    public float shuffleDuration = 0.15f;        // 한 번 셔플 시 애니메이션 시간
    public float waitBetweenShuffles = 0.05f;    // 셔플 간 대기 시간
    public int shuffleCount = 20;                // 총 셔플 횟수

    private RectTransform[] rects;               // 버튼들의 RectTransform 참조
    private Vector3[] originalPositions;         // 버튼들의 원래 위치 저장
    StageManager stageManager;                   // StageManager 인스턴스 참조

    void Start()
    {
        stageManager = StageManager.instance;    // StageManager 싱글톤 참조
        InitializeButtons();                     // 버튼 초기화 및 리스너 설정
        StartCoroutine(GameIntroSequence());     // 게임 인트로 코루틴 시작
    }

    // 버튼들을 초기화하고 클릭 이벤트 리스너를 설정함
    void InitializeButtons()
    {
        int len = buttons.Length;
        rects = new RectTransform[len];
        originalPositions = new Vector3[len];

        for (int i = 0; i < len; i++)
        {
            rects[i] = buttons[i].GetComponent<RectTransform>();
            originalPositions[i] = rects[i].anchoredPosition;

            int index = i; // 캡처 변수
            buttons[i].onClick.AddListener(() => OnButtonClick(index)); // 버튼 클릭 이벤트
        }
    }

    // 게임 시작 전 인트로 시퀀스를 실행하는 코루틴
    IEnumerator GameIntroSequence()
    {
        Blocker.SetActive(true);      // 입력 막기
        StartGameBtn.SetActive(false); // 시작 버튼 숨기기
        fadeIn.SetActive(true);       // 페이드인 효과

        yield return new WaitForSeconds(1f);
        fadeIn.SetActive(false);      // 페이드인 제거

        yield return new WaitForSeconds(1f);
        animator.SetTrigger("ThisCard"); // 정답 카드 애니메이션 재생

        yield return new WaitForSeconds(2f);
        StartGameBtn.SetActive(true);  // 시작 버튼 보이기
        Blocker.SetActive(false);      // 입력 허용
    }

    // 시작 버튼 클릭 시 실행되는 함수
    public void OnStartGameButtonClicked()
    {
        Blocker.SetActive(true);
        StartGameBtn.SetActive(false);
        StartCoroutine(ShuffleMultipleTimes(shuffleCount)); // 셔플 시작
    }

    // 버튼 클릭 시 처리
    void OnButtonClick(int index)
    {
        if (index == 0)
        {
            Debug.Log("클리어!"); // 정답
        }
        else
        {
            stageManager.LPdown(); // 오답 시 라이프 감소
            StartCoroutine(OnWrongAnswer()); // 오답 처리 코루틴 시작
        }
    }

    // 오답 처리 코루틴
    IEnumerator OnWrongAnswer()
    {
        Blocker.SetActive(true);              // 입력 차단
        animator.SetTrigger("ThisCard");      // 정답 카드 다시 보여주기

        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(ShuffleMultipleTimes(shuffleCount)); // 다시 셔플

        Blocker.SetActive(false);             // 입력 허용
    }

    // 여러 번 셔플을 반복하는 코루틴
    IEnumerator ShuffleMultipleTimes(int times)
    {
        for (int i = 0; i < times; i++)
        {
            yield return StartCoroutine(ShuffleOnce()); // 한 번 셔플
            yield return new WaitForSeconds(waitBetweenShuffles); // 대기
        }

        Blocker.SetActive(false); // 셔플 후 입력 허용
    }

    // 한 번의 셔플을 수행하는 코루틴
    IEnumerator ShuffleOnce()
    {
        int len = buttons.Length;
        int[] order = Enumerable.Range(0, len).ToArray(); // 0~n 인덱스 배열
        int[] original = (int[])order.Clone();            // 원본 복사

        // 같은 순서가 나오지 않도록 셔플
        do
        {
            ShuffleArray(order);
        } while (IsSameOrder(order, original));

        // 각 버튼을 새 위치로 이동
        for (int i = 0; i < len; i++)
            StartCoroutine(MoveSmoothly(rects[i], originalPositions[order[i]]));

        yield return new WaitForSeconds(shuffleDuration);
    }

    // RectTransform을 부드럽게 target 위치로 이동시키는 코루틴
    IEnumerator MoveSmoothly(RectTransform rect, Vector3 target)
    {
        Vector3 start = rect.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < shuffleDuration)
        {
            elapsed += Time.deltaTime;
            rect.anchoredPosition = Vector3.Lerp(start, target, elapsed / shuffleDuration);
            yield return null;
        }

        rect.anchoredPosition = target; // 마지막 위치 정렬
    }

    // 배열 셔플 (Fisher-Yates 알고리즘)
    void ShuffleArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int r = Random.Range(i, array.Length);
            (array[i], array[r]) = (array[r], array[i]);
        }
    }

    // 배열 순서가 같은지 확인
    bool IsSameOrder(int[] a, int[] b)
    {
        return a.SequenceEqual(b);
    }

    // 다른 곳에서 시작 버튼 클릭할 때 호출할 수 있도록 공개 메서드 추가
    public void StartGameBtnClick()
    {
        Blocker.SetActive(true);
        StartGameBtn.SetActive(false);
        StartCoroutine(ShuffleMultipleTimes(shuffleCount));
    }
}
