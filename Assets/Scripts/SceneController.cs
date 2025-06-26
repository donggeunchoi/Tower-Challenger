using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName) //씬 로드
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAdditive(string sceneName)  //씬에있는 오브젝트를 현재씬에 추가
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)  //씬을 메모리에서 제거
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    //복사본
    private void OnEnable()
    {
        // 씬이 로드될 때마다 OnSceneLoaded 함수가 호출됨
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 이벤트 해제(중복 실행 및 메모리 누수 방지)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 완전히 로드되면 자동 호출
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬 로드 완료: " + scene.name);
        // 여기서 Map 등 원하는 오브젝트를 안전하게 탐색 및 초기화 가능
    }
}