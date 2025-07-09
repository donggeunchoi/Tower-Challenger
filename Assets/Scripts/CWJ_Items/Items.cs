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
        input = FindAnyObjectByType<PlayerInput>();
    }

    public bool TopTicketUse()
    {
        if (topTicket)
        {
            return true;
        }
        else
        {
            Debug.Log("타워에서 사용해야 합니다.");
            return false;
        }
    }

    public bool SandGlass()
    {
        if (StageManager.instance != null && StageManager.instance.stageTimer != null)
        {
            StageManager.instance.stageTimer.timer += 10;
            return true;
        }
        else
        {
            Debug.Log("인게임에서 사용해야해요");
            return false;
        }
    }
    
    public bool MoveSpeedUp()
    {
        if (input != null)
        {
            input.speed += speedUp;
            return true;
        }
        else
        {
            Debug.Log("PlayerInput이 없습니다.");
            return false;
        }
    }

   
    // public void Sandglass()
    // {
    //     if (input != null)
    //     {
    //         StageManager.instance.stageTimer.timer += 10;
    //         use = true;
    //     }
    //     else
    //     {
    //         use = false;
    //         Debug.Log("인풋이 없어서 패스");
    //     }
    // }
    //
    // public void MoveSpeedUp()
    // {
    //     if (input != null)
    //     {
    //         input.speed += speedUp;
    //         use = true;
    //     }
    //     else
    //     {
    //         use = false;
    //         Debug.Log("플레이어 인풋이 없어서 패스합니다.");
    //     }
    // }
}
