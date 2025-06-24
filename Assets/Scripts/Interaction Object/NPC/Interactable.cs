using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
    void Interact();
}

public class InteractionUI : MonoBehaviour
{
    public PlayerInteraction owner;
    public Button closeButton;

    private void Start()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(Close);
        }
    }

    private void Close()
    {
        if (owner != null)
        {
            owner.NotifyUIDestroyed(gameObject);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (owner != null)
        {
            owner.NotifyUIDestroyed(gameObject);
        }
    }
}
