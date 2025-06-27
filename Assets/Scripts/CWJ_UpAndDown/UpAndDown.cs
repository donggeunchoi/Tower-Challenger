using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public int failcount;
    private bool isOver = false;
    public string NumGenration()
    {
        float num = Random.Range(0f, 101f);
        UpAndDownManager.instance.randomNumber = num;

        return UpAndDownManager.instance.randomNumber.ToString("N0");
    }

    public void Success()
    {
        var num = UpAndDownManager.instance.upAndDownUI.numInput.text;

        if (num == UpAndDownManager.instance.randomNumber.ToString("N0"))
        {
            Debug.Log("Success");
        }
    }

    public void Failure()
    {
        int.TryParse(UpAndDownManager.instance.upAndDownUI.numInput.text, out int num);

        if (num != UpAndDownManager.instance.randomNumber)
        {
            if (num < (int)UpAndDownManager.instance.randomNumber)
            {
                Debug.Log("UP");
            }
            else if (num > (int)UpAndDownManager.instance.randomNumber)
            {
                Debug.Log("Down");
            }
            failcount--;
            Debug.Log(failcount);
        }

        if (failcount == 0)
        {
            StageManager.instance.stageLP.LPdown();

            if (!isOver)
            {
                failcount = 7;
            }
        }
    }
}
