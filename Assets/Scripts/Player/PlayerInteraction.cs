using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Button interactionButton; //상호작용 버튼
    public Transform interactionUIPannel; //상호작용 UI가 뜰 공간
    private InteractionNPC currentNPC;  //현재 NPC
    private GameObject currentUI;  //현재 띄워진 UI를 저장

    private void Start()
    {
        interactionButton.onClick.AddListener(InteractWithNPC);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var npc = other.GetComponentInParent<InteractionNPC>();  //NPC에게서 스크립트 가져오기
        if (npc != null)  //현재 NPC는 상호작용하고 있는 NPC
        {
            currentNPC = npc;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var npc = other.GetComponentInParent<InteractionNPC>(); //NPC에게서 스크립트 가져오기
        if (npc != null && npc == currentNPC)  //NPC초기화 및 창닫기
        {
            currentNPC = null;
            CloseUI();
        }
    }

    private void InteractWithNPC()
    {
        if (currentNPC == null)  //상호작용할 NPC가없으면 반환
            return;

        if (currentUI == null)  //만약 지금 띄워진 UI가없으면 NPC의 UI를 지정위치에 인스턴스
        {
            currentUI = Instantiate(currentNPC.uiPrefab, interactionUIPannel);
        }
        else  //아니라면 닫기
        {
            CloseUI();
        }
    }

    private void CloseUI() //아까 지정했던 NPC의 UI를 파괴 및 지정UI초기화
    {
        if (currentUI != null)
        {
            Destroy(currentUI);
            currentUI = null;
        }
    }
}