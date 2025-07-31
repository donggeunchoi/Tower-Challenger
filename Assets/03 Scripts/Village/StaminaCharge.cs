using UnityEngine;
using UnityEngine.UI;
public class StaminaCharge : MonoBehaviour
{
    public GameObject timeText;
    public GameObject StaminaRecharge;

    void Update()
    {
        if (GameManager.Instance != null)
        {
            float current = GameManager.Instance.stamina.staminatimer / 60f;
            string Show = $"{current.ToString("N0")} / 30";
            timeText.GetComponent<TMPro.TMP_Text>().text = Show;

            float fill = Mathf.Clamp01(current / 30f);
            StaminaRecharge.GetComponent<Image>().fillAmount = fill;
        }
    }
}
