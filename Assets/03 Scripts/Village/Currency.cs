using TMPro;
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
        if (gold != Save.playerData.gold || diamond != Save.playerData.diamond)
            UpdateCurrency();
    }

    private void UpdateCurrency()
    {
        gold = Save.playerData.gold;
        diamond = Save.playerData.diamond;

        if (goldText != null && diamondText != null)
        {
            goldText.text = gold.ToString();
            diamondText.text = diamond.ToString();
        }
    }
}
