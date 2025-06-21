using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class InteractionUI : MonoBehaviour
{
    public PlayerInteraction owner; // 이 UI를 띄운 PlayerInteraction 객체

    private void OnDestroy()
    {
        if (owner != null)
        {
            owner.NotifyUIDestroyed(this.gameObject);
        }
    }
}
