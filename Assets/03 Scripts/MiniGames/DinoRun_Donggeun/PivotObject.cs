using UnityEngine;

public class PivotObject : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
            transform.Rotate(Vector3.forward * 180f * Time.deltaTime);
    }
}
