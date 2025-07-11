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
    [Header("배경 UI")]
    public GameObject BG, BG2;                  // 배경 스프라이트 오브젝트
    public float backgroundScrollSpeed = 1f;    // 배경 스크롤 속도
    private float backgroundWidth = 20f;        // 배경 하나의 너비

    public GameObject SpeedInput;

    private TMP_InputField speedInputField;

    private StageManager stageManager;

    void Start()
    {
        stageManager = StageManager.instance;
        if (SpeedInput)
        {
            speedInputField = SpeedInput.GetComponent<TMP_InputField>();
        }
        if (BG)
        {
            SpriteRenderer sr = BG.GetComponent<SpriteRenderer>();
            if (sr)
                backgroundWidth = sr.bounds.size.x;
        }

        // 배경 초기 위치 설정 (무한 스크롤용)
        if (BG && BG2)
        {
            BG.transform.position = new Vector3(0f, BG.transform.position.y, BG.transform.position.z);
            BG2.transform.position = new Vector3(backgroundWidth, BG2.transform.position.y, BG2.transform.position.z);
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
        ScrollBackground(Time.deltaTime);
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
    private void ScrollBackground(float deltaTime)
    {
        if (BG || BG2)
        {
            BG.transform.position += Vector3.left * backgroundScrollSpeed * deltaTime;
            BG2.transform.position += Vector3.left * backgroundScrollSpeed * deltaTime;

            if (BG.transform.position.x <= -backgroundWidth)
                BG.transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
            if (BG2.transform.position.x <= -backgroundWidth)
                BG2.transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
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
                            newSpawnPos = new Vector3(-6f, -5f, Player.transform.position.z);
                            break;
                        case 2:
                            newSpawnPos = new Vector3(-8f, -4f, Player.transform.position.z);
                            break;
                        case 3:
                            newSpawnPos = new Vector3(-8f, -3f, Player.transform.position.z);
                            break;
                        case 4:
                            newSpawnPos = new Vector3(-10f, -5f, Player.transform.position.z);
                            break;
                        case 5:
                            stageManager.MiniGameResult(true);
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