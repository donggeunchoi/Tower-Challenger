using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    public void Interact()
    {
        Open();
    }
    
    
    public void Open()
    {
        if (_isOpen)
        {
            return;
        }
        
        _isOpen = true;
        spriteRenderer.sprite = openSprite;
        
    }
   
}
