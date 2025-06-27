using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageTimer : MonoBehaviour
{
    public const float MAX_TIME = 120f;
    public float timer = 120f;
    private bool isActive = false;
    public TextMeshProUGUI timerText;
    public Image uiBar;

    private void Start()
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StageManager.instance.GameOver();
            }
        }

        timerText.text = Mathf.Round(timer).ToString();
        uiBar.fillAmount = GetPercent();
    }

    public void SetTimer()
    {
        timer = MAX_TIME;
        isActive = true;
    }

    public void ResetTimer()
    {
        timer = MAX_TIME;
        isActive = false;
    }

    float GetPercent()
    {
        return timer / MAX_TIME;
    }
}
