using UnityEngine;

public class BackGroundLooper : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float backgroundWidth; // 배경 하나의 가로 길이
    public int backGroundCount = 3; // 배경 갯수
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float speed = DinoMiniGame.instance.CurrentSpeed;
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // 배경이 왼쪽으로 완전히 나가면 오른쪽으로 재배치
        if (transform.position.x < -backgroundWidth)
        {
            float offset = backGroundCount * backgroundWidth;
            transform.position += Vector3.right * offset;
        }
    }
}
