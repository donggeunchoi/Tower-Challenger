using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;

public class SellGameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject fadeIn;
    public GameObject Blocker;
    public GameObject StartGameBtn;
    public Button[] buttons;
    public Animator animator;

    [Header("Settings")]
    public float shuffleDuration = 0.15f;
    public float waitBetweenShuffles = 0.05f;
    public int shuffleCount = 20;

    private RectTransform[] rects;
    private Vector3[] originalPositions;
    StageManager stageManager;

    

    void Start()
    {
        stageManager = StageManager.instance;
        InitializeButtons();
        StartCoroutine(GameIntroSequence());
    }

    void InitializeButtons()
    {
        int len = buttons.Length;
        rects = new RectTransform[len];
        originalPositions = new Vector3[len];

        for (int i = 0; i < len; i++)
        {
            rects[i] = buttons[i].GetComponent<RectTransform>();
            originalPositions[i] = rects[i].anchoredPosition;

            int index = i; // 캡처
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
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
        animator.SetTrigger("ThisCard");

        yield return new WaitForSeconds(2f);
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
        if (index == 0)
        {
            Debug.Log("클리어!");
        }
        else
        {
            //stageManager.LPdown();
            StartCoroutine(OnWrongAnswer());
        }
    }

    IEnumerator OnWrongAnswer()
    {
        Blocker.SetActive(true);
        animator.SetTrigger("ThisCard");

        yield return new WaitForSeconds(2f);
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
        int len = buttons.Length;
        int[] order = Enumerable.Range(0, len).ToArray();
        int[] original = (int[])order.Clone();

        do
        {
            ShuffleArray(order);
        } while (IsSameOrder(order, original));

        for (int i = 0; i < len; i++)
            StartCoroutine(MoveSmoothly(rects[i], originalPositions[order[i]]));

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