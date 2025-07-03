using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpAndDownUI : MonoBehaviour
{
    public TextMeshProUGUI number;
    public TextMeshProUGUI count;

    public TMP_InputField numInput;

    public Image[] LP;
    public Sprite fullLP;
    public Sprite emptyLP;

    public Button[] answerButton;
    public TextMeshProUGUI textAnswer;

    public Image up;
    public Image down;

    public Image buttonEnter;
    public Image buttonExit;

    public void InitUi()
    {
        number.text = UpAndDownManager.instance.upAndDown.NumGenration();
        count.text = UpAndDownManager.instance.upAndDown.curCount.ToString();

        SetActiveFalse();

        for (int i = 0; i < LP.Length; i++)
        {
            LP[i].sprite = fullLP;
        }

    }


    public void AnswerButton()
    {
        UpAndDownManager.instance.upAndDown.Success();
        UpAndDownManager.instance.upAndDown.Failure();
        count.text = UpAndDownManager.instance.upAndDown.curCount.ToString();

        StartCoroutine(ButtonCorutine());

        textAnswer.text = "";
    }

    public void InputAnswerButton(int index)
    {
        UpAndDownManager.instance.upAndDown.InputAnswer(index);
    }

    public void SetActiveFalse()
    {
        up.gameObject.SetActive(false);
        down.gameObject.SetActive(false);
        buttonExit.gameObject.SetActive(false);
    }

    private IEnumerator ButtonCorutine()
    {
        buttonEnter.gameObject.SetActive(false);
        buttonExit.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        buttonExit.gameObject.SetActive(false);
        buttonEnter.gameObject.SetActive(true);
    }
}