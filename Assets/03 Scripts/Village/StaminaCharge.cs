using System;
using UnityEngine;

public class StaminaCharge : MonoBehaviour
{
    public GameObject timeText;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        string Show = $"{(gameManager.staminatimer * 1/60f).ToString("N0")} / 30";
        timeText.GetComponent<TMPro.TMP_Text>().text = Show;
    }
}
