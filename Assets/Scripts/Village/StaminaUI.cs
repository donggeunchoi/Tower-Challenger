using UnityEngine;

public class StaminaUI : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject[] staminaUI;
    private int stamina;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (stamina != gameManager.mainStamina)
        {
            UpdateStaminaUI();
        }
    }

    private void UpdateStaminaUI()
    {
        for (int i = 0; i <= staminaUI.Length; i++)
        {
            staminaUI[i].gameObject.SetActive(false);
        }

        for (int i = 0; i <= gameManager.mainStamina; i++)
        {
            staminaUI[i].gameObject.SetActive(true);
        }

        stamina = gameManager.mainStamina;
    }
}
