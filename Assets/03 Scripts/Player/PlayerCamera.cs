using UnityEditor;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothSpeed = 5f;

    private void Awake()
    {   
        //DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        GameObject[] camera = GameObject.FindGameObjectsWithTag("MainCamera");
        if (camera.Length >= 2)
        {
            for (int i = 0; i < camera.Length; i++)
            {
                if (camera[i] != this.gameObject)
                {
                    Destroy(camera[i]);
                }
            }  
        }

        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            this.transform.position = smoothedPosition;
        }
    }
}
