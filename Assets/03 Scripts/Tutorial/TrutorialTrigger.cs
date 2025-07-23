using System;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TrutorialTrigger : MonoBehaviour,IInteractable
{
    public TutorialManager tutorialManager;
    private TutorialBase _tutorialStep;
    

   // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tutorialManager == null)
        {
            tutorialManager = FindFirstObjectByType<TutorialManager>(FindObjectsInactive.Exclude); //FindObjectOfType<TutorialManager>();
        }
        _tutorialStep = GetComponentInParent<TutorialBase>();

        if (GetComponent<Rigidbody2D>() == null)
        {
            var rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            rigidbody.gravityScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        
        var dashStep = _tutorialStep as DashTutorial;
        var boxStep = _tutorialStep as BoxTutorial;
        var inventoryStep = _tutorialStep as InventoryTutorial;
        var portalStep = _tutorialStep as PortalTutorial;
            
        if (dashStep != null && !dashStep.Action)
        {
            return;
        }

        if (boxStep != null && !boxStep.Action)
        {
            return;
        }

        if (inventoryStep != null && !inventoryStep.Action)
        {
            return;
        }

        if (portalStep != null && !portalStep.Action)
        {
            return;
        }
        
            
        tutorialManager.NextStep();
        Destroy(gameObject);
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            tutorialManager.NextStep();
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        //여기에서 박스를 작동시킬 순 없을까?
    }
}
