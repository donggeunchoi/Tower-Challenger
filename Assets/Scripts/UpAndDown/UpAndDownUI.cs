using TMPro;
using UnityEngine;

public class UpAndDownUI : MonoBehaviour
{
    public TextMeshProUGUI number;
    public TMP_InputField numInput;

    public void InitUi()
    {
        number.text = UpAndDownManager.instance.upAndDown.NumGenration();
    }

    public void AnswerButton()
    {

    }
}