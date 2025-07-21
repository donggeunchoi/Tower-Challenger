using UnityEngine;
using UnityEngine.UI;

public class MoveTutorial : TutorialBase
{
    [SerializeField]
    private HintUI hintUI;
    [SerializeField]
    private string hintMessage;
    
    public override void Enter()
    {
        // HintUI.Show(hintMessage);
    }

    public override void Play(TutorialManager controller)
    {
        
    }

    public override void Exit()
    {
        hintUI.Hide();
    }
}
