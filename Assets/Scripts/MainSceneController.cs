using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour // 이름 변경
{
    public static MainSceneController Instance; // 싱글톤 인스턴스 이름도 변경

    private Scene _mainScene;
    private GameObject[] _mainSceneRootObjects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 초기 메인 씬 설정
            _mainScene = SceneManager.GetActiveScene();
            _mainSceneRootObjects = _mainScene.GetRootGameObjects();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisableMainScene()
    {
        if (_mainSceneRootObjects == null) return;

        foreach (GameObject obj in _mainSceneRootObjects)
        {
            // DontDestroyOnLoad 오브젝트는 제외
            if (obj.scene.buildIndex == -1) continue;

            obj.SetActive(false);
        }
    }

    public void EnableMainScene()
    {
        if (_mainSceneRootObjects == null) return;

        foreach (GameObject obj in _mainSceneRootObjects)
        {
            if (obj.scene.buildIndex == -1) continue;

            obj.SetActive(true);
        }
    }
}