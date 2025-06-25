using UnityEngine;
using UnityEngine.UI;

public interface IInteractable  //상호작용이 가능하면 상호 작용을 할수있도록 조치
{
    void Interact();
}

public class InteractionUI : MonoBehaviour
{
    public PlayerInteraction owner;
    public Button closeButton;

    private void Start()
    {
        if (closeButton != null)  //NPC의 UI버튼은 여기서 할당!
        {
            closeButton.onClick.AddListener(Close);
        }
    }

    private void Close()  //UI창을 여러개 띄워놓는 것을 방지!
    {
        if (owner != null)
        {
            owner.UIDestroyed(gameObject);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()  //만약에 자동으로 부숴지면 호출!
    {
        if (owner != null)
        {
            owner.UIDestroyed(gameObject);
        }
    }
}
