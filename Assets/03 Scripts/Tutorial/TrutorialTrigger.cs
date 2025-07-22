using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TrutorialTrigger : MonoBehaviour
{
    public TutorialManager tutorialManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tutorialManager == null)
        {
            tutorialManager = FindFirstObjectByType<TutorialManager>(FindObjectsInactive.Exclude); //FindObjectOfType<TutorialManager>();
        }
        
        var collider = GetComponent<Collider2D>();
        collider.isTrigger = true;

        if (GetComponent<Rigidbody2D>() == null)
        {
            var rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            rigidbody.gravityScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialManager.NextStep();
            Destroy(gameObject);
        }
    }
}
