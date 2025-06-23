using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    MiniGameManager miniGameManager;
    public const int DEFALT_LP = 4;
    public int bonusLP = 0;
    public int currentLP;
    private int bestStage;
    private int currentStage;

    private void Awake()
    {
        if (miniGameManager == null)
        {
            miniGameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        currentLP = DEFALT_LP + bonusLP;
        currentStage = 0;
    }

    public void StageClear()
    {
        currentStage++;
        //랜덤스테이지
    }

    public void LPdown()
    {
        currentLP--;
        if (currentLP <= 0)
        {
            if (currentStage > bestStage)
                bestStage = currentStage;
            GameOver();
        }
    }

    private void GameOver()
    {
        //값을 넘겨주고
        Destroy(this);
    }
}
