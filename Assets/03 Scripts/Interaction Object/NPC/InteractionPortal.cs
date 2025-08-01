using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PortalType { StartGame, NextGame, NextFloor, Tutorial }  //우선 포탈 타입을 나눠놓기

public class InteractionPortal : MonoBehaviour, IInteractable
{
    
    public PortalType portalType;    //포탈 타입 인스펙터에서 고를 수 있게
    private Vector3 playerPosition;
    public int portalNumber;
    private int layerNumber;
    public bool isPortar;
    public void Interact()  //플레이어 상호작용시 포탈 타입에 맞춰 각각의 코드를 진행
    {
        switch (portalType)
        {
            case PortalType.NextGame:
                Debug.Log(playerPosition);
                StageManager.instance.SaveClearPortal(portalNumber);
                PlayerManager.Instance.SavePlayerPosition(playerPosition, layerNumber);  //플레이어의 위치를 저장하고
                StageManager.instance.StartNextMiniGame();  //미니게임 씬으로 이동합니다
                break;
            case PortalType.NextFloor:
                UIManager.Instance.InstantiateUI(UIManager.Instance.allClearUI);  //다음 층으로 이동하는 UI를 띄워줍니다
                break;
            case PortalType.Tutorial:
                SceneManager.LoadScene("GameScene");
                GameManager.Instance.playerData.tutorialCompleted = true;
                GameManager.Instance.playerData.SaveData();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)  //상호작용 영역에 플레이어가 들어오면 상호작용 가능
    {
        if (other.CompareTag("Player"))
        {
            if (portalType == PortalType.StartGame)
                StartCoroutine(StartGameRoutine());

            layerNumber = other.gameObject.layer;
            playerPosition = other.transform.position;
        }
    }
    
    private IEnumerator StartGameRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        StageManager.instance.StartGame();
    }
}
