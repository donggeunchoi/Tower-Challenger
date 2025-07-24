using UnityEngine;

public class BoxTutorial : TutorialBase 
{
    public PlayerInteraction playerInput;
    private bool _action = false;
    public bool Action => _action;
    [SerializeField]
    private HintUI hintUI;
    [SerializeField]
    private string hintMessage;
    [SerializeField]
    private string hintMessage2;

    public Box _box;
    
    private bool _interectionState;
    
    
    
    public override void Enter()
    {
        hintUI.Show(hintMessage);

        if (playerInput == null)
        {
            playerInput = FindFirstObjectByType<PlayerInteraction>(FindObjectsInactive.Exclude);
        }
        
        if(_box == null)
            Debug.Log("박스가 없어요");
        
        _action = false;
        _interectionState = playerInput.interactionButton;
    }

    public override void Play(TutorialManager controller)
    {
        
        if (!_action && playerInput.clicked)
        {
            _action = true;
            _box.Interact();
            hintUI.Show(hintMessage2);
        }
    }

    public override void Exit()
    {
        hintUI.Hide();
    }
    
}
