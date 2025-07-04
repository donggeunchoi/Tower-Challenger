using UnityEngine;

public class HardGameGameManager : MonoBehaviour
{
    public GameObject[] StraightBall;
    public GameObject[] TurnBall;
    public GameObject[] Lv;

    public float moveSpeed = 5f;

    void Update()
    {
        foreach (GameObject ball in StraightBall)
        {
            if (ball != null)
            {
                // 오브젝트의 forward 방향(즉, Z축 기준 방향)으로 이동
                ball.transform.position += ball.transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }
}