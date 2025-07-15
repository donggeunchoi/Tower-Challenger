using UnityEngine;

public class RemoveWall : MonoBehaviour
{
    private Transform cameraTransfrom;

    public float distanceCamera = 20f;


    private void Start()
    {
        cameraTransfrom = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < cameraTransfrom.position.y - distanceCamera)
        {
            WallPool.Instance.ReturnWall(this.gameObject);
        }
    }
}
