using UnityEngine;

public class InventoryTutorial : TutorialBase
{
    [SerializeField]
    private HintUI hintUI;
    [SerializeField]
    private string hintMessage;
    [SerializeField] 
    private string hintMessage2;
    [SerializeField]
    private Transform target;
    [SerializeField] 
    private float targetClear = 0.5f;
    
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
        if (!_action && playerInput.isInventory)
        {
            _action = true;
            hintUI.Show(hintMessage2);
        }
    }

    public override void Exit()
    {
        hintUI.Hide();
    }
   
}
