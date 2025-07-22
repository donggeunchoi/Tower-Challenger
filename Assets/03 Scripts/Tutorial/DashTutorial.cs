using UnityEngine;
using UnityEngine.UI;

public class DashTutorial : TutorialBase
{
    [SerializeField]
    private HintUI hintUI;
    [SerializeField]
    private string hintMessage;
    [SerializeField]
    private Transform target;
    [SerializeField] 
    private float targetClear = 0.5f; //도달 판정 반경

    private bool _dash;
    
    public PlayerInput playerInput;
    
    public override void Enter()
    {
        hintUI.Show(hintMessage);
        _dash = false;

        if (playerInput == null)
        {
            playerInput = FindFirstObjectByType<PlayerInput>(FindObjectsInactive.Exclude);
        }
    }

    public override void Play(TutorialManager controller)
    {
        if (!_dash && playerInput.isDashing)
        {
            _dash = true;
            controller.NextStep();
        }
           
        
    }

    public override void Exit()
    {
        hintUI.Hide();
    }
}