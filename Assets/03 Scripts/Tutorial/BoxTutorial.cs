using UnityEngine;

public class BoxTutorial : TutorialBase
{
    public PlayerInteraction playerInput;
    private bool _action = false;
    public bool Action => _action;
    [SerializeField] private HintUI hintUI;
    [SerializeField] private string hintMessage;
    [SerializeField] private string hintMessage2;

    [SerializeField] private InteractionBox targetBox;


    private bool _interectionState;



    public override void Enter()
    {
        hintUI.Show(hintMessage);

        if (playerInput == null)
        {
            playerInput = FindFirstObjectByType<PlayerInteraction>(FindObjectsInactive.Exclude);
        }

        if (targetBox == null)
            targetBox = FindFirstObjectByType<InteractionBox>(FindObjectsInactive.Exclude);

        if (targetBox != null)
            targetBox.OnOpened += HandleBoxOpened; // ← 열림 이벤트 구독
        else
            Debug.LogWarning("BoxTutorial: targetBox가 없습니다.");

        _action = false;
        _interectionState = playerInput.interactionButton;
    }

    public override void Play(TutorialManager controller)
    {

        // if (!_action && playerInput.clicked)
        // {
        //     _action = true;
        //     hintUI.Show(hintMessage2);
        // }
    }

    public override void Exit()
    {
        if (targetBox != null)
            targetBox.OnOpened -= HandleBoxOpened; 
        hintUI.Hide();
    }


    private void HandleBoxOpened()
    {
        _action = true;
        hintUI.Show(hintMessage2);
    }

}
