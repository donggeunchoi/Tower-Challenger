using UnityEngine;

public class BackGroundLooper : MonoBehaviour
{
    public Transform[] backgrounds; // 자식 배경들
    private float backgroundWidth;

    void Start()
    {
        // 자식 중 하나에서 가로 너비 가져오기
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        foreach (Transform bg in backgrounds)
        {
            if (bg.position.x < -backgroundWidth)
            {
                Transform rightmost = FindRightmostBackground();
                float newX = rightmost.position.x + backgroundWidth;
                bg.position = new Vector3(newX, bg.position.y, bg.position.z);
            }
        }
    }

    Transform FindRightmostBackground()
    {
        Transform rightmost = backgrounds[0];

        foreach (Transform bg in backgrounds)
        {
            if (bg.position.x > rightmost.position.x)
            {
                rightmost = bg;
            }
        }

        return rightmost;
    }
}
