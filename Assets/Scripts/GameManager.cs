using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

    public void StageClear()
    {

    }

    public void FailStage()
    {
        
    }
}
