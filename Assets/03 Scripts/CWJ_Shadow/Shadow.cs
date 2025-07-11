using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shadow : MonoBehaviour
{
    public void Answer(int selectedIndex)
    {
        var p = ShadowManager.instance.shadowData[ShadowManager.instance.randomIndex];// 랜덤으로 생성된 문제

        Debug.Log(p);
        Debug.Log(selectedIndex);

        if (selectedIndex == p.successIndex)
        {
            if (StageManager.instance != null)
            {
                StageManager.instance.MiniGameResult(true);
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
    }
}
