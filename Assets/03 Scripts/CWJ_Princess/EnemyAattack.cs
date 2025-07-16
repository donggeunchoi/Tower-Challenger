using UnityEngine;

public class EnemyAattack : MonoBehaviour
{
    private Transform princess;
    private float rangeVetical;

    private float rangeTime = 0f;

    private void OnEnable()
    {
        GameObject obj = GameObject.Find("Princess");
        if (obj != null )
        {
            princess = obj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private float RangeVertical()
    {
        rangeVetical = Random.Range(PrincessManager.princessInstance.minY, PrincessManager.princessInstance.maxY);
        
        return rangeVetical;
    }

    private void Move()
    {
        //
        float veticalOffset = Mathf.Sin(Time.time * PrincessManager.princessInstance.veticalSpeed) * RangeVertical();

        // 타겟과의 거리 계산
        Vector3 dir = (princess.position - transform.position).normalized;

        Vector3 move = dir * PrincessManager.princessInstance.batSpeed * Time.deltaTime;

        Vector3 newPos = transform.position + move;
        newPos.y += veticalOffset;

        transform.position = newPos;
    }
}