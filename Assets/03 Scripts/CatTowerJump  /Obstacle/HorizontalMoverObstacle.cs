using UnityEngine;

public class HorizontalMoverObstacle : ObstacleBase
{
    public float moveDistance = 1f;
    public float moveSpeed = 2f;
    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
    }

    protected override void OnUpdateObstacle()
    {
        float delta = moveSpeed * Time.deltaTime * (movingRight ? 1f : -1f);
        transform.position += new Vector3(delta, 0, 0);

        if (transform.position.x >= startPos.x + moveDistance) movingRight = false;
        else if (transform.position.x <= startPos.x - moveDistance) movingRight = true;
    }
   
}
