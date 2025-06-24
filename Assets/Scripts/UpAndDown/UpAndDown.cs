using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    private int failcount = 0;

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
            failcount++;
            Debug.Log(failcount);
        }

        if (failcount == 7)
        {
            Debug.Log("PL 1 감소");
        }
    }
}
