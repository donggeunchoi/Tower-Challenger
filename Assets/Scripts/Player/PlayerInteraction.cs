using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Button interactionButton;          //상호작용 버튼
    public Transform interactionUIPannel;     //상호작용 했을때 UI가 뜨는공간
    private IInteractable currentInteractable;//상호작용 연결 (어떤상호작용이 추가되도 대처할 수 있도록)
    private GameObject currentUI;             //상호작용에 의한 UI가 무엇인지 저장

    private void Start()
    {
        interactionButton.onClick.AddListener(Interact);
    }

    private void OnTriggerEnter2D(Collider2D other)  //트리거호출로 상호 작용이 가능하면 상호작용
    {
        currentInteractable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit2D(Collider2D other)  //NPC가 UI를 띄워줬으면 닫는 조치
    {
        if (other.GetComponent<IInteractable>() == currentInteractable)
        {
            currentInteractable = null;

            // NPC UI 닫기
            if (currentUI != null)
            {
                Destroy(currentUI);
                currentUI = null;
            }
        }
    }

    private void Interact() //상호작용
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    public void NotifyUIDestroyed(GameObject ui)  //만약에 UI가 자동으로 닫혔으면 null로 수정
    {
        if (currentUI == ui)
            currentUI = null;
    }
}