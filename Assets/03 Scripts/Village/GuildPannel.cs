using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuildPannel : MonoBehaviour
{
    [SerializeField] public CharacterData characterData;
    private Guild guild;

    [SerializeField] private Image characterSprite;
    [SerializeField] private Image currencySprite;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancleButton;

    [SerializeField] private Sprite gold;
    [SerializeField] private Sprite diamond;

    public void Init(CharacterData data, Guild parentGuild)
    {
        characterData = data;
        guild = parentGuild;

        if (characterSprite != null && characterData.inGameImage != null)
            characterSprite.sprite = characterData.inGameImage;

        if (characterData.priceType == PriceType.dia)
            currencySprite.sprite = diamond;
        else if (characterData.priceType == PriceType.gold)
            currencySprite.sprite = gold;

        price.text = characterData.Price.ToString();
        characterName.text = characterData.name;
        description.text = characterData.description;

        buyButton.onClick.AddListener(OnClickBuy);
        cancleButton.onClick.AddListener(OnClickCancle);
    }

    private void OnClickBuy()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.character.charaters.Any(c => c.characterName == characterData.characterName))
        {
            Debug.Log("이미 구매한 캐릭터입니다");
            Destroy(this.gameObject);
            return;
        }

        bool purchased = false;

        switch (characterData.priceType)
        {
            case PriceType.dia:
                if (characterData.Price <= Save.playerData.diamond)
                {
                    GameManager.Instance.UseDiamond(characterData.Price);
                    GameManager.Instance.character.AddCharacter(characterData.characterName);
                    GameManager.Instance.SaveData();
                    purchased = true;
                }
                else
                {
                    Debug.Log("다이아몬드가 부족합니다");
                }
                break;

            case PriceType.gold:
                if (characterData.Price <= Save.playerData.gold)
                {
                    GameManager.Instance.UseGold(characterData.Price);
                    GameManager.Instance.character.AddCharacter(characterData.characterName);
                    GameManager.Instance.SaveData();
                    purchased = true;
                }
                else
                {
                    Debug.Log("골드가 부족합니다");
                }
                break;
            default:
                break;
        }

        if (purchased)
        {
            Guild guild = FindAnyObjectByType<Guild>();
            if (guild != null)
            {
                guild.RefreshCharacterButton();
            }
            Destroy(this.gameObject);
        }
    }

    private void OnClickCancle()
    {
        Destroy(this.gameObject);
    }
}
