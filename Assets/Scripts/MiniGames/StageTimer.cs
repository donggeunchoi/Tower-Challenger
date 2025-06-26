using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageTimer : MonoBehaviour
{
    public const float MAX_TIME = 60f;
    public float timer = 60f;
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
