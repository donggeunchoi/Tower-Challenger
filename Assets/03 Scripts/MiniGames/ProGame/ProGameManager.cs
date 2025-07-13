using System.Collections;
using TMPro;
using UnityEngine;

public class ProGameManager : MonoBehaviour
{
    public GameObject[] BallSpwun;
    public GameObject SpeedInput;

    [Header("속도")]
    public float moveSpeed = 5f;
    [Header("다음 패턴")]
    public float PatternSpeed = 3f;

    public GameObject BallPreFab;

    [Header("플레이어")]
    public GameObject Player;

    private TMP_InputField speedInputField;

    private bool GameStart;

    void Start()
    {
        GameStart = false;

        if (SpeedInput != null)
        {
            speedInputField = SpeedInput.GetComponent<TMP_InputField>();
        }

        // 시작 시 코루틴 실행
        StartCoroutine(GameRoutine());
    }

    private IEnumerator GameRoutine()
    {
        yield return new WaitForSeconds(3f);
        GameStart = true;

        while (GameStart)
        {
            int i = Random.Range(0, 3); // 0 또는 1

            if (i == 0)
            {
                StartCoroutine(AllBallCoroutine_StagePattern());
            }
            else if (i == 1)
            {
                StartCoroutine(AllBallCoroutine());
            }
            else if (i == 2) 
            {
               AllBall();
            }
            else
            {
                yield return null;
            }

            yield return new WaitForSeconds(PatternSpeed);
        }
    }

    private void AllBall()
    {
        foreach (GameObject spawnPoint in BallSpwun)
        {
            if (spawnPoint != null && Player != null)
            {
                Vector3 direction = (Player.transform.position - spawnPoint.transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);

                // Instantiate 대신 풀에서 가져오기
                GameObject spawnedBall = PoolManager.Instance.GetObject(BallPreFab, spawnPoint.transform.position, rotation);

                Ball moveScript = spawnedBall.GetComponent<Ball>();
                if (moveScript != null)
                {
                    moveScript.SetSpeed(moveSpeed);
                }
            }
        }
    }
    private IEnumerator AllBallCoroutine()
    {
        foreach (GameObject spawnPoint in BallSpwun)
        {
            if (spawnPoint != null && Player != null)
            {
                Vector3 direction = (Player.transform.position - spawnPoint.transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);

                GameObject spawnedBall = PoolManager.Instance.GetObject(BallPreFab, spawnPoint.transform.position, rotation);

                Ball moveScript = spawnedBall.GetComponent<Ball>();
                if (moveScript != null)
                {
                    moveScript.SetSpeed(moveSpeed);
                }

                yield return new WaitForSeconds(0.5f); // 각 공을 0.5초 간격으로 발사
            }
        }
    }
    private IEnumerator AllBallCoroutine_StagePattern()
    {
        // 1단계: 0, 1, 2
        yield return StartCoroutine(FireBallsByIndices(new int[] { 0, 1, 2 }));
        yield return new WaitForSeconds(0.5f);

        // 2단계: 3, 8
        yield return StartCoroutine(FireBallsByIndices(new int[] { 3, 7 }));
        yield return new WaitForSeconds(0.5f);

        // 3단계: 4, 5, 6
        yield return StartCoroutine(FireBallsByIndices(new int[] { 4, 5, 6 }));
    }
    private IEnumerator FireBallsByIndices(int[] indices)
    {
        foreach (int index in indices)
        {
            if (index >= 0 && index < BallSpwun.Length)
            {
                GameObject spawnPoint = BallSpwun[index];

                if (spawnPoint != null && Player != null)
                {
                    Vector3 direction = (Player.transform.position - spawnPoint.transform.position).normalized;
                    Quaternion rotation = Quaternion.LookRotation(direction);

                    GameObject spawnedBall = PoolManager.Instance.GetObject(BallPreFab, spawnPoint.transform.position, rotation);

                    Ball moveScript = spawnedBall.GetComponent<Ball>();
                    if (moveScript != null)
                    {
                        moveScript.SetSpeed(moveSpeed);
                    }
                }
            }
        }

        yield return null; // 코루틴이므로 최소 1프레임 대기
    }
    void Update() //입력창
    {
        if (speedInputField != null)
        {
            float parsedSpeed;
            if (float.TryParse(speedInputField.text, out parsedSpeed))
            {
                moveSpeed = parsedSpeed;
            }
        }

       
    }
}
