using UnityEngine;

public class FireAttack : MonoBehaviour
{
    private Transform princess;

    private int side;

    private void OnEnable()
    {
        GameObject obj = GameObject.Find("Princess");
        if (obj != null )
        {
            princess = obj.GetComponent<Transform>();
        }
        else
        {
            Debug.Log("princess is null");
        }
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        Vector3 dir = (princess.position - transform.position).normalized;

        transform.position += dir * PrincessManager.princessInstance.fireSpeed * Time.deltaTime;
    }
}
