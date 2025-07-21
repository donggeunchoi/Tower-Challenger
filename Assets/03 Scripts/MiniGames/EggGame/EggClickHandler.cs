using UnityEngine;

public class EggClickHandler : MonoBehaviour
{
    public EggGameManager gameManager;

    private void OnMouseDown()
    {
      gameManager.EggClick();       
    }
}