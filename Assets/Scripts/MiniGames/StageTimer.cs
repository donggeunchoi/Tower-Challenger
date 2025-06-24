using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float timer = 60f;
    private bool _isActive = true;

    private void Update()
    {
        if (_isActive) timer -= Time.deltaTime;
    }

    public void Pause() => _isActive = false;
    public void Resume() => _isActive = true;
}
