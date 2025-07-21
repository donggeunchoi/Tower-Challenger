using UnityEngine;

public class StaminaCharge : MonoBehaviour
{
    public GameObject timeText;

    void Update()
    {
        if (GameManager.Instance != null)
        {
            string Show = $"{(GameManager.Instance.stamina.staminatimer / 60f).ToString("N0")} / 30";
            timeText.GetComponent<TMPro.TMP_Text>().text = Show;
        }
    }
}
