using UnityEngine;

public class StageLP : MonoBehaviour
{
    public const int DEFALT_LP = 4;
    public int bonusLP;
    public int currentLP;

    public void ResetLP() => currentLP = DEFALT_LP + bonusLP;

    public void LPdown()
    {
        currentLP = Mathf.Max(currentLP - 1, 0);
    }
    public void RecoverLP(int amount)
    {
        currentLP = Mathf.Min(currentLP + amount, DEFALT_LP + bonusLP);
    }
}