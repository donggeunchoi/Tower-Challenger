using UnityEngine;
using UnityEngine.UI;

public class MoveTutorial : TutorialBase
{
    [SerializeField]
    private HintUI hintUI;
    [SerializeField]
    private string hintMessage;
    [SerializeField]
    private Transform target;

    private bool _moved;
    public override void Enter()
    {
        hintUI.Show(hintMessage);
        _moved = false;
    }

    public override void Play(TutorialManager controller)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        if (!_moved && (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f))
        {
            _moved = true;
            controller.NextStep();
            return;
        }
    }

    public override void Exit()
    {
        hintUI.Hide();
    }
}
