using UnityEngine;
using UnityEngine.UI;

public class InteractionNPC : MonoBehaviour, IInteractable
{
    public GameObject uiPrefab;
    public Transform uiParent;
    private GameObject currentUI;

    public void Interact()
    {
        if (currentUI == null)
        {
            currentUI = Instantiate(uiPrefab, uiParent.position, Quaternion.identity, uiParent);

            // UI 닫기 버튼 설정
            var closeButton = currentUI.GetComponentInChildren<Button>();
            if (closeButton != null)
            {
                closeButton.onClick.AddListener(CloseUI);
            }
        }
        else
        {
            CloseUI();
        }
    }

    private void CloseUI()
    {
        if (currentUI != null)
        {
            Destroy(currentUI);
            currentUI = null;
        }
    }
}