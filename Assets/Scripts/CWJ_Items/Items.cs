using UnityEngine;

public class Items : MonoBehaviour
{
    public static Items Instance;

    PlayerInput input;
    public bool topTicket = false;

    private float speedUp = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        input = GetComponent<PlayerInput>();
    }

    public void TopTicketUse()
    {
        TopTicket();
    }

    public bool TopTicket()
    {
        if (!topTicket)
        {
            topTicket = true;
            return true;
        }
        return false;
    }
    public void Sandglass()
    {
        StageManager.instance.stageTimer.timer += 10;
    }

    public void MoveSpeedUp()
    {
        input.speed += speedUp;
    }
}
