using UnityEngine;

public class SpinnerObstacle : ObstacleBase
{
    public float spinSpeed = 180f;
    
    protected override void OnUpdateObstacle()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}
