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
   

    private bool _action;
    public bool Action => _action;
    
    public PlayerInput playerInput;
    
    public override void Enter()
    {
        hintUI.Show(hintMessage);
        _action = false;

        if (playerInput == null)
        {
            playerInput = FindFirstObjectByType<PlayerInput>(FindObjectsInactive.Exclude);
        }
    }

    public override void Play(TutorialManager controller)
    {
        if (!_action && playerInput.isDashing)
        {
            _action = true;
        }
    }

    public override void Exit()
    {
        hintUI.Hide();
    }
}