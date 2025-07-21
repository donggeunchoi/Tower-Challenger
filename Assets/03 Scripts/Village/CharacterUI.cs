using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Image characterImage;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.character.equippedCharacter!= null)
                characterImage.sprite = GameManager.Instance.character.equippedCharacter.characterImage;
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        { 
            if (GameManager.Instance.character.equippedCharacter != null)
                characterImage.sprite = GameManager.Instance.character.equippedCharacter.characterImage;
        }
    }
}
