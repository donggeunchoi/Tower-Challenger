using UnityEngine;

public class Dino_Button_UI : MonoBehaviour
{
    public Dino_Player_Move player;

    public void DinoRunButton()
    {
        player.PlayerJump();
    }
}
