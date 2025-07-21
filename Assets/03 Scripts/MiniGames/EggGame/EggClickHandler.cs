using UnityEngine;

public class EggClickHandler : MonoBehaviour
{
    public EggGameManager gameManager;

    private void OnMouseDown()
    {
        if (gameManager != null)
        {
            gameManager.EggClick();
        }
        else
        {
            Debug.LogWarning("EggClickHandler: gameManager가 연결되지 않았습니다.");
        }
    }
}