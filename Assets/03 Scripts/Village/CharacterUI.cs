using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Image characterImage;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.equimentCharacter != null)
            characterImage.sprite = GameManager.Instance.equimentCharacter.characterImage;
        }
    }
}
