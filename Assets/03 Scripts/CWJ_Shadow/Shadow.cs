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

            if (!_clear)
            {
                _clear = true;
                ShowClearUI();
                SoundManager.instance.PlaySound2D("MiniGameClear");
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
            
            yield return new WaitForSeconds(1.5f);
            
            if (StageManager.instance != null)
            {
                StageManager.instance.MiniGameResult(true);
            }
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
    }
}
