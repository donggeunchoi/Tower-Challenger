using UnityEngine;

public class PortalTutorial : TutorialBase
{
    [SerializeField]
    private HintUI hintUI;
    [SerializeField]
    private string hintMessage;
    [SerializeField] 
    private string hintMessage2;
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
        if (!_action)
        {
            hintUI.Show(hintMessage2);
            //내생각 여기에서 포탈을 생성을 해야하지 싶은데
        }
    }
    

    public override void Exit()
    {
        hintUI.Hide();
    }
    
}
