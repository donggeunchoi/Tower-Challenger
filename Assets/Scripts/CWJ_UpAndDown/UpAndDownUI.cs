using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpAndDownUI : MonoBehaviour
{
    public TextMeshProUGUI number;
    public TextMeshProUGUI count;

    public TMP_InputField numInput;
    public Slider slider;

    public Image[] LP;
    public Sprite fullLP;
    public Sprite emptyLP;

    public void InitUi()
    {
        number.text = UpAndDownManager.instance.upAndDown.NumGenration();
        count.text = UpAndDownManager.instance.upAndDown.failcount.ToString();
        slider.maxValue = StageManager.instance.stageTimer.timer;

        for (int i = 0; i < LP.Length; i++)
        {
            LP[i].sprite = fullLP;
        }
    }
     
    public void AnswerButton()
    {
        UpAndDownManager.instance.upAndDown.Success();
        UpAndDownManager.instance.upAndDown.Failure();
    }
}