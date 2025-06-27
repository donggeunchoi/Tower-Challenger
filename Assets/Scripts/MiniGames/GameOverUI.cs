using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button returnToTitle;
    private const string _mainSceneName = "VillageScene";  //메인씬 이름
    private void Start()
    {
        returnToTitle.onClick.AddListener(OnclickReturnTitle);
    }

    private void OnclickReturnTitle()
    {
        SceneManager.LoadScene(_mainSceneName);
        StageManager.instance.infoUI.SetActive(false);
    }
}
