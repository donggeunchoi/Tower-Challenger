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
        if (StageManager.instance != null)
            isActive = StageManager.instance.isGameActive;
    }

    private void Update()
    {
        if (isActive && StageManager.instance != null)
        {
            timer -= Time.deltaTime * StageManager.instance.timerMultiplier;
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
        this.gameObject.SetActive(true);
        timer = MAX_TIME;
        isActive = true;
    }

    public void ResetTimer()
    {
        this.gameObject.SetActive(false);
        timer = MAX_TIME;
        isActive = false;
    }

    float GetPercent()
    {
        return timer / MAX_TIME;
    }

    public void StopTimer()
    {
        isActive = false;
    }

    public void StartTimer()
    {
        isActive = true;
    }
}
