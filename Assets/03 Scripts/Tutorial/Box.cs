using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public GameObject LPRenderer;

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

        StartCoroutine(Broken());
    }

    public void GoldShow()
    {
        LPRenderer.gameObject.SetActive(true);
    }

    IEnumerator Broken()
    {
        GoldShow();
        
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        
    }
   
}
