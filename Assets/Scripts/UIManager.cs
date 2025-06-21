using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //화면 가로고정
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
