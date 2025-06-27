using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void OnclickStart()
    {
        SceneManager.LoadScene("VillageScene");
    }
}
