using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EggGameManager : MonoBehaviour
{
    [Header("알")]
    public GameObject[] Eggs;

    [Header("HP 관련")]
    public GameObject HPBar,HPText; // HP 바 (Image 오브젝트)

    [Header("출력 UI")]
    public GameObject PrintOut;
    public GameObject ClickBlocker;

    [Header("난이도")]
    public int Lv = 2;
    public int HitDamage = 3;
    public int EndTime = 10;
    public int healHP = 2;

    [Header("HP")]
    public int Egg0 = 100;
    public int Egg1 = 200;
    public int Egg2 = 300;

    private bool isHealing = false;
    private int HP =1;
    private int MaxHP;
    private bool GameStart = false;
    private float tiltTimer;
    private float originalWidth;
    private TextMeshProUGUI hpTextUI;
    private TextMeshProUGUI printOutUI;
    private RectTransform hpRect;
    private Animator animator;

    void Set()
    {
        animator = Eggs[Lv].GetComponent<Animator>();
        printOutUI = PrintOut.GetComponent<TextMeshProUGUI>();
        hpTextUI = HPText.GetComponent<TextMeshProUGUI>();
        hpRect = HPBar.GetComponent<RectTransform>();
    }
    void Start()
    {
        foreach (GameObject egg in Eggs)
        {
            Button btn = egg.GetComponent<Button>();
            if (btn) btn.onClick.AddListener(EggClick);
            egg.SetActive(false);
        }
        Eggs[Lv].SetActive(true);
        Set();
        InitHP();
        UpdateTime();
        StartCoroutine(HealTime());
    }
    void InitHP()
    {
        originalWidth = hpRect.sizeDelta.x;
        switch (Lv)
        {
            case 2: MaxHP = Egg2; break;
            case 1: MaxHP = Egg1; break;
            default: MaxHP = Egg0; break;
        }
    }
    void Update()
    {
        if (GameStart)
        {
            tiltTimer += Time.deltaTime;
            UpdateTime();

            if (tiltTimer >= 10f)
            {
                GameStart = false;
                StartCoroutine(HealTime());
            }
        }
    }

    public void EggClick()
    {
        if (GameStart)
        {
            HP -= HitDamage;
            if (HP < 0) HP = 0;

            UpdateHPBar();
            UpdateHPText();
            EggBreak();

            if (HP == 0)
            {
                ClickBlocker.SetActive(true);
                GameStart = false;
                StartCoroutine(PlayClearAnimation());
            }
        }
    }
    IEnumerator PlayClearAnimation()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Clear");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Clear");
        yield return new WaitForSeconds(2f);
    }
    public void EggBreak()
    { 
        float hpRatio = (float)HP / MaxHP;

        if (hpRatio <= 0.1f)
        {
            animator.SetTrigger("4");
        }
        else if (hpRatio <= 0.25f)
        {
            animator.SetTrigger("3");
        }
        else if (hpRatio <= 0.5f)
        {
            animator.SetTrigger("2");
        }
        else if (hpRatio <= 0.75f)
        {
            animator.SetTrigger("1");
        }
        else if (hpRatio <= 1f)
        {
            animator.SetTrigger("Full");
        }
    }

    void UpdateHPBar()
    {
        float ratio = (float)HP / MaxHP;
        hpRect.sizeDelta = new Vector2(originalWidth * ratio, hpRect.sizeDelta.y);
    }
    void UpdateHPText()
    {
        hpTextUI.text = HP + "/" + MaxHP;
    }
    void UpdateTime()
    {
        string timerText = tiltTimer.ToString("F1");
        printOutUI.text = timerText + " / " + EndTime;
    }
    IEnumerator HealTime()
    {
        
        ClickBlocker.SetActive(true);
        tiltTimer = 0;
        if (isHealing) yield break;
        isHealing = true;
        GameStart = false; 

        while (HP < MaxHP)
        {
            HP += healHP;
            if (HP > MaxHP) HP = MaxHP;

            UpdateHPBar();
            UpdateHPText();
            EggBreak();
            yield return new WaitForSeconds(0.1f);
        }

        isHealing = false;
        ClickBlocker.SetActive(false);
        GameStart = true; 
    }
}