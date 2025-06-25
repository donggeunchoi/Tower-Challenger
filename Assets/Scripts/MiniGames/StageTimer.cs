using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float timer = 60f;
    private bool isActive = false;

    private void Start()
    {
        isActive = false;
    }

    private void Update()
    {
        //if (isActive)
        //{
        //    timer -= Time.deltaTime;
        //}
        PlayTiem();
    }
    public void PlayTiem()
    {
        timer -= Time.deltaTime;
    }
    public void SetTimer()
    {
        timer = 60f;
        isActive = true;
    }

    public void ResetTimer()
    {
        timer = 60f;
        isActive = false;
    }
}
