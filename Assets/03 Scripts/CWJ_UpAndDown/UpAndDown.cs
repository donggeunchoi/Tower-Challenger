using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpAndDown : MonoBehaviour
{
    public int failCount;
    public int curCount;
    //public int curLP = 4;

    public int max_Num;
    
    [Header("미니게임 클리어 UI")]
    public GameObject miniGameClearUI;
    public Canvas mainCanvas;

    private void Start()
    {
        if (StageManager.instance!= null && GameManager.Instance != null)
        {
            int difficulty = StageManager.instance.difficulty;

            MiniGameData data = GameManager.Instance.miniGameDataList.Find(x => x.name == "Up&down" && x.DifficultyLevel == difficulty);

            if (data != null)
            {
                max_Num = data.max_Num;
                failCount = data.FailCount;
            }
        }
        
        curCount = failCount;

        UpAndDownManager.instance.upAndDownUI.count.text = curCount.ToString();
        NumGenration();
    }

    public string NumGenration()
    {
        int num = Random.Range(1, max_Num + 1);

        //Debug.Log(num);

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
                GameObject miniGameClear = Instantiate(miniGameClearUI,mainCanvas.transform);
                miniGameClear.transform.SetAsLastSibling();

                StartCoroutine(WaitinTime());
                
                UpAndDownManager.instance.StartCoroutine(UpAndDownManager.instance.ShowAnswer());
            }
        }
    }

    IEnumerator WaitinTime()
    {
        yield return new WaitForSeconds(1f);
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
                
            }
            LPDown();
        }
    }

    public void LPDown()
    {
        curCount--;
        if (curCount < 0)
        {
            curCount = Mathf.Max(curCount - 1, 0);

            if (StageManager.instance != null)
                StageManager.instance.MiniGameResult(false);
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