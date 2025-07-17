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


    private bool isHealing = false;
    private int healHP = 2;
    private int Lv = 3;
    private int HP;
    private int MaxHP;
    private int HitDamage = 3;
    private int EndTime = 10;
    private bool GameStart = false;
    private float tiltTimer;
    private float originalWidth;
    private TextMeshProUGUI hpTextUI;
    private TextMeshProUGUI printOutUI;
   private RectTransform hpRect;

    void Start()
    {
        printOutUI = PrintOut.GetComponent<TextMeshProUGUI>();
        UpdateTime();
        InitHP(); //난이도별 HP 설정
        StartCoroutine(HealTime());
        foreach (GameObject egg in Eggs)
        {
            Button btn = egg.GetComponent<Button>();
            if (btn) btn.onClick.AddListener(EggClick);
        }
    }
    void InitHP()
    {
        hpTextUI = HPText.GetComponent<TextMeshProUGUI>();   
        hpRect = HPBar.GetComponent<RectTransform>();
        originalWidth = hpRect.sizeDelta.x;
        switch (Lv)
        {
            case 3: MaxHP = 300; break;
            case 2: MaxHP = 200; break;
            default: MaxHP = 100; break;
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

            if (HP == 0)
            {
                ClickBlocker.SetActive(true);
                GameStart = false;
                Debug.Log("클리어");
            }
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
        GameStart = false; // 힐 중에는 게임 시작 안 함

        while (HP < MaxHP)
        {
            HP += healHP;
            if (HP > MaxHP) HP = MaxHP;

            UpdateHPBar();
            UpdateHPText();

            yield return new WaitForSeconds(0.1f);
        }

        isHealing = false;
        ClickBlocker.SetActive(false);
        GameStart = true; // 힐 끝나고 게임 시작
    }
}