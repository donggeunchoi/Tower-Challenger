using UnityEngine;

public class TutorialPortalOpen : MonoBehaviour
{
    public GameObject tutorialPortal;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("여긴 들어오긴했니?");
        Debug.Log(tutorialPortal.activeSelf);
        if (other.CompareTag("Player") && !tutorialPortal.activeSelf)
        {
            tutorialPortal.SetActive(true);
        }
    }
}
