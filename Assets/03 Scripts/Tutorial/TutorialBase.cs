using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Play(TutorialManager controller);
    public abstract void Exit();
}
