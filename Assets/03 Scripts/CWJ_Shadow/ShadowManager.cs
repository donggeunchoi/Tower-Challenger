using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShadowData", menuName = "UI/Data")]
public class ShadowData : ScriptableObject
{
    public Sprite shadowSprite; // 그림자 이미지
    public Sprite[] choices; // 선택지 이미지
    public float shadowTime; // 그림자 이미지가 보여지는 시간
    public int successIndex;
}
public class ShadowManager : MonoBehaviour
{
    public static ShadowManager instance;

    public Shadow shadow;
    public ShadowUI shadowUI;
    public ShadowData[] shadowData;

    public float time;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < shadowData.Length; i++)
        {
            shadowData[i].shadowTime = time;
        }
        
        shadowUI.shadowGameInit();
    }

}
