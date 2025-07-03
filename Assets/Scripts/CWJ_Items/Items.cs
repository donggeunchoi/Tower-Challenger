using UnityEngine;

public class Items : MonoBehaviour
{
    public static Items Instance;

    PlayerInput input;
    public bool topTicket = false;

    public bool use = false;

    private float speedUp = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        input = FindObjectOfType<PlayerInput>();
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
            use = true;
            return true;
        }
        return false;
    }
    public void Sandglass()
    {
        if (input != null)
        {
            StageManager.instance.stageTimer.timer += 10;
            use = true;
        }
        else
        {
            use = false;
            Debug.Log("인풋이 없어서 패스");
        }
    }

    public void MoveSpeedUp()
    {
        if (input != null)
        {
            input.speed += speedUp;
            use = true;
        }
        else
        {
            use = false;
            Debug.Log("플레이어 인풋이 없어서 패스합니다.");
        }
    }
}
