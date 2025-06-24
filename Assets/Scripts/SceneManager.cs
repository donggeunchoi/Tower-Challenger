using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private string _mainSceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 현재 씬을 메인 씬으로 설정
            _mainSceneName = SceneManager.GetActiveScene().name;
            Debug.Log($"메인 씬 설정: {_mainSceneName}");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 메인 씬 전체 비활성화
    public void DisableMainScene()
    {
        Scene mainScene = SceneManager.GetSceneByName(_mainSceneName);
        if (!mainScene.isLoaded) return;

        GameObject[] rootObjects = mainScene.GetRootGameObjects();
        Debug.Log($"비활성화할 오브젝트 수: {rootObjects.Length}");

        foreach (GameObject obj in rootObjects)
        {
            // DontDestroyOnLoad 오브젝트는 제외
            if (obj.transform.parent == null && obj.scene.name == _mainSceneName)
            {
                obj.SetActive(false);
                Debug.Log($"비활성화: {obj.name}");
            }
        }
    }

    // 메인 씬 전체 활성화
    public void EnableMainScene()
    {
        Scene mainScene = SceneManager.GetSceneByName(_mainSceneName);
        if (!mainScene.isLoaded) return;

        GameObject[] rootObjects = mainScene.GetRootGameObjects();
        Debug.Log($"활성화할 오브젝트 수: {rootObjects.Length}");

        foreach (GameObject obj in rootObjects)
        {
            if (obj.transform.parent == null && obj.scene.name == _mainSceneName)
            {
                obj.SetActive(true);
                Debug.Log($"활성화: {obj.name}");
            }
        }
    }
}