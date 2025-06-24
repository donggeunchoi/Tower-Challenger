using UnityEngine;

public class InteractionPortal : MonoBehaviour
{
    public enum PortalType { StartGame, NextGame, NextFloor }  //우선 포탈 타입을 나눠놓기
    public PortalType portalType;                              //포탈 타입 인스펙터에서 고를 수 있게

    public void Interact(GameObject player)  //플레이어 상호작용시 포탈 타입에 맞춰 각각의 코드를 진행
    {
        switch (portalType)
        {
            case PortalType.StartGame:
                StageManager.Instance.StartGame();  //게임이 시작됩니다
                break;
            case PortalType.NextGame:
                StageManager.Instance.SavePlayerPosition(player.transform.position);  //플레이어의 위치를 저장하고
                StageManager.Instance.StartNextMiniGame();  //미니게임 씬으로 이동합니다
                break;
            case PortalType.NextFloor:
                StageManager.Instance.NextFloor();  //다음 층으로 이동합니다
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)  //상호작용 영역에 플레이어가 들어오면 상호작용 가능
    {
        if (other.CompareTag("Player"))
        {
            Interact(other.gameObject);
        }
    }
}