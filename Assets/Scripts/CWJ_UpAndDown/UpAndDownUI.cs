using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpAndDownUI : MonoBehaviour
{
    public TextMeshProUGUI number;
    public TextMeshProUGUI count;
    public TextMeshProUGUI lp;

    public TMP_InputField numInput;
    public Slider slider;

    public void InitUi()
    {
        number.text = UpAndDownManager.instance.upAndDown.NumGenration();
        count.text = UpAndDownManager.instance.upAndDown.failcount.ToString();
        lp.text = StageManager.instance.stageLP.currentLP.ToString();
        slider.maxValue = StageManager.instance.stageTimer.timer;
    }

    public void AnswerButton()
    {
        UpAndDownManager.instance.upAndDown.Success();
        UpAndDownManager.instance.upAndDown.Failure();
    }
}