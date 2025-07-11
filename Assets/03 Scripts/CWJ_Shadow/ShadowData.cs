using UnityEngine;

[CreateAssetMenu(fileName = "ShadowData", menuName = "UI/Data")]
public class ShadowData : ScriptableObject
{
    public Sprite shadowSprite; // 그림자 이미지
    public Sprite[] choices; // 선택지 이미지
    public float shadowTime; // 그림자 이미지가 보여지는 시간
    public int successIndex;
}