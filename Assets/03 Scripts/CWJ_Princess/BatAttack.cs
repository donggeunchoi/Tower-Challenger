using UnityEngine;

public class BatAttack : MonoBehaviour
{
    private Transform princess;

    private float amplitude;
    private float curAmplitude;
    private int random;

    float time = 0f;
    float rangeTime = 0.5f;
    private void OnEnable()
    {
        GameObject obj = GameObject.Find("Princess");
        if (obj != null)
        {
            princess = obj.transform;
        }
    }

    private void Start()
    {
        amplitude = Random.Range(PrincessManager.princessInstance.minY, PrincessManager.princessInstance.maxY);
        curAmplitude = amplitude;

        RandomAttck();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= rangeTime)
        {
            amplitude = Random.Range(PrincessManager.princessInstance.minY, PrincessManager.princessInstance.maxY);
            time = 0f;
        }
        curAmplitude = Mathf.Lerp(curAmplitude, amplitude, Time.deltaTime);

        switch (random)
        {
            case 0:
                AmplitudeAttack();
                break;
            case 1:
                StraightAttack();
                break;
        }
    }

    private void RandomAttck()
    {
        random = Random.Range(0, 2);

        switch (random)
        {
            case 0:
                AmplitudeAttack();
                break;
            case 1:
                StraightAttack();
                break;
        }
    }


    private void AmplitudeAttack()
    {
        //
        float wave = Mathf.Sin(Time.time * PrincessManager.princessInstance.frequency) * curAmplitude;

        // 몬스터와 타겟과의 거리
        Vector3 dir = (princess.position - transform.position).normalized;
        // 
        Vector3 move = dir * PrincessManager.princessInstance.batSpeed * Time.deltaTime;

        // 공주에게로 이동
        Vector3 newPos = transform.position + move;

        // 공주에게 이동하는 동안 지그재그로 움직임
        newPos.y = wave;

        transform.position = newPos;
    }

    private void StraightAttack()
    {
        Vector3 dir = (princess.position - transform.position).normalized;
        // 
        transform.position += dir * (PrincessManager.princessInstance.batSpeed + 10f) * Time.deltaTime;
    }
}
