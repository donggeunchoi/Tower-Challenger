using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HardGameGoalTrigger : MonoBehaviour
{
    public GameObject GoalBtn;

        private void Start()
        {
            GoalBtn.SetActive(false);

            Button btn = GoalBtn.GetComponent<Button>();

            if (btn != null)
            {
                btn.onClick.AddListener(GoalBtnClick);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
    {
        string npcName = this.gameObject.name;

        if (other.CompareTag("Player"))
        {
            GoalBtn.SetActive(true);
        }
    }
    private void GoalBtnClick()
    {
        this.gameObject.SetActive(false);

        GameObject goal = GameObject.Find("Goal");

        if (goal == null)
        {
            HardGameGameManager Next = FindObjectOfType<HardGameGameManager>();
            if (Next)Next.NextLv();
        }
        else
        {
            Debug.LogWarning("아직 있어");
        }
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GoalBtn.SetActive(false);
        }
    }

}
