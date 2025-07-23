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

    [SerializeField] private Box _box;
    
    private bool _interectionState = false;
    
    
    
    public override void Enter()
    {
        hintUI.Show(hintMessage);

        if (playerInput == null)
        {
            playerInput = FindFirstObjectByType<PlayerInteraction>(FindObjectsInactive.Exclude);
        }

        _box = GetComponentInChildren<Box>();
        
        _action = false;
        _interectionState = playerInput.interactionButton;
        Debug.Log(_interectionState);
    }

    public override void Play(TutorialManager controller)
    {
        
        if (!_action && playerInput.clicked)
        {
            _action = true;
            _box.Open();
            hintUI.Show(hintMessage2);
        }
    }

    public override void Exit()
    {
        hintUI.Hide();
    }
    
}
