using UnityEngine;

public class EnemyAattack : MonoBehaviour
{
    private Transform princess;

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

    private void Move()
    {
        Vector3 dir = (princess.position - transform.position).normalized;

        transform.position += dir * PrincessManager.princessInstance.moveSpeed * Time.deltaTime;
    }
}