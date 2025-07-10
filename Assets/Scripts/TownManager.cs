using UnityEngine;

public class TownManager : MonoBehaviour
{
    public static TownManager townInstance;

    public NPController npcController;
    public VillageManager villageManager;
    public Guild guild;
    public CharactorChoice choice;
    public Store store;

    private void Awake()
    {
        if (townInstance == null)
        {
            townInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
