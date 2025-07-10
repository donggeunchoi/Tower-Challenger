using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerInstance;

    public StageLP stageLP;
    public Inventory inventory;
    public ItemManager itemManager;
    public MailBox mailBox;
    public PlayerInput playerInput;
    public PlayerInteraction playerInteraction;
    public PlayerCamera playerCamera;
    
    private void Awake()
    {
        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        
    }
}
