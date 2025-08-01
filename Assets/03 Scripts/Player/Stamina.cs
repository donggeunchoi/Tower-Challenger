using System;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public const int MAX_STAMINA = 5;
    public int mainStamina { get; private set; }
    public float staminatimer { get; private set; }
    public const float STAMINA_TIME = 1800f;

    private void Update()
    {
        UpdateStaminaTimer();
    }

    public void LoadStamina()
    {
        mainStamina = Save.playerData.stamina;

        if (!string.IsNullOrEmpty(Save.playerData.lastTimeString))
        {
            Save.playerData.lastTime = DateTime.Parse(Save.playerData.lastTimeString, null,
                System.Globalization.DateTimeStyles.RoundtripKind);
        }
        else
        {
            Save.playerData.lastTime = DateTime.Now;
        }

        float secondsPassed = (float)(DateTime.Now - Save.playerData.lastTime).TotalSeconds;
        staminatimer = Save.playerData.staminaTimer + secondsPassed;
    }

    private void UpdateStaminaTimer()
    {
        if (mainStamina >= MAX_STAMINA)
        {
            staminatimer = 0;
            return;
        }
        staminatimer += Time.deltaTime;

        if (staminatimer >= STAMINA_TIME)
        {
            Debug.Log("시간이 돌았습니다");
            AddStamina();
            staminatimer -= STAMINA_TIME;
        }
    }

    public void AddStamina()
    {
        mainStamina = Mathf.Min(mainStamina + 1, MAX_STAMINA);
        Save.SaveData();
    }

    public void UseStamina()
    {
        if (mainStamina < 0)
            return;

        mainStamina = Mathf.Max(mainStamina - 1, 0);
        Save.SaveData();
    }
}
