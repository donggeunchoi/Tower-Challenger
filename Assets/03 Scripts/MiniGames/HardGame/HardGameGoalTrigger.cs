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

            if (this.gameObject.name == "Goal")
            {
                btn.onClick.AddListener(GoalBtnClick);
            }
            else
            {
            btn.onClick.AddListener(GoalConditionClick);
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
        GameObject goal = GameObject.Find("GoalCondition");

        if (goal == null)
        {
            HardGameGameManager next = Object.FindFirstObjectByType<HardGameGameManager>();
            if (next) next.NextLv();
        }
        else
        {
            Debug.Log("아직 있어");
        }
    }
    private void GoalConditionClick()
    {
        this.gameObject.SetActive(false);
    }




    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GoalBtn.SetActive(false);
        }
    }

}
