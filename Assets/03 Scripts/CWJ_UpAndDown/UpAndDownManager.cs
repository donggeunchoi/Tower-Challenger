using System.Collections;
using UnityEngine;

public class UpAndDownManager : MonoBehaviour
{
    public static UpAndDownManager instance;

    public UpAndDownUI upAndDownUI;
    public UpAndDown upAndDown;


    public float randomNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upAndDownUI.InitUi();
    }
    private void Update()
    {
        //upAndDownUI.slider.value = StageManager.instance.stageTimer.timer;
    }

    public IEnumerator ShowAnswer()
    {
        upAndDownUI.number.text = randomNumber.ToString();

        yield return new WaitForSeconds(1.0f);

        if(StageManager.instance != null)
            StageManager.instance.MiniGameResult(true);
    }
}