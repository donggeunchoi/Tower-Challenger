using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public int failcount;

    public int curCount;
    //public int curLP = 4;
    public int maxRandomNum;


    private bool isOver = false;
    public string NumGenration()
    {

        int num = Random.Range(1, maxRandomNum + 1);

        UpAndDownManager.instance.randomNumber = num;

        return UpAndDownManager.instance.randomNumber.ToString("N0");
    }

    public void Success()
    {
        var num = UpAndDownManager.instance.upAndDownUI.numInput.text;

        if (num == UpAndDownManager.instance.randomNumber.ToString("N0"))
        {

            if (num == (int)UpAndDownManager.instance.randomNumber)
            {
                if (StageManager.instance != null)
                {
                    StageManager.instance.MiniGameResult(true);
                }
            }
        }
    }

    public void Failure()
    {
        int.TryParse(UpAndDownManager.instance.upAndDownUI.numInput.text, out int num);

        if (num != UpAndDownManager.instance.randomNumber)
        {
            if (num < (int)UpAndDownManager.instance.randomNumber)
            {

                if (num < (int)UpAndDownManager.instance.randomNumber)
                {
                    StartCoroutine(SetFalse(UpAndDownManager.instance.upAndDownUI.up));
                }
                else if (num > (int)UpAndDownManager.instance.randomNumber)
                {
                    StartCoroutine(SetFalse(UpAndDownManager.instance.upAndDownUI.down));
                }

            }
            else if (num > (int)UpAndDownManager.instance.randomNumber)
            {
                Debug.Log("Down");
            }
            failcount--;
            Debug.Log(failcount);
        }
        LPDown();
       
    }

    public void LPDown()
    {


        curCount--;
        if (curCount < 0)
        {
            curCount = Mathf.Max(curCount - 1, 0);
            if (StageManager.instance != null)
            {
                StageManager.instance.MiniGameResult(false);
            }
        }
    }
}
