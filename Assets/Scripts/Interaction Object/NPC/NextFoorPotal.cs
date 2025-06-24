using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFoorPotal : MonoBehaviour
{
    StageManager stageManager;

    private void Awake()
    {
        stageManager = StageManager.instance;
    }

    private void Start()
    {
        stageManager.NextFloor();
        SceneManager.LoadScene("TopScene-2");
    }
}
