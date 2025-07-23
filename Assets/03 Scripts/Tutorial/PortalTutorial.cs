using UnityEngine;

public class PortalTutorial : TutorialBase
{
    [SerializeField]
    private HintUI hintUI;
    [SerializeField]
    private string hintMessage;
    [SerializeField]
    private Transform target;
    
    private bool _action;
    public bool Action => _action;

    public override void Enter()
    {
        var portal = FindFirstObjectByType<Map>(FindObjectsInactive.Exclude);
        
        hintUI.Show(hintMessage);
        portal.TutorialPortalOpen();
        
        _action = false;
    }

    public override void Play(TutorialManager collider)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        if (!_action && (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f))
        {
            _action = true;
        }
    }
    

    public override void Exit()
    {
        hintUI.Hide();
    }
    
}
