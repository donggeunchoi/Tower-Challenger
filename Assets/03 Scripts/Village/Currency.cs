﻿using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    private int gold;
    private int diamond;

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI diamondText;

    private void Start()
    {
        UpdateCurrency();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null)
        {
            if (gold != GameManager.Instance.account.gold || diamond != GameManager.Instance.account.diamond)
                UpdateCurrency();
        }
    }

    private void UpdateCurrency()
    {
        if (GameManager.Instance != null)
        {
            gold = GameManager.Instance.account.gold;
            diamond = GameManager.Instance.account.diamond;
        }

        if (goldText != null && diamondText != null)
        {
            goldText.text = gold.ToString();
            diamondText.text = diamond.ToString();
        }
    }
}
