using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;      // 따라갈 대상 (고양이)
    public float smoothSpeed;
    public float yOffset;    // 약간 위에서 보이도록

    private float highestY;       // 지금까지 도달한 가장 높은 y

    void Start()
    {
        if (target != null)
        {
            highestY = target.position.y;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        float targetY = target.position.y + yOffset;

        // 오직 높아질 때만 따라감 (아래로는 안 내려옴)
        
            highestY = targetY;
            Vector3 newPosition = new Vector3(transform.position.x, highestY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
        
    }
}
