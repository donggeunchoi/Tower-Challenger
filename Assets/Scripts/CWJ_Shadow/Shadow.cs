using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shadow : MonoBehaviour
{
    public void Answer(int selectedIndex)
    {
        var p = ShadowManager.instance.shadowData[ShadowManager.instance.shadowUI.randomIndex];// 랜덤으로 문제를 생성

        if (selectedIndex == p.successIndex)
        {
            Debug.Log("클리어");
            return;
        }
        else
        {
            Debug.Log("잘못 선택 했음");
        }
    }
}
