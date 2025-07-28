using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class SellGameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject fadeIn;
    public GameObject Blocker;
    public GameObject StartGameBtn;
    public GameObject[] Card;

    
    [Header("Settings")]
    public float shuffleDuration = 0.15f;      //섞이는속도
    public float waitBetweenShuffles = 0.05f;  //딜레이
    public int shuffleCount = 20;              //셔플 횟수
    public int card;

    private RectTransform[] rects;
    private Vector3[] originalPositions;
    private StageManager stageManager;
    
    [Header("미니게임 클리어 UI")]
    public GameObject miniGameClearUI;
    public Canvas mainCanvas;


    void Start()
    {
        stageManager = StageManager.instance;

        if (stageManager != null && GameManager.Instance != null )
        {
            int difficulty = stageManager.difficulty;

            MiniGameData data = GameManager.Instance.miniGameDataList.Find(x => x.name == "야바위" && x.DifficultyLevel == difficulty);

            if (data != null)
            {
                shuffleCount = data.shuffleCount;
                shuffleDuration = data.shuffleDuration;
                card = data.Card;
            }
        }

        if (card >= Card.Length)
        {
            card = Card.Length;
        }
        else if (card < 1)
        {
            card = 1;
        }
            
        InitializeCards();
        StartCoroutine(GameIntroSequence());
    }

    void InitializeCards()
    {
        int len = Card.Length;
        rects = new RectTransform[card];
        originalPositions = new Vector3[card];

        for (int i = 0; i < len; i++)
        {
            GameObject obj = Card[i];
            if (i < card)
            {
                obj.SetActive(true); // 필요한 카드만 활성화
                rects[i] = obj.GetComponent<RectTransform>();
                originalPositions[i] = rects[i].anchoredPosition;

                int index = i; // 캡처
                Button btn = obj.GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => OnButtonClick(index));
            }
            else
            {
                obj.SetActive(false); // 나머지 카드는 비활성화
            }
        }
    }

    IEnumerator GameIntroSequence()
    {
        Blocker.SetActive(true);
        StartGameBtn.SetActive(false);
        fadeIn.SetActive(true);

        yield return new WaitForSeconds(1f);
        fadeIn.SetActive(false);

        yield return new WaitForSeconds(1f);
        Card[0].GetComponent<Animator>().SetTrigger("ThisCard");

        yield return new WaitForSeconds(1f);
        Card[0].GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(1.5f);
        StartGameBtn.SetActive(true);
        Blocker.SetActive(false);
    }

    public void OnStartGameButtonClicked()
    {
        Blocker.SetActive(true);
        StartGameBtn.SetActive(false);
        StartCoroutine(ShuffleMultipleTimes(shuffleCount));
    }

    void OnButtonClick(int index)
    {
        StartCoroutine(OnButtonClickRoutine(index));
    }

    IEnumerator OnButtonClickRoutine(int index)
    {
        Blocker.SetActive(true);
        Card[index].GetComponent<Animator>().SetTrigger("ThisCard");
        yield return new WaitForSeconds(0.3f);
        Card[index].GetComponent<Animator>().SetTrigger("Effect");
        yield return new WaitForSeconds(2);
        if (index == 0)
        {
            GameObject miniGameClear = Instantiate(miniGameClearUI,mainCanvas.transform);
            miniGameClear.transform.SetAsLastSibling();
            
            if (StageManager.instance != null)
                StageManager.instance.MiniGameResult(true);
            Blocker.SetActive(false);
        }
        else
        {
            if (StageManager.instance != null)
                StageManager.instance.MiniGameResult(false);
            yield return StartCoroutine(OnWrongAnswer());
        }
    }

    IEnumerator OnWrongAnswer()
    {
        Blocker.SetActive(true);
        Card[0].GetComponent<Animator>().SetTrigger("ThisCard");

        yield return new WaitForSeconds(1f);
        Card[0].GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(ShuffleMultipleTimes(shuffleCount));

        Blocker.SetActive(false);
    }

    IEnumerator ShuffleMultipleTimes(int times)
    {
        for (int i = 0; i < times; i++)
        {
            yield return StartCoroutine(ShuffleOnce());
            yield return new WaitForSeconds(waitBetweenShuffles);
        }

        Blocker.SetActive(false);
    }

    IEnumerator ShuffleOnce()
    {
        int len = card;
        int[] order = Enumerable.Range(0, len).ToArray();
        int[] original = (int[])order.Clone();

        do
        {
            ShuffleArray(order);
        } while (IsSameOrder(order, original));

        for (int i = 0; i < len; i++)
        {
            StartCoroutine(MoveSmoothly(rects[i], originalPositions[order[i]]));
        }

        yield return new WaitForSeconds(shuffleDuration);
    }

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

        rect.anchoredPosition = target;
    }

    void ShuffleArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int r = Random.Range(i, array.Length);
            (array[i], array[r]) = (array[r], array[i]);
        }
    }

    bool IsSameOrder(int[] a, int[] b)
    {
        return a.SequenceEqual(b);
    }
    public void StartGameBtnClick()
    {
        Blocker.SetActive(true);
        StartGameBtn.SetActive(false);
        StartCoroutine(ShuffleMultipleTimes(shuffleCount));
    }
}