using UnityEngine;

public class BarSize : MonoBehaviour
{
    public float scaleX = 4f;

    void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = scaleX;
        transform.localScale = scale;
    }
}