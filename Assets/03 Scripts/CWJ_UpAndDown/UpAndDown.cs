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
    private bool _clear = false;

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
                if (!_clear)
                {
                    _clear = true;
                    ShowClearUI();
                    StartCoroutine(WaitinTime());
                }
                
            }
        }
    }

    IEnumerator WaitinTime()
    {
        if(_clear == false) yield break;
        
        yield return new WaitForSeconds(1f);
        
        UpAndDownManager.instance.StartCoroutine(UpAndDownManager.instance.ShowAnswer());
    }
    
    IEnumerator ScaleUp(RectTransform rect, float duration)
    {
        float time = 0f;
        Vector3 from = Vector3.zero;
        Vector3 to = Vector3.one;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, time / duration);
            rect.localScale = Vector3.Lerp(from, to, t);
            yield return null;
        }
        
        rect.localScale = to;
    }

    void ShowClearUI()
    {
        GameObject miniGameClear = Instantiate(miniGameClearUI,mainCanvas.transform);
        miniGameClear.transform.SetAsLastSibling();
        
        var rt = miniGameClear.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 0.5f);         // 하단 중앙
        rt.anchoredPosition = Vector2.zero;       // 캔버스 하단 중앙
        rt.localScale = Vector3.zero;             // 초기 스케일 0
        
        // 3) Scale 애니메이션
        StartCoroutine(ScaleUp(rt, 0.5f));        // 0.5초 동안
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
                SoundManager.instance.PlaySound2D("UpandDowncount");
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