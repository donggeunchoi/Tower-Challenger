using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour
{
    // 발동 시 한 번 호출
    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    // 비활성화 시 한 번 호출
    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    // 매 프레임 동작
    protected virtual void Update()
    {
        OnUpdateObstacle();
    }

    // 파생 클래스가 구현해야 할 핵심 동작 메서드
    protected abstract void OnUpdateObstacle();
}
