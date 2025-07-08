using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HardGameGameManager : MonoBehaviour
{
    public GameObject[] StraightBall;
    public GameObject[] TurnBall;
    public GameObject[] TurnBall2;
    public GameObject[] Lv;
    [Header("플레이어")]
    public GameObject Player;
    [Header("속도")]
    public float moveSpeed = 5f;

    public GameObject SpeedInput;

    private TMP_InputField speedInputField;

    void Start()
    {
        if (SpeedInput)
        {
            speedInputField = SpeedInput.GetComponent<TMP_InputField>();
        }
    }
    void Update()
    {
        if (speedInputField != null)
        {
            float parsedSpeed;
            if (float.TryParse(speedInputField.text, out parsedSpeed))
            {
                moveSpeed = parsedSpeed;
            }
        }

        foreach (GameObject ball in StraightBall)
        {
            if (ball != null && ball.activeInHierarchy)
            {
                ball.transform.position += ball.transform.forward * moveSpeed * Time.deltaTime;
            }
        }

        foreach (GameObject ball in TurnBall)
        {
            if (ball != null && ball.activeInHierarchy)
            {
                ball.transform.Rotate(0f, 0f, 90f * Time.deltaTime); // 시계 방향
            }
        }

        foreach (GameObject ball in TurnBall2)
        {
            if (ball != null && ball.activeInHierarchy)
            {
                ball.transform.Rotate(0f, 0f, -90f * Time.deltaTime); // 반시계 방향
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
                        case 1:
                            newSpawnPos = new Vector3(-8f, 4.5f, Player.transform.position.z);
                            break;
                    }

                    Player.transform.position = newSpawnPos;
                    Player.GetComponent<HardGamePlayer>().SetSpawn(newSpawnPos);
                }

                return;
            }
        }
    }
}