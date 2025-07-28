using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shadow : MonoBehaviour
{
    [Header("미니게임 클리어 UI")]
    public GameObject miniGameClearUI;
    public Canvas mainCanvas;
    private bool _clear = false;
    
    
    public void Answer(int selectedIndex)
    {
        var p = ShadowManager.instance.shadowData[ShadowManager.instance.randomIndex];// 랜덤으로 생성된 문제

        Debug.Log(p);
        Debug.Log(selectedIndex);

        if (selectedIndex == p.successIndex)
        {
            
            GameObject miniGameClear = Instantiate(miniGameClearUI,mainCanvas.transform);
            miniGameClear.transform.SetAsLastSibling();

            if (!_clear)
            {
                _clear = true;
                StartCoroutine(WaitinTime());
            }
            
            Debug.Log("클리어");
            return;
        }
        else
        {
            if (StageManager.instance != null)
            {
                StageManager.instance.MiniGameResult(false);
            }
            ShadowManager.instance.shadowUI.shadowImage.gameObject.SetActive(true);
            ShadowManager.instance.shadowUI.shadowGameInit();

            Debug.Log("잘못 선택 했음");
        }
        
        IEnumerator WaitinTime()
        {
            if(_clear == false) yield break;
            
            yield return new WaitForSeconds(1f);
            
            if (StageManager.instance != null)
            {
                StageManager.instance.MiniGameResult(true);
            }
        }
    }
}
