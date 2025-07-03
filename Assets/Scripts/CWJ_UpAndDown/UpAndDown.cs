using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpAndDown : MonoBehaviour
{
    public int failcount;
    public int curCount;
    public int curLP = 4;


    private bool isOver = false;

    public string NumGenration()
    {
        float num = Random.Range(0f, 31f);
        UpAndDownManager.instance.randomNumber = num;

        return UpAndDownManager.instance.randomNumber.ToString("N0");
    }

    public void Success()
    {
        int num;

        bool success = int.TryParse(UpAndDownManager.instance.upAndDownUI.textAnswer.text, out num);

        if (success)
        {
            if (num == (int)UpAndDownManager.instance.randomNumber)
            {
                Debug.Log("Success");
            }
        }

    }

    public void Failure()
    {
        int num;

        bool success = int.TryParse(UpAndDownManager.instance.upAndDownUI.textAnswer.text, out num);

        if (success)
        {
            if (num != UpAndDownManager.instance.randomNumber)
            {
                if (num < (int)UpAndDownManager.instance.randomNumber)
                {
                    StartCoroutine(SetFalse(UpAndDownManager.instance.upAndDownUI.up));
                }
                else if (num > (int)UpAndDownManager.instance.randomNumber)
                {
                    StartCoroutine(SetFalse(UpAndDownManager.instance.upAndDownUI.down));
                }
                curCount = Mathf.Max(curCount - 1, 0);
            }
            LPDown();
        }
    }

    public void LPDown()
    {
        if (curCount == 0)
        {
            curLP--;
            UpAndDownManager.instance.upAndDownUI.LP[curLP].sprite = UpAndDownManager.instance.upAndDownUI.emptyLP;
        }
    }

    public void InputAnswer(int index)
    {
        if (UpAndDownManager.instance.upAndDownUI.textAnswer.text.Length < 2)
        {
            UpAndDownManager.instance.upAndDownUI.textAnswer.text += index.ToString();
        }
    }

    private IEnumerator SetFalse(Image obj)
    {
        obj.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        obj.gameObject.SetActive(false);
    }
}