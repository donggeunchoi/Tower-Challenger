using UnityEngine;

public class InteractionPortal : MonoBehaviour, IInteractable
{
    public enum PortalType { StartGame, NextGame, NextFloor }  //우선 포탈 타입을 나눠놓기
    public PortalType portalType;    //포탈 타입 인스펙터에서 고를 수 있게
    private Vector3 playerPosition;

    public void Interact()  //플레이어 상호작용시 포탈 타입에 맞춰 각각의 코드를 진행
    {
        switch (portalType)
        {
            case PortalType.StartGame:
                StageManager.instance.StartGame();  //게임이 시작됩니다
                break;
            case PortalType.NextGame:
                StageManager.instance.SavePlayerPosition(playerPosition);  //플레이어의 위치를 저장하고
                StageManager.instance.StartNextMiniGame();  //미니게임 씬으로 이동합니다
                break;
            case PortalType.NextFloor:
                StageManager.instance.NextFloor();  //다음 층으로 이동합니다
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)  //상호작용 영역에 플레이어가 들어오면 상호작용 가능
    {
        if (other.CompareTag("Player"))
        {
            playerPosition = other.transform.position;
        }
    }
}