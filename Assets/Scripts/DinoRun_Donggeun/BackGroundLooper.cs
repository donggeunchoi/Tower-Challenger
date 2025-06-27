using UnityEngine;

public class BackGroundLooper : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float backgroundWidth = 20f; // 배경 하나의 가로 길이
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
            transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
        }
    }
}
