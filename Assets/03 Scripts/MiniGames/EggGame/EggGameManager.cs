﻿using System.Collections;
using UnityEngine;
using TMPro;

public class EggGameManager : MonoBehaviour
{
    [Header("알")]
    public GameObject[] Eggs;

    [Header("부스러기")]
    public GameObject[] Particle;

    [Header("HP 관련")]
    public GameObject HPBar, HPText;

    [Header("출력 UI")]
    public GameObject PrintOut;
    public GameObject ClickBlocker;
    public GameObject EggHealText;

    [Header("난이도")]
    public int Lv = 2;
    public int HitDamage = 3;
    public int EndTime = 10;


    [Header("HP")]
    public int Egg0 = 100;
    public int Egg1 = 200;
    public int Egg2 = 300;

    private bool isHealing = false;
    private int HP = 1;
    private int MaxHP;
    private bool GameStart = false;
    private float tiltTimer;
    private float originalWidth;
    private int healHP = 1;

    private TextMeshProUGUI hpTextUI;
    private TextMeshProUGUI printOutUI;
    private RectTransform hpRect;
    private Animator animator;


    private string currentState = "";


    void Start()
    {
        for (int i = 0; i < Eggs.Length; i++)
        {
            Eggs[i].SetActive(false);
        }
        ClickBlocker.SetActive(false);
        Eggs[Lv].SetActive(true);
        EggHealText.SetActive(false);
        Set();
        InitHP();
        UpdateTime();
        StartCoroutine(HealTime());
    }

    void Set()
    {
        animator = Eggs[Lv].GetComponent<Animator>();
        printOutUI = PrintOut.GetComponent<TextMeshProUGUI>();
        hpTextUI = HPText.GetComponent<TextMeshProUGUI>();
        hpRect = HPBar.GetComponent<RectTransform>();
    }

    void InitHP()
    {
        originalWidth = hpRect.sizeDelta.x;

        switch (Lv)
        {
            case 2: MaxHP = Egg2; healHP = 5; break;
            case 1: MaxHP = Egg1; healHP = 4; break;
            default: MaxHP = Egg0;healHP = 3; break;
        }

        UpdateHPBar();
        UpdateHPText();
    }

    void Update()
    {
        if (GameStart)
        {
            tiltTimer += Time.deltaTime;
            UpdateTime();

            if (tiltTimer >= EndTime)
            {
                GameStart = false;
                StartCoroutine(HealTime());
            }
        }
    }
    public void EggClick()
    {
        if (!GameStart || HP <= 0) return;

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

    IEnumerator PlayClearAnimation()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Clear");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Clear");
        yield return new WaitForSeconds(2f);
    }

    void EggBreak()
    {
        float hpRatio = (float)HP / MaxHP;
        string newState;

        if (hpRatio <= 0.1f)
        {
            newState = "4";
        }
        else if (hpRatio <= 0.25f)
        {
            newState = "3";
        }
        else if (hpRatio <= 0.5f)
        {
            newState = "2";
        }
        else if (hpRatio <= 0.75f)
        {
            newState = "1";
        }
        else
        {
            newState = "Full";
        }

        if (newState != currentState)
        {
            animator.SetTrigger(newState);
            currentState = newState;
  
            if(!isHealing)         
            {
                StartCoroutine(PlayCrackParticleOnce());
            }
        }
    }
    IEnumerator PlayCrackParticleOnce()
    {
        GameObject particle = PoolManager.Instance.GetObject(
            Particle[Lv],
            Eggs[Lv].transform.position,
            Quaternion.Euler(0, -180f, 0)
        );
            particle.GetComponent<EggBreak>().EggBreakPC();
        
        yield return null;
    }



    void UpdateHPBar()
    {
        float ratio = (float)HP / MaxHP;
        hpRect.sizeDelta = new Vector2(originalWidth * ratio, hpRect.sizeDelta.y);
    }

    void UpdateHPText()
    {
        hpTextUI.text = HP + " / " + MaxHP;
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

        EggHealText.SetActive(false); // 초기 비활성화

        float healDelay = 0.1f;
        int totalHealAmount = MaxHP - HP;
        int healSteps = Mathf.CeilToInt((float)totalHealAmount / healHP);
        float totalHealTime = healSteps * healDelay;

        bool shouldShowHealText = totalHealTime >= 3f;

        for (int i = 0; HP < MaxHP; i++)
        {
            float remainingTime = totalHealTime - (i * healDelay);

            if (shouldShowHealText && remainingTime <= 3f && !EggHealText.activeSelf)
            {
                EggHealText.SetActive(true);
            }

            HP += healHP;
            if (HP > MaxHP) HP = MaxHP;

            UpdateHPBar();
            UpdateHPText();
            EggBreak();
            yield return new WaitForSeconds(healDelay);
        }
        isHealing = false;
        ClickBlocker.SetActive(false);
        GameStart = true;
        if (shouldShowHealText)
        {
            yield return new WaitForSeconds(2f);
            EggHealText.SetActive(false);
        }
    }
}