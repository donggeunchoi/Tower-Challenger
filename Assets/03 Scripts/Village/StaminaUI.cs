using UnityEngine;

public class StaminaUI : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject[] staminaUI;
    private int stamina;

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager != null && staminaUI != null)
        {
            UpdateStaminaUI();
        }
    }

    private void Update()
    {
        if (stamina != gameManager.stamina.mainStamina)
        {
            UpdateStaminaUI();
        }
    }

    private void UpdateStaminaUI()
    {
        if (staminaUI.Length >= gameManager.stamina.mainStamina)
        {
            for (int i = 0; i < staminaUI.Length; i++)
            {
                staminaUI[i].gameObject.SetActive(false);
            }

            for (int j = 0; j < Mathf.Min(gameManager.stamina.mainStamina, staminaUI.Length); j++)
            {
                staminaUI[j].gameObject.SetActive(true);
            }

            stamina = gameManager.stamina.mainStamina;
        }
    }
}
