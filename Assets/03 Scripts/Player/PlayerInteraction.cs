using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Button interactionButton;          //상호작용 버튼
    public Image interactImage;
    public Transform interactionUIPannel;     //상호작용 했을때 UI가 뜨는공간
    private IInteractable currentInteractable;//상호작용 연결 (어떤상호작용이 추가되도 대처할 수 있도록)
    private GameObject currentUI;    //상호작용에 의한 UI가 무엇인지 저장
    public bool clicked { get; private set; }
    private void Start()
    {
        interactionButton.onClick.AddListener(Interact);
        interactionButton.TryGetComponent<Image>(out interactImage);
    }

    private void Update()
    {
        float alpha;
        if (currentInteractable != null)
            alpha = 1.0f;
        else
            alpha = 70f / 255f;

        if (interactImage != null)
        {
            Color buttonColor = interactImage.color;
            if (!Mathf.Approximately(buttonColor.a, alpha))
            {
                buttonColor.a = alpha;
                interactImage.color = buttonColor;
            }
        }
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

    public void Interact() //상호작용
    {
        if (currentInteractable != null)
        {
            clicked = true;
            currentInteractable.Interact();
        }
    }

    public void UIDestroyed(GameObject ui)  //만약에 UI가 자동으로 닫혔으면 null로 수정
    {
        if (currentUI == ui)
            currentUI = null;
    }
}