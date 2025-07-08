using Unity.VisualScripting;
using UnityEngine;

public class HardGameGameManager : MonoBehaviour
{
    public GameObject[] StraightBall;
    public GameObject[] TurnBall;
    public GameObject[] Lv;
    [Header("플레이어")]
    public GameObject Player;
    [Header("속도")]
    public float moveSpeed = 5f;

    void Update()
    {
        foreach (GameObject ball in StraightBall)
        {
            if (ball != null && ball.activeInHierarchy)
            {
                // 오브젝트의 forward 방향(즉, Z축 기준 방향)으로 이동
                ball.transform.position += ball.transform.forward * moveSpeed * Time.deltaTime;
            }
        }
        foreach (GameObject ball in TurnBall)
        {
            if (ball != null && ball.activeInHierarchy)
            {
                // Z축 기준으로 회전 (예: 초당 90도)
                ball.transform.Rotate(0f, 0f, 90f * Time.deltaTime);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Ball"))
        {
            Vector3 rot = transform.eulerAngles;
            rot.y += 180f;
            transform.rotation = Quaternion.Euler(rot);
        }
    }

    public void NextLv()
    {
        for (int i = 0; i < Lv.Length - 1; i++)
        {
            if (Lv[i].activeSelf)
            {
                Lv[i].SetActive(false);
                Lv[i + 1].SetActive(true);

                if (Player)
                {
                    Vector3 newSpawnPos = Vector3.zero;

                    switch (i + 1)
                    {
                        case 1: //2스테이지
                            newSpawnPos = new Vector3(-8f, 4.5f, Player.transform.position.z);
                            break;
                    }

                    Player.transform.position = newSpawnPos;

                    // 여기가 핵심!
                    Player.GetComponent<HardGamePlayer>().SetSpawn(newSpawnPos);
                }

                return;
            }
        }
    }
}