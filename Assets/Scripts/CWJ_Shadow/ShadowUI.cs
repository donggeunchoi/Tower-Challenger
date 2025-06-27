using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShadowUI : MonoBehaviour
{
    public Sprite[] shadowSprite; // 문제
    public Sprite[] imageSprite; // 정답 버튼들

    public Image animalImage;

    public Image[] buttonImages;

    public int shadowIndex;
    public int imageIndex;

    public void Init()
    {
        //RandomGeneration();
        //ButtonInit();

        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[imageIndex].sprite = imageSprite[imageIndex];

            Debug.Log($"button name : {buttonImages[i].name}");
            Debug.Log($"button sprite name : {buttonImages[i].sprite.name}");
        }
    }

    public void RandomGeneration()
    {
        // 이미지를 랜덤으로 생성
        shadowIndex = Random.Range(0, shadowSprite.Length);
        animalImage.sprite = shadowSprite[shadowIndex];
    }
    private void ButtonInit()
    {
        imageIndex = Random.Range(0, buttonImages.Length);// 랜덤으로 0 ~ 2 값

        for (int i = 0; i < buttonImages.Length; i++) // 2까지
        {
            // 중복된 이미지가 들어가면 안됌
            //if (buttonImages[i].sprite.name != )

            buttonImages[imageIndex].sprite = imageSprite[imageIndex]; // 버튼[0~2]중에 이미지[0~2]
        }
    }
}
