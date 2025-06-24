using UnityEngine;

public class StageLP : MonoBehaviour
{
    StageManager stageManager;

    public const int DEFALT_LP = 4;
    public int bonusLP;
    public int currentLP;
    
    void Start()
    {
        stageManager = StageManager.instance;
    }

    public void ResetLP()
    {
        currentLP = DEFALT_LP + bonusLP;
    }

    public bool IsLPdown()
    {
        currentLP = Mathf.Max(currentLP - 1, 0);

        if (currentLP <= 0)
        {
            return false;
        }
        return true;
    }
}
