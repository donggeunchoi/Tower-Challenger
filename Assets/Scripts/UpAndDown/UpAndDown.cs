using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public string NumGenration()
    {
        float num = Random.Range(0f, 101f);
        UpAndDownManager.instance.randomNumber = num;

        return UpAndDownManager.instance.randomNumber.ToString("N0");
    }

    public void Success()
    {

    }
}
