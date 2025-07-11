using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Guild : MonoBehaviour
{
    [Header("길드")]
    public Transform pannelTransform;
    public GameObject pannel;
    public GameObject currentPannel;

    [Header("캐릭터 데이터")]
    public CharacterData[] characterDatas;
    public GameObject[] ClearImage;
    public Button[] targetButton;
    public Button cancleButton;

    private void OnEnable()
    {
        cancleButton.onClick.AddListener(OnCancleButton);

        for (int i = 0; i < targetButton.Length; i++)  //나중에 플레이어 데이터를 넣어서 플레이어 데이터 수만큼 인스턴스하고 그 버튼에 이미지 추가 필요
        {
            targetButton[i].onClick.RemoveAllListeners();
        }

        for (int i = 0; i < targetButton.Length; i++)  //임시 (버튼배열과 코드가 일치해야되는 문제점이있음) 버튼을 인스턴스해주는 방식으로 변경할 예정 지금은 이미지 안맞음
        {
            int index = i;
            if (targetButton[i] != null && characterDatas[i] != null)
            {
                targetButton[i].onClick.AddListener(() => OnClickCharactarBuy(index));

                if (GameManager.Instance.charactors.Any(c => c == characterDatas[i]))
                {
                    ClearImage[i].SetActive(true);
                    Destroy(targetButton[i]);
                }
            }
            
        }
    }

    public void OnClickCharactarBuy(int characterNum)
    {
        CharacterData data = characterDatas[characterNum];

        if (currentPannel != null)
            Destroy(currentPannel);

        currentPannel = Instantiate(pannel, pannelTransform);
        GuildPannel guildPanel = currentPannel.GetComponent<GuildPannel>();
        guildPanel.Init(data);

        //ClearImage[characterNum].SetActive(true);
        //Destroy(targetButton[characterNum]);
    }

    public void OnCancleButton()
    {
        this.gameObject.SetActive(false);
    }
}
